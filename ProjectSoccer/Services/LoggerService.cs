namespace ProjectSoccer.Services
{
    public class LoggerService : ILoggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[ConsoleLogger] - " + message);
        }
    }
}
