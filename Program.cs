using System;
using System.IO;
using System.Threading;

namespace timer
{
    class Program
    {
        private const string fileName = "timings.txt";

        static void Main(string[] args)
        {
            if (File.Exists(fileName)) 
            {
                Console.WriteLine($"{fileName} already exists");
            }
            else 
            {
                using (StreamWriter sw = File.CreateText(fileName))
                {
                    Log(sw);
                }	

                while(true) 
                {
                    Thread.Sleep(60000);

                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        Log(sw);
                    }
                }
            }
        }

        private static void Log(StreamWriter sw) 
        {
            sw.WriteLine(TimeOnly.FromDateTime(DateTime.UtcNow));
        }
    }
}
