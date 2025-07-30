

namespace Backend.Utils
{
    public static class LoggerHelper
    {
        public static void Log(string message)
        {
            string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            Console.WriteLine(logLine);
            File.AppendAllText("Txt_Files/Log.txt", logLine + Environment.NewLine);
        } 
    }
}