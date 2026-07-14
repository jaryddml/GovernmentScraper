using System;
public class DotGovFitler
{
    public string AllowedTLD = ".gov";

    public void IsDotGov(string url)
    {
        if (url.Contains(AllowedTLD))
        {
            Console.WriteLine($"{url} is a dotgov website");
        }
    }
}