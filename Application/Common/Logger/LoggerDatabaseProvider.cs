using Exam2C2P.Application.Common.Interfaces;
using Exam2C2P.Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Exam2C2P.Application.Common.Logger
{
    public class LoggerDatabaseProvider : ILoggerProvider
    {
        private string _connectionString;

        public LoggerDatabaseProvider(string connectionString)
        {
            _connectionString = connectionString;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new Logger(categoryName, _connectionString);
        }

        public void Dispose()
        {
        }

        public class Logger : ILogger
        {
            private readonly string _categoryName;
            private string _connectionString;

            public Logger(string categoryName, string connectionString)
            {
                _connectionString = connectionString;
                _categoryName = categoryName;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (logLevel == LogLevel.Critical || logLevel == LogLevel.Error || logLevel == LogLevel.Warning)
                {
                    RecordMsg(logLevel, eventId, state, exception, formatter);
                }

            }

            private void RecordMsg<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                string query = @"INSERT INTO [dbo].[EventLogs]
                           ([EventId]
                           ,[LogLevel]
                           ,[Message]
                           ,[Created])
                     VALUES
                           (@EventId
                           ,@LogLevel
                           ,@Message
                           ,@Created)";

                using SqlConnection connection = new SqlConnection(_connectionString);
                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@EventId", eventId.Id);
                command.Parameters.AddWithValue("@LogLevel", logLevel.ToString());
                command.Parameters.AddWithValue("@Message", formatter(state, exception));
                command.Parameters.AddWithValue("@Created", DateTime.UtcNow);
                command.CommandType = CommandType.Text;
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return new NoopDisposable();
            }

            private class NoopDisposable : IDisposable
            {
                public void Dispose()
                {
                }
            }
        }
    }
}
