using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

public class CrawlerDatabase : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<CrawlUrl>()
            .HasIndex(c => c.Url)
            .IsUnique();
        modelBuilder.Entity<CrawlUrl>()
            .Property(c => c.Status)
            .HasConversion<string>();
    }
    public DbSet<CrawlUrl> CrawlUrls { get; set; }
    
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=crawler.db");
    }
}

public class CrawlUrl
{
    public int Id { get; set; }
    public required string Url { get; set; }

    public int Attempts { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public CrawlStatus Status { get; set; } = CrawlStatus.Queued;

    public enum CrawlStatus
    {
        Queued,
        Processing,
        Visited,
        Failed
    }
}

public class DbOperation
{
    private readonly CrawlerDatabase database;

    public DbOperation(CrawlerDatabase database)
    {
        this.database = database;
    }
    
    public bool TryAddUrl(string url)
    {
       bool alreadyExists =  database.CrawlUrls.Any(crawlUrl => crawlUrl.Url == url);

       if (alreadyExists)
       {
           return false;
       }

       CrawlUrl crawlUrl = new()
       {
           Url = url
       };
       database.CrawlUrls.Add(crawlUrl);
       database.SaveChanges();
       return true;
    }

    public CrawlUrl? GetNextUrl()
    {
        return database.CrawlUrls
            .Where(crawlUrl => crawlUrl.Status == CrawlUrl.CrawlStatus.Queued)
            .OrderBy(crawlUrl => crawlUrl.CreatedAt)
            .FirstOrDefault();
    }

    public bool isQueueEmpty()
    {
        var status = database.CrawlUrls.Where(crawlUrl => crawlUrl.Status == CrawlUrl.CrawlStatus.Queued);
        if (status == null)
        {
            return false;
        }

        return true;
    }

    public void MarkProcessing(CrawlUrl crawlUrl)
    {
        crawlUrl.Status = CrawlUrl.CrawlStatus.Processing;
        database.SaveChanges();
    }
    public void MarkVisited(CrawlUrl crawlUrl)
    {
        crawlUrl.Status = CrawlUrl.CrawlStatus.Visited;
        database.SaveChanges();
    }

    public void MarkFailed(CrawlUrl crawlUrl)
    {
        crawlUrl.Status = CrawlUrl.CrawlStatus.Failed;
        crawlUrl.Attempts++;
        database.SaveChanges();
    }
}

