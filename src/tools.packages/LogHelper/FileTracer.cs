using crossapp.log.logging;
using System;
using System.Threading.Tasks;

namespace LogHelper
{
    public class FileTracer : ILogHandler
    {
        readonly string UrlFile;
        /// <summary>
        /// Used to lock threads
        /// </summary>
        readonly object Semaphore = new object();

        private string Format(string message=null, Exception exception = null)
        {
            return $"{Environment.NewLine}{Environment.NewLine}************{Environment.NewLine}{message ?? String.Empty}{Environment.NewLine}{exception?.ToString() ?? String.Empty}{Environment.NewLine}";
        }

        public FileTracer(string urlFile)
        {
            this.UrlFile = urlFile;
        }
        /// <summary>
        /// Trace an error
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        public async Task Trace(string message, Exception exception = null)
        {
            lock (Semaphore)
            {
                System.IO.File.AppendAllText(this.UrlFile, this.Format(message, exception));
            }
        }
        /// <summary>
        /// Trace an error
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public Task Trace(Exception exception)
        {
            return Trace(null, exception);
        }
    }
}
