using HtmlAgilityPack;

public class PageClassifier
{
    public Dictionary<String, int> Keywords = new Dictionary<string, int>
    {
        // Strong official titles
        ["mayor"] = 20,
        ["governor"] = 20,
        ["commissioner"] = 18,
        ["supervisor"] = 18,
        ["councilmember"] = 18,
        ["councilman"] = 18,
        ["councilwoman"] = 18,
        ["alderman"] = 18,
        ["delegate"] = 18,
        ["senator"] = 18,
        ["representative"] = 18,
        ["trustee"] = 17,
        ["sheriff"] = 17,
        ["treasurer"] = 17,
        ["assessor"] = 17,
        ["clerk"] = 17,
        ["prosecutor"] = 17,
        ["attorney"] = 17,
        ["administrator"] = 17,
        ["manager"] = 16,
        ["director"] = 16,
        ["chief"] = 16,
        ["secretary"] = 16,

        // Government bodies
        ["council"] = 15,
        ["board"] = 15,
        ["commission"] = 15,
        ["committee"] = 12,

        // Office-related
        ["office"] = 12,
        ["staff"] = 12,
        ["leadership"] = 12,
        ["directory"] = 12,
        ["contact"] = 10,
        ["employee"] = 10,
        ["official"] = 12,

        // Agencies / departments
        ["agency"] = 10,
        ["department"] = 10,
        ["division"] = 9,
        ["bureau"] = 9,

        // Jurisdictions
        ["city"] = 8,
        ["county"] = 8,
        ["state"] = 8,
        ["town"] = 8,
        ["municipal"] = 8,
        ["parish"] = 8,
        ["borough"] = 8,
        ["government"] = 5
    };

    public int GetScoreForPage(string html, Uri pageUrl)
    {
        HtmlDocument document = new();
        document.LoadHtml(html);
        int score = 0;

        string urlText = pageUrl.AbsolutePath.ToLowerInvariant();
        HtmlNode? titleNode = document.DocumentNode.SelectSingleNode("//title");
        string titleText = titleNode?.InnerText.ToLowerInvariant() ?? "";
        HtmlNode? headingNode = document.DocumentNode.SelectSingleNode("//h1");
        string headingNodeLower = headingNode?.InnerText.ToLowerInvariant() ?? "";
        HtmlNode? bodyNode = document.DocumentNode.SelectSingleNode("//body");
        string bodyNodeLower = bodyNode?.InnerText.ToLowerInvariant() ?? "";
        
        foreach (KeyValuePair<string, int> keyword in Keywords)
        {
            if (urlText.Contains(keyword.Key))
            {
                score += keyword.Value;
            }

            if (titleText.Contains(keyword.Key))
            {
                score += keyword.Value;
            }

            if (headingNodeLower.Contains(keyword.Key))
            {
                score += keyword.Value;
            }

            if (bodyNodeLower.Contains(keyword.Key))
            {
                int newValue = keyword.Value / 2;
                score += newValue;
            }
        }
        return score;
    }

    public int GetScoreForUrl(Uri pageUrl)
    {
        int score = 0;
        string url = pageUrl.OriginalString;

        foreach (KeyValuePair<string, int> keyword in Keywords)
        {
            if (url.Contains(keyword.Key))
            {
                score += keyword.Value;
            }
        }

        return score;
    }
}