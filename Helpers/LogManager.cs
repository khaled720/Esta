using Microsoft.AspNetCore;

namespace ESTA.Helpers
{
    public class LogManager<T>
    {
        private readonly ILogger<T> logger;

        public LogManager(ILogger<T> _logger)
        {
            logger = _logger;
        }

        public void WriteError(string str)
        {
            try
            {
                logger.LogError(str);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void WriteInfo(string str)
        {
            try
            {
                logger.LogInformation(str);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
