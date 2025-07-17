using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant.Infrastructure
{
    internal static class Config
    {
        private static readonly Lazy<IConfiguration> _config = new(LoadConfiguration);

        private static IConfiguration LoadConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
        }

        public static string ConnectionStringMySQL()
        {
            var connectionString = _config.Value["DatabaseConnections:MySQL"];

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException("Database connection string is not configured");

            return connectionString;
        }


    }
}
