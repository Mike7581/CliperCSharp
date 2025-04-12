namespace CliperBot
{
    class Program
    {
        [STAThread]
        private static async Task Main()
        {
            Console.WriteLine("Welcome to CliperBot!!");
            
            var telegramBot = new TelegramBotWrapper();
            await telegramBot.Start();
            
            var path = FormController.Init();
            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("Nothing path selected. Exiting...");
                telegramBot.Stop();
                return;
            }
            Console.WriteLine($"Selected path: {path}");
            
            using var watcher = new FileSystemWatcher(path);

            watcher.NotifyFilter = NotifyFilters.Attributes
                                   | NotifyFilters.CreationTime
                                   | NotifyFilters.DirectoryName
                                   | NotifyFilters.FileName
                                   | NotifyFilters.LastAccess
                                   | NotifyFilters.LastWrite
                                   | NotifyFilters.Security
                                   | NotifyFilters.Size;

            watcher.Changed += OnChanged;
            watcher.Created += OnCreated;

            watcher.Filter = "*.mp4";
            watcher.EnableRaisingEvents = true;
            
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
            
            telegramBot.Stop();
            telegramBot.Dispose();
        }

        private static void OnChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType != WatcherChangeTypes.Changed)
            {
                return;
            }
            Console.WriteLine($"Changed: {e.FullPath}");
        }
        
        private static void OnCreated(object sender, FileSystemEventArgs e)
        {
            var value = $"Created: {e.FullPath}";
            Console.WriteLine(value);
        }
    }
}