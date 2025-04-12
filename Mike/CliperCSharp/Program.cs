using System;
using System.IO;
using System.Windows.Forms;

internal static class CliperCSharp
{
    [STAThread]
    static void Main()
    {
        Console.WriteLine("Rodando");

        using FolderBrowserDialog dialog = new FolderBrowserDialog();
        string folderPath = string.Empty;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            folderPath = dialog.SelectedPath;
        }

        Console.WriteLine(folderPath);

        using var watcher = new FileSystemWatcher(folderPath);
        watcher.Filter = "*.*";
        watcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite | NotifyFilters.FileName;
        watcher.Created += OnCreated;
        watcher.Renamed += OnRenamed;
        watcher.EnableRaisingEvents = true;
        Console.WriteLine("Monitorando, aperte Q para sair.");
        while (Console.Read() != 'q') ;
    }
    private static void OnCreated(object sender, FileSystemEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine($"Video detectado!: {e.FullPath}");
    }
    private static void OnRenamed(object sender, RenamedEventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        Console.WriteLine($"Video detectado!: {e.FullPath}");
    }
}