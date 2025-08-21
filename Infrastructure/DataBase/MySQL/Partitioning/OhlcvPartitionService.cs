using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using TradingAssistant.Core.Entities.Exchanges;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Partitioning;

/// <summary>
/// Сервис для управления партициями таблицы OHLCV данных
/// Реализует LIST партиционирование по InstrumentId
/// </summary>
public class OhlcvPartitionService : IOhlcvPartitionService
{
    private readonly AppDbContext _context;
    private readonly ILogger<OhlcvPartitionService> _logger;
    private readonly string _connectionString;

    public OhlcvPartitionService(
        AppDbContext context,
        ILogger<OhlcvPartitionService> logger)
    {
        _context = context;
        _logger = logger;
        _connectionString = _context.Database.GetConnectionString()
                          ?? throw new InvalidOperationException("Connection string is not available");
    }

    /// <inheritdoc />
    public async Task<bool> PartitionExistsAsync(long instrumentId, CancellationToken cancellationToken = default)
    {
        try
        {
            var partitionName = GetPartitionName(instrumentId);
            var sql = $"""
                SELECT COUNT(*) 
                FROM information_schema.PARTITIONS 
                WHERE TABLE_SCHEMA = DATABASE() 
                AND TABLE_NAME = 'OhlcvData' 
                AND PARTITION_NAME = '{partitionName}'
                """;

            var exists = await _context.Database.ExecuteSqlRawAsync(sql, cancellationToken) > 0;
            _logger.LogDebug("Проверка партиции {PartitionName}: {Exists}", partitionName, exists);

            return exists;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при проверке существования партиции для InstrumentId {InstrumentId}", instrumentId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> CreatePartitionAsync(long instrumentId, CancellationToken cancellationToken = default)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            var partitionName = GetPartitionName(instrumentId);

            // Проверяем, не существует ли уже партиция
            var checkSql = $"""
                SELECT COUNT(*) 
                FROM information_schema.PARTITIONS 
                WHERE TABLE_SCHEMA = DATABASE() 
                AND TABLE_NAME = 'OhlcvData' 
                AND PARTITION_NAME = '{partitionName}'
                """;

            await using var checkCommand = new MySqlCommand(checkSql, connection, transaction);
            var exists = (long)(await checkCommand.ExecuteScalarAsync(cancellationToken))! > 0;

            if (exists)
            {
                _logger.LogWarning("Партиция {PartitionName} уже существует", partitionName);
                return false;
            }

            // Создаем новую партицию
            var createSql = $"""
                ALTER TABLE OhlcvData 
                ADD PARTITION (
                    PARTITION {partitionName} VALUES IN ({instrumentId})
                )
                """;

            await using var createCommand = new MySqlCommand(createSql, connection, transaction);
            await createCommand.ExecuteNonQueryAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Создана новая партиция {PartitionName} для InstrumentId {InstrumentId}",
                partitionName, instrumentId);

            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Ошибка при создании партиции для InstrumentId {InstrumentId}", instrumentId);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<int> RepartitionFromDefaultAsync(CancellationToken cancellationToken = default)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        try
        {
            // Находим записи в p_other, которые должны быть в других партициях
            var sql = """
                SELECT DISTINCT InstrumentId 
                FROM OhlcvData PARTITION (p_other)
                WHERE InstrumentId IS NOT NULL
                """;

            await using var command = new MySqlCommand(sql, connection);
            await using var reader = await command.ExecuteReaderAsync(cancellationToken);

            var instrumentIds = new List<long>();
            while (await reader.ReadAsync(cancellationToken))
            {
                instrumentIds.Add(reader.GetInt64(0));
            }

            await reader.CloseAsync();

            var movedCount = 0;
            foreach (var instrumentId in instrumentIds)
            {
                movedCount += await RepartitionInstrumentDataAsync(instrumentId, connection, cancellationToken);
            }

            _logger.LogInformation("Перемещено {Count} записей из партиции p_other", movedCount);
            return movedCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при перепартизации данных из p_other");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<int> RepartitionFromPartitionAsync(string partitionName, CancellationToken cancellationToken = default)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        try
        {
            // Получаем InstrumentId из имени партиции
            if (!long.TryParse(partitionName.Replace("p_instrument_", ""), out var instrumentId))
            {
                throw new ArgumentException($"Неверное имя партиции: {partitionName}");
            }

            var movedCount = await RepartitionInstrumentDataAsync(instrumentId, connection, cancellationToken);

            _logger.LogInformation("Перемещено {Count} записей из партиции {PartitionName}",
                movedCount, partitionName);

            return movedCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при перепартизации данных из партиции {PartitionName}", partitionName);
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<List<string>> GetPartitionsListAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var sql = """
                SELECT PARTITION_NAME 
                FROM information_schema.PARTITIONS 
                WHERE TABLE_SCHEMA = DATABASE() 
                AND TABLE_NAME = 'OhlcvData'
                ORDER BY PARTITION_NAME
                """;

            return await _context.Database.SqlQueryRaw<string>(sql)
                .ToListAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка партиций");
            throw;
        }
    }

    /// <inheritdoc />
    public async Task<bool> RemoveEmptyPartitionAsync(long instrumentId, CancellationToken cancellationToken = default)
    {
        await using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);

        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            var partitionName = GetPartitionName(instrumentId);

            // Проверяем, что партиция пуста
            var checkSql = $"""
                SELECT COUNT(*) 
                FROM OhlcvData PARTITION ({partitionName})
                """;

            await using var checkCommand = new MySqlCommand(checkSql, connection, transaction);
            var count = (long)(await checkCommand.ExecuteScalarAsync(cancellationToken))!;

            if (count > 0)
            {
                _logger.LogWarning("Партиция {PartitionName} не пуста ({Count} записей)", partitionName, count);
                return false;
            }

            // Удаляем пустую партицию
            var removeSql = $"""
                ALTER TABLE OhlcvData 
                DROP PARTITION {partitionName}
                """;

            await using var removeCommand = new MySqlCommand(removeSql, connection, transaction);
            await removeCommand.ExecuteNonQueryAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            _logger.LogInformation("Удалена пустая партиция {PartitionName}", partitionName);
            return true;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Ошибка при удалении партиции для InstrumentId {InstrumentId}", instrumentId);
            throw;
        }
    }

    /// <summary>
    /// Генерирует имя партиции на основе InstrumentId
    /// </summary>
    private static string GetPartitionName(long instrumentId) => $"p_instrument_{instrumentId}";

    /// <summary>
    /// Перемещает данные для конкретного InstrumentId в правильную партицию
    /// </summary>
    private async Task<int> RepartitionInstrumentDataAsync(long instrumentId, MySqlConnection connection, CancellationToken cancellationToken)
    {
        await using var transaction = await connection.BeginTransactionAsync(cancellationToken);

        try
        {
            // Создаем партицию если её нет
            if (!await PartitionExistsAsync(instrumentId, cancellationToken))
            {
                await CreatePartitionAsync(instrumentId, cancellationToken);
            }

            // Перемещаем данные
            var moveSql = $"""
                INSERT INTO OhlcvData 
                SELECT * FROM OhlcvData PARTITION (p_other) 
                WHERE InstrumentId = {instrumentId}
                """;

            await using var moveCommand = new MySqlCommand(moveSql, connection, transaction);
            var affectedRows = await moveCommand.ExecuteNonQueryAsync(cancellationToken);

            // Удаляем перемещенные данные из p_other
            if (affectedRows > 0)
            {
                var deleteSql = $"""
                    DELETE FROM OhlcvData PARTITION (p_other) 
                    WHERE InstrumentId = {instrumentId}
                    """;

                await using var deleteCommand = new MySqlCommand(deleteSql, connection, transaction);
                await deleteCommand.ExecuteNonQueryAsync(cancellationToken);
            }

            await transaction.CommitAsync(cancellationToken);

            _logger.LogDebug("Перемещено {Count} записей для InstrumentId {InstrumentId}",
                affectedRows, instrumentId);

            return affectedRows;
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync(cancellationToken);
            _logger.LogError(ex, "Ошибка при перемещении данных для InstrumentId {InstrumentId}", instrumentId);
            throw;
        }
    }
}