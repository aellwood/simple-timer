using System;
using System.IO;
using System.Threading;

namespace timer
{
    class Program
    {
        private const string DefaultFileName = "timings";
        private const string FileExtension = ".txt";
        private const int DefaultThreadSleep = 60000;

        static void Main(string[] args)
        {
            var fileNameArg = args.Length > 0 ? args[0] : null;
            var fileName = GetFileName(fileNameArg);

            if (File.Exists(fileName)) 
            {
                Console.WriteLine($"{fileName} file name already exists");
            }
            else 
            {
                var sleepTimeArg = args.Length > 1 ? args[1] : null;
                var sleepTime = GetSleepTime(sleepTimeArg);

                Console.WriteLine($"{Environment.NewLine}Starting logging.{Environment.NewLine}");

                using (StreamWriter sw = File.CreateText(fileName))
                {
                    Log(sw);
                }	

                while (true) 
                {
                    Thread.Sleep(sleepTime);

                    using (StreamWriter sw = File.AppendText(fileName))
                    {
                        Log(sw);
                    }
                }
            }
        }

        private static string GetFileName(string fileNameArg)
        {
            if (string.IsNullOrWhiteSpace(fileNameArg) || 
                fileNameArg.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0 || 
                fileNameArg == "default") 
            {
                var defaultFileName = $"timings-{CurrentTime.ToString().Replace(":", ".")}.txt";

                Console.WriteLine("No file name specified, invalid file name or 'default' specified - using default file naming convention.");
                Console.WriteLine($"File name used: {defaultFileName}");

                return defaultFileName;
            }

            var customFileName = $"{fileNameArg}.txt";

            Console.WriteLine($"Using specified file name: {customFileName}");

            return customFileName;
        }

        private static int GetSleepTime(string sleepArg) 
        {
            if (!string.IsNullOrWhiteSpace(sleepArg))
            {
                var isValidDuration = int.TryParse(sleepArg, out var sleep);

                if (isValidDuration) 
                {
                    Console.WriteLine($"Specified thread sleep time of {sleep}ms is valid and will be used.");

                    return sleep;
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
