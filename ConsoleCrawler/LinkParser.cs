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
                bool isResolved = Uri.TryCreate(sourceUrl, href, out Uri resolvedUri);
                if (!isResolved) continue;

                if (resolvedUri.Scheme == Uri.UriSchemeHttp || resolvedUri.Scheme == Uri.UriSchemeHttps)
                {
                    linkList.Add(resolvedUri);
                }
            }
        }
        return linkList;
    }
}