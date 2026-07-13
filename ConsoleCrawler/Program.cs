using System;
internal class Program
{
    static void Main()
    {
        Frontier frontier = new Frontier(); 
        string filePath = "seedurls.txt";
        foreach (string url in File.ReadLines(filePath))
        {
            Console.WriteLine(url);
            if (Uri.TryCreate(url, UriKind.RelativeOrAbsolute , out var uri))
            {
                frontier.Add(uri);
            }

        }
        frontier.PrintQueue();
        
    }
}