using NLog;

namespace EntityFrameworkDemo.Log
{
    public class LogAdapter
    {
        private ILogger _logger;

        public void Initial<T>()
        {
            _logger = LogManager.GetLogger(typeof(T).FullName);
        }

    }
}