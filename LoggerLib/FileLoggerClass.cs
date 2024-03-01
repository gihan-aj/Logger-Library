namespace LoggerLib
{
    public class FileLogger : IFileLogger
    {
        //private string filepath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";
        private string path = Path.GetFullPath(Directory.GetCurrentDirectory());

        public void SetFilePath(string path)
        {
            this.path = path;
        }

        public void LogInfo(string msg)
        {
            LogToTXT("[INFO]", msg);
        }

        public void LogWarning(string msg)
        {
            LogToTXT("[WARNING]", msg);
        }

        public void LogError(string msg)
        {
            LogToTXT("[ERROR]", msg);
        }

        private void LogToTXT(string level, string msg)
        {
            DateTime timeStamp = DateTime.Now;
            string year = timeStamp.ToString("yyyy");
            string month = timeStamp.ToString("MM");
            string date = timeStamp.ToString("yyyy-MM-dd");

            string logText = $"{timeStamp.ToString("yyyy-MM-dd HH:mm:ss")} -> {level.PadRight(8)}: {msg}";

            string loggingPath = path + $"\\Logs\\{year}\\{month}";

            // Create irectory if not exist
            if (!Directory.Exists(loggingPath))
            {
                Directory.CreateDirectory(loggingPath);
            }

            string filePath = Path.Combine(loggingPath, $"LOG-{date}.txt");

            if (!string.IsNullOrEmpty(logText))
            {
                // Create if file does not exists
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = new StreamWriter(filePath))
                    {
                        sw.WriteLine(logText);
                    }

                    // Logginfg errors in seperate .txt
                    if (level == "[ERROR]")
                    {
                        string errorFilePath = Path.Combine(loggingPath, $"LOG-{level}-{date}.txt");

                        using (StreamWriter sw = new StreamWriter(errorFilePath))
                        {
                            sw.WriteLine(logText);
                        }
                    }

                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filePath))
                    {
                        sw.WriteLine(logText);
                    }

                    // Logginfg errors in seperate .txt
                    if (level == "[ERROR]")
                    {
                        string errorFilePath = Path.Combine(loggingPath, $"LOG-{level}-{date}.txt");

                        using (StreamWriter sw = File.AppendText(errorFilePath))
                        {
                            sw.WriteLine(logText);
                        }
                    }
                }


            }
        }
    }
}
