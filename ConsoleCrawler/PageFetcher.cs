using System.Net.Http;
using System.Threading.Tasks;

public class PageFetcher
{
    HttpClient Client = new HttpClient();

    public async Task<string> FetchPage(string url)
    {
        try
        {
            string responseBody = await Client.GetStringAsync(url);

            //Console.WriteLine(responseBody);
            return responseBody;
        }
        catch (HttpRequestException err)
        {
            Console.WriteLine($"Request failed: {err.Message}");
            return "err";
        }
    }
}