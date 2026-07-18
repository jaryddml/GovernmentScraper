using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

internal class Program
{
    static async Task Main()
    {
        // Constructs all needed classes
        PageFetcher pageFetcher = new PageFetcher();
        LinkParser linkParser = new LinkParser();
        DotGovFitler dotGovFilter = new DotGovFitler();
        using CrawlerDatabase database = new CrawlerDatabase();
        database.Database.EnsureCreated();
        DbOperation dbOperation = new DbOperation(database);
        Frontier frontier = new Frontier(dbOperation);
        TimeSpan requestedDelay = TimeSpan.FromSeconds(2);
        PageClassifier pageClassifier = new PageClassifier();
        
        // Inserts Seeded URLS
        string filePath = "seedurls.txt";
        foreach (string url in File.ReadLines(filePath))
        {
            if (Uri.TryCreate(url, UriKind.Absolute , out Uri uri))
            {
                frontier.Add(uri, 0);
            }
        }

        do
        {
            CrawlUrl currentWebPage = frontier.GetNext();
            if (currentWebPage == null)
            {
                Console.WriteLine("Queue empty");
                break;
            }
            
            dbOperation.MarkProcessing(currentWebPage);

            if (!Uri.TryCreate(currentWebPage.Url, UriKind.Absolute, out Uri currentWebPageUri))
            {
                Console.WriteLine("could not make uri");
                break;
            }
            Console.WriteLine($"CURRENT WEB PAGE {currentWebPageUri}");
            string result = await pageFetcher.FetchPage(currentWebPageUri);
            int pageRating = pageClassifier.GetScoreForPage(result, currentWebPageUri);
            dbOperation.SetWebpageQuality(currentWebPage, pageRating);

            await Task.Delay(requestedDelay);
            
            if (result == "err")
            {
                dbOperation.MarkFailed(currentWebPage);
                continue;
            }

            List<Uri> scrapedLinks = linkParser.LinkGetter(result, currentWebPageUri);
            List<Uri> filteredLinks = new List<Uri>();

            filteredLinks = dotGovFilter.IsDotGov(scrapedLinks);

            foreach (Uri link in filteredLinks)
            {
                int priority = pageClassifier.GetScoreForUrl(link);
                frontier.Add(link, priority);
            }
            dbOperation.MarkVisited(currentWebPage);
        } while (dbOperation.isQueueEmpty());
    }
}

// seeded urls -> Frontier -> page fetcher -> link parser -> link filter -> check if reseen link -> frontier queue
//                                  |
//                                  V
//                                  official information scraper. -> store data -> varify if accurate -> push to front end


// need to implement the rest of frontier. Add more robust error handling. and scaffold everything together.