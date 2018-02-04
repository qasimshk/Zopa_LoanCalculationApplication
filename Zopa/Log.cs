using System;
using log4net;

namespace Zopa
{
    public class Log
    {
        private static readonly ILog Logs =
            LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod()
                .DeclaringType);

        public static void Info(string message) => Logs.Info(message);

        public static void Fatel(string message) => Logs.Fatal(message);

        public static void Error(string message) => Logs.Error(message);
    }
}
