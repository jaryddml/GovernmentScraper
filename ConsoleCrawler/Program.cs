using System;
internal class Program
{
    static async Task Main()
    {
        Frontier frontier = new Frontier(); 
        string filePath = "seedurls.txt";
        foreach (string url in File.ReadLines(filePath))
        {
            if (Uri.TryCreate(url, UriKind.Absolute , out var uri))
            {
                frontier.Add(uri);
            }
        }
        
        // to test the functionality of other systems until i implement frontier logic
        string testUrl = "https://virginia.gov";
        PageFetcher pageFetcher = new PageFetcher();
        string result = await pageFetcher.FetchPage(testUrl);

        LinkParser linkParser = new LinkParser();
        linkParser.LinkGetter(result, testUrl);
        
        //test for DotgovFilter
        DotGovFitler dotGovFilter = new DotGovFitler();
        dotGovFilter.IsDotGov(testUrl);
        
        //test for .add frontier

        Uri testUri = new Uri("https://ca.gov");
        frontier.Add(testUri);
        frontier.Add(testUri);
        frontier.PrintQueue();
    }
}

// seeded urls -> Frontier -> page fetcher -> link parser -> link filter -> check if reseen link -> frontier
//                                  |
//                                  V
//                                  official information scraper. -> store data -> varify if accurate -> push to front end


// need to implement a "check if its reseen link", and "official information scraper".
// then scaffold everything together.    