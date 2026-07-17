using System.Net.Http;
using System.Threading.Tasks;

public class PageFetcher
{
    HttpClient client = new HttpClient();
    

    public async Task<string> FetchPage(Uri url)
    {
        client.DefaultRequestHeaders.TryAddWithoutValidation(
            "User-Agent", 
            "GovCrawler/0.1 (Github: Currently Closed Repo; Contact: GovernmentCrawler@protonmail.com; Purpose: Database of governing officials)"
        );
        try
        {
            string responseBody = await client.GetStringAsync(url);

            //Console.WriteLine(responseBody);
            return responseBody;
        }
        catch (HttpRequestException err)
        {
            Console.WriteLine($"Request failed: {err.Message} on {url}");
            return "err";
        }
    }
}