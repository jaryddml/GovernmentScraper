using System;
internal class Program
{
    static async Task Main()
    {
        Frontier frontier = new Frontier(); 
        string filePath = "seedurls.txt";
        foreach (string url in File.ReadLines(filePath))
        {
            if (Uri.TryCreate(url, UriKind.Absolute , out Uri uri))
            {
                frontier.Add(uri);
            }
        }

        PageFetcher pageFetcher = new PageFetcher();
        LinkParser linkParser = new LinkParser();
        DotGovFitler dotGovFilter = new DotGovFitler();
        List<Uri> filteredLinks = new List<Uri>();


        do
        {
            Uri currentWebPage = frontier.GetNext();
            Console.WriteLine($"CURRENT WEB PAGE {currentWebPage}");
            string result = await pageFetcher.FetchPage(currentWebPage);
            if (result == "err")
            {
                continue;
            }

            List<Uri> scrapedLinks = linkParser.LinkGetter(result, currentWebPage);
            Console.WriteLine($"Scraped links: {scrapedLinks.Count}");
            Console.WriteLine($"Scraped links: {scrapedLinks.Count}");
            Console.WriteLine($"Scraped links: {scrapedLinks.Count}");
            Console.WriteLine($"Scraped links: {scrapedLinks.Count}");

            filteredLinks = dotGovFilter.IsDotGov(scrapedLinks);
            Console.WriteLine($"Filtered links: {filteredLinks.Count}");
            Console.WriteLine($"Filtered links: {filteredLinks.Count}");
            Console.WriteLine($"Filtered links: {filteredLinks.Count}");
            Console.WriteLine($"Filtered links: {filteredLinks.Count}");
            
            
            foreach (Uri link in filteredLinks)
            {
                frontier.Add(link);
            }
        }while (frontier.IsQueEmpty() != true);
    }
}

// seeded urls -> Frontier -> page fetcher -> link parser -> link filter -> check if reseen link -> frontier queue
//                                  |
//                                  V
//                                  official information scraper. -> store data -> varify if accurate -> push to front end


// need to implement the rest of frontier. Add more robust error handling. and scaffold everything together.