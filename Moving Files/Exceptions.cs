using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moving_Files
{
    class Exceptions
    {
        /// <summary>
        /// [SOBRECARGADO] Registra  Exceptions en MTLOGGER.GeneralLog
        /// </summary>
        /// <param name="ex">Exception</param>
        public static void Show(Exception ex)
        {
            // Get stack trace for the exception with source file information
            var st = new StackTrace(ex, true);
            // Get the top stack frame
            var frame = st.GetFrame(0);
            // Get the line number from the stack frame
            var line = frame.GetFileLineNumber();
            var fileName = frame.GetFileName();
            var method = frame.GetMethod();

            Console.WriteLine("File: " + fileName);
            Console.WriteLine("Method: " + method);
            Console.WriteLine("Line: " + line);

            Console.WriteLine("Message: " + ex.Message);
            Console.WriteLine("StackTrace " + ex.StackTrace);
        }

        public static void Show(string message)
        {
            Console.WriteLine(message);
        }
    }
}
