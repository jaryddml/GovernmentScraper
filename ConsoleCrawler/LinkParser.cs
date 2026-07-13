using HtmlAgilityPack;

public class LinkParser
{
    public void LinkGetter(string html)
    {
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(html);
        var anchorNodes = doc.DocumentNode.SelectNodes("//a[@href]");
        if (anchorNodes != null)
        {
            foreach (var node in anchorNodes)
            {
                string href = node.GetAttributeValue("href", string.Empty);
                Console.WriteLine(href);
            }
        }
    }
}