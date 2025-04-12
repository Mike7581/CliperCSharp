namespace CliperBot;

using System;

public class FormController
{
    public static String Init()
    {
        Console.WriteLine("Initializing FormController...");
        
        var fbd = new FolderBrowserDialog();
        return fbd.ShowDialog() == DialogResult.OK ? fbd.SelectedPath : "";
    }
}