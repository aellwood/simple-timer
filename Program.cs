using System;
using System.IO;
using System.Threading;

namespace timer
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = GetFileName(args.Length > 0 ? args[0] : null);

            if (File.Exists(fileName)) 
            {
                Console.WriteLine($"{fileName} file name already exists");
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

        private static string GetFileName(string specifiedName)
        {
            if (string.IsNullOrWhiteSpace(specifiedName)) 
            {
                var defaultFileName = $"timings-{CurrentTime}.txt";

                Console.WriteLine("No file name specified - using default file naming convention.");
                Console.WriteLine($"File name used: {defaultFileName}");

                return defaultFileName;
            }

            var specifiedFileName = $"{specifiedName}.txt";

            Console.WriteLine($"Using specified file name: {specifiedFileName}");

            return specifiedFileName;
        }

        private static void Log(StreamWriter sw) 
        {
            var currentTime = CurrentTime;

            Console.WriteLine(currentTime);

            sw.WriteLine(currentTime);
        }

        private static TimeOnly CurrentTime => TimeOnly.FromDateTime(DateTime.UtcNow);
    }
}
