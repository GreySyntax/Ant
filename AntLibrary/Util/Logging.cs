using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AntLibrary.Util
{
    class Logging
    {
        private static object _writeLock = new object();

        public enum ELogLevel
        {
            DEBUG,
            INFO,
            WARNING,
            ERROR
        }

        public static void WriteRaw(string message)
        {
            lock (_writeLock)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(message);
            }
        }

        public static void LogEvent(string system, string message, ELogLevel level)
        {
            message = message.Replace("\r", "\r\n");

            lock (_writeLock)
            {
                //[TIME] [SYSTEM] MESSAGE
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[{0}] [", DateTime.Now.ToString());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(system);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("] ");

                switch (level)
                {
                    case ELogLevel.DEBUG:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case ELogLevel.INFO:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case ELogLevel.WARNING:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                    case ELogLevel.ERROR:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                }

                Console.WriteLine(message);
            }
        }
    }
}
