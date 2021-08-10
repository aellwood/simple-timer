using System;
using System.IO;
using System.Threading;

namespace timer
{
    class Program
    {
        private const string DefaultFileName = "timings";
        private const string fileExtension = ".txt";
        private const int DefaultThreadSleep = 60000;

        static void Main(string[] args)
        {
            var specifiedName = args.Length > 0 ? args[0] : null;
            var fileName = GetFileName(specifiedName);

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

                var specifiedSleepTime = args.Length > 1 ? args[1] : null;
                var sleepTime = GetSleepTime(specifiedSleepTime);

                while(true) 
                {
                    Thread.Sleep(sleepTime);

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

        private static int GetSleepTime(string specifiedThreadSleep) 
        {
            if (!string.IsNullOrWhiteSpace(specifiedThreadSleep))
            {
                var validTime = int.TryParse(specifiedThreadSleep, out var specifiedAsInt);
                if (validTime) 
                {
                    Console.WriteLine($"Specified thread sleep time of {specifiedAsInt}ms is valid and will be used.");
                    return specifiedAsInt;
                }
            }

            Console.WriteLine($"Thread sleep time not specified or invalid. Using default value of {DefaultThreadSleep}ms.");
            return DefaultThreadSleep;
            
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
