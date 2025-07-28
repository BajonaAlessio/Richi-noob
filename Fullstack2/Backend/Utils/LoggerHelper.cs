

namespace Backend.Utils
{
    //questa classe statica si occupa di tracciare le operazioni di log
    public static class LoggerHelper
    {
        public static void Log(string message)
        {
            //creare una stringa - che vedo in console: timestamp
            string logLine = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}";
            // scrive log sul console
            Console.WriteLine(logLine);
            //scrive sul file di log
            File.AppendAllText("log.txt", logLine + Environment.NewLine);
        } 
    }
}