using HtmlAgilityPack;

public class LinkParser
{
    public void LinkGetter(string html, string sourceUrl)
    {
        Uri baseUri = new Uri(sourceUrl);
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);
        var anchorNodes = doc.DocumentNode.SelectNodes("//a[@href]");
        if (anchorNodes != null)
        {
            foreach (var node in anchorNodes)
            {
                string href = node.GetAttributeValue("href", string.Empty);
                
                //Console.WriteLine($"href = '{href}'");
                if (string.IsNullOrWhiteSpace(href))
                {
                    continue;
                }
                Uri resolvedUri = new Uri(baseUri, href);
                //Console.WriteLine($"tryCreate: {resolvedUri}");

                if (resolvedUri.Scheme == "https" || resolvedUri.Scheme == "http")
                {
                    //send to isDotGovWebsite();
                    Console.WriteLine($"I am a valid link {resolvedUri}");
                }
                else
                {
                    Console.WriteLine($"i am not a valid link {resolvedUri}");
                }
            }
        }
    }
}