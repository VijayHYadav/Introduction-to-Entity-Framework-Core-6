using Microsoft.Extensions.Logging;

namespace EFCoreMoviesTests.Mocks
{
    public class LoggerFake<T> : ILogger<T>
    {
        public int CountLogs { get; set; }
        public string LastLog { get; set; }
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            CountLogs++;
            LastLog = state.ToString();
        }
    }
}