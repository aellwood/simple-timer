using System;
using System.IO;
using System.Threading;

namespace timer
{
    class Program
    {
        private const string FileName = "timings";
        private const string FileExtension = "txt";

        static void Main(string[] args)
        {
            var fileName = $"{FileName}-{CurrentTime}.{FileExtension}";

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
            sw.WriteLine(CurrentTime);
        }

        private static TimeOnly CurrentTime => TimeOnly.FromDateTime(DateTime.UtcNow);
    }
}
