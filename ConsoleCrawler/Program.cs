using System;
internal class Program
{
    static async Task Main()
    {
        Frontier frontier = new Frontier(); 
        string filePath = "seedurls.txt";
        foreach (string url in File.ReadLines(filePath))
        {
            //Console.WriteLine(url);
            if (Uri.TryCreate(url, UriKind.Absolute , out var uri))
            {
                frontier.Add(uri);
            }
        }

        string testUrl = "https://virginia.gov";
        PageFetcher pageFetcher = new PageFetcher();
        string result = await pageFetcher.FetchPage(testUrl);

        LinkParser linkParser = new LinkParser();
        linkParser.LinkGetter(result);
    }
}