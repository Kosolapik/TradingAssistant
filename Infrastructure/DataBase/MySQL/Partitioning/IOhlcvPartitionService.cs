using MySqlConnector;

namespace TradingAssistant.Infrastructure.DataBase.MySQL.Partitioning;

/// <summary>
/// Сервис для управления партициями таблицы OHLCV данных
/// Использует LIST партиционирование по InstrumentId
/// </summary>
public interface IOhlcvPartitionService
{
    /// <summary>
    /// Проверяет существование партиции для указанного InstrumentId
    /// </summary>
    /// <param name="instrumentId">ID торгового инструмента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True если партиция существует, иначе False</returns>
    Task<bool> PartitionExistsAsync(long instrumentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создает новую партицию для указанного InstrumentId
    /// </summary>
    /// <param name="instrumentId">ID торгового инструмента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>True если партиция создана успешно</returns>
    Task<bool> CreatePartitionAsync(long instrumentId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Перемещает данные из партиции по умолчанию (p_other) в правильные партиции
    /// Запускается вручную для исправления ошибочно попавших данных
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество перемещенных записей</returns>
    Task<int> RepartitionFromDefaultAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Перепартизирует данные из указанной партиции в правильные партиции
    /// </summary>
    /// <param name="partitionName">Имя партиции для обработки</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Количество перемещенных записей</returns>
    Task<int> RepartitionFromPartitionAsync(string partitionName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получает список всех существующих партиций
    /// </summary>
    Task<List<string>> GetPartitionsListAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Удаляет пустую партицию (только если в ней нет данных)
    /// </summary>
    Task<bool> RemoveEmptyPartitionAsync(long instrumentId, CancellationToken cancellationToken = default);
}