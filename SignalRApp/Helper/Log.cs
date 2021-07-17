using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRApp.Helper
{
    public class Log
    {
        public static void Message(string user, string message)
        {
            using StreamWriter sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "logMessage\\" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + ".txt");
            sw.WriteLine("[" + DateTime.Now + "] - " + user + ": " + message);
            sw.Close();
        }
        public static void Chunk(string file) //byte[]
        {
            using StreamWriter sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory + "logChunk\\" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + ".chk");
            sw.WriteLine(file);
            sw.Close();
        }
    }
}
