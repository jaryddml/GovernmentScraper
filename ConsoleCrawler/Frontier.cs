using System.ComponentModel;
using System.Collections.Concurrent;

public class Frontier
{
   private readonly DbOperation dbOperation;

   public Frontier(DbOperation dbOperation)
   {
      this.dbOperation = dbOperation;
   }
   public void Add(Uri url, int priority)
   {
      Uri normalizedUrl = NormalizeUrl(url);
      string urlString = normalizedUrl.OriginalString;

      bool isAdded = dbOperation.TryAddUrl(urlString, priority);
   }

   public CrawlUrl? GetNext()
   {
      return dbOperation.GetNextUrl();
   }
   

   public Uri NormalizeUrl(Uri url)
   {
      UriBuilder builder =  new UriBuilder(url);
      builder.Scheme = Uri.UriSchemeHttps;

      builder.Host = builder.Host.ToLowerInvariant();

      if (builder.Scheme == Uri.UriSchemeHttps && builder.Port == 443)
      {
         builder.Port = -1;
      }

      string path = builder.Path;
      if (path.Length > 1 && path.EndsWith('/'))
      {
         path = path.TrimEnd('/');
      }
      builder.Path = path;

      builder.Fragment = string.Empty;

      builder.Query = string.Empty;

      return builder.Uri;

   }
}