using System;
public class DotGovFitler
{
    public string AllowedTLD = "gov";

    public List<Uri> IsDotGov(List<Uri> uris)
    {
        List<Uri> dotGovUris = new List<Uri>();
        foreach (Uri uri in uris)
        {
            string host = uri.Host;
            string[] parts = host.Split('.');
            if (parts[parts.Length - 1] == AllowedTLD)
            {
                dotGovUris.Add(uri);
            }
            else
            {
                continue;
            }
        }

        return dotGovUris;
    }
}