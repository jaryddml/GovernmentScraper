using HtmlAgilityPack;

public class LinkParser
{
    public List<Uri> LinkGetter(string html, Uri sourceUrl)
    {
        List<Uri> linkList = new List<Uri>();
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
                    Console.WriteLine($"{href} is empty");
                    continue;
                }
                // resolver is smart and can put all links through without breaking them
                Uri resolvedUri = new Uri(sourceUrl, href);
                //Console.WriteLine($"tryCreate: {resolvedUri}");

                if (resolvedUri.Scheme == Uri.UriSchemeHttp || resolvedUri.Scheme == Uri.UriSchemeHttps)
                {
                    //send to isDotGovWebsite();
                    Console.WriteLine($"I am a valid link {resolvedUri}");
                    linkList.Add(resolvedUri);
                }
                else
                {
                    Console.WriteLine($"i am not a valid link {resolvedUri}");
                }
            }
        }

        return linkList;
    }
}