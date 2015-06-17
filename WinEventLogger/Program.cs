using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinEventLogger
{
    class Program
    {
        static void Main(string[] args)
        {

            string source;
            string message;
            ushort eventid;

            if (args.Length != 3) {
                System.Console.WriteLine("Expected 3 arguments: source message eventid");
                System.Environment.Exit(1);
            }
            source = args[0];
            message = args[1];
            eventid = 0;
            try
            {
                eventid = Convert.ToUInt16(args[2]);
            }
            catch (System.FormatException) {  
                System.Console.WriteLine("Third Argument (EventID) should be an integer between 0 and 65535, using 0");
            }
            catch (System.OverflowException)
            {
                System.Console.WriteLine("Third Argument (EventID) should be an integer between 0 and 65535, using 0");
            }

            System.Diagnostics.EventLog appLog = new System.Diagnostics.EventLog();
            appLog.Source = source;
            try
            {
                appLog.WriteEntry(message, System.Diagnostics.EventLogEntryType.Information, eventid);
            }
            catch (System.Security.SecurityException)
            {
                System.Console.WriteLine("Must be ran as administrator to write to event logs, try running in an elevated command prompt");
                System.Environment.Exit(1);
            }
        }
    }
}
