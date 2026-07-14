using System.ComponentModel;
using System.Collections.Concurrent;

public class Frontier
{
   private Queue<Uri> _queue = new();
   HashSet<Uri> seenUrls = new();

   public void Add(Uri url)
   {
      bool wasAddedToSeenUrls = seenUrls.Add(url);
      if (wasAddedToSeenUrls)
      {
         _queue.Enqueue(url);
         Console.WriteLine($"{url}, added to queue");
      }
      else
      {
         Console.WriteLine($"{url}, Not added to queue");
         return;
      }
   }

   public Uri GetNext()
   {
      if (IsQueEmpty())
      {
         Console.WriteLine("Nothing Left in frontier");
         
      }
      return _queue.Dequeue();
   }

   public bool IsQueEmpty()
   {
      if (_queue.Count == 0)
      {
         return true;
      }
      else
      {
         return false;
      }
   }

   public void PrintQueue()
   {
      foreach (Uri url in _queue)
      {
         Console.WriteLine(url);
      }
   }

}