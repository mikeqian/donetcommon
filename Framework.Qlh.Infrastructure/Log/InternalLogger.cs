using System;
using System.IO;

namespace Framework.Qlh.Common.Infrastructure.Log
{
    internal static class InternalLogger
    {
        private static object _logLockObject = new object();
        /// <summary>
        /// Write the log to local file system.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(string message)
        {
            message = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " " + message;
            message += Environment.NewLine;

            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, DateTime.Now.ToString("yyyy-MM-dd") + ".log");

            lock (_logLockObject)
            {
                File.AppendAllText(path, message);
            }
        }
    }
}
