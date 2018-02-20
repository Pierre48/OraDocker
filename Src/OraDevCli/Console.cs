using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraDevCli
{
    static class Console
    {
        public static bool Confirm()
        {
            WriteInfoLine("Press Y to confirn or any other character to cancel");
            var result = System.Console.Read();
            return result=='y' || result == 'Y';
        }
        public static void WriteErrorLine(string message) => WritMessqge(message,ConsoleColor.Red);
        public static void WriteInfoLine(string message) => WritMessqge(message,ConsoleColor.White);
        public static void WriteSuccessLine(string message) => WritMessqge(message,ConsoleColor.Green);
        public static void WriteDebugLine(string message) => WritMessqge(message, ConsoleColor.Gray);

        private static void WritMessqge(string message, ConsoleColor color)
        {
            var initialColor = System.Console.ForegroundColor;
            try
            {
                System.Console.ForegroundColor = color;
                System.Console.WriteLine(message);
            }
            finally
            {
                System.Console.ForegroundColor = initialColor;
            }
        }

        internal static void WriteErrorLine(Exception exception)
        {
            var aggregateException = exception as AggregateException;
            if (aggregateException != null)
            {
                foreach (var children in aggregateException.InnerExceptions) WriteErrorLine(children);
            }
            else
            WriteErrorLine(exception.Message);
        }
    }
}
