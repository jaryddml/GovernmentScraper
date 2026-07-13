using System.ComponentModel;
using System.Collections.Concurrent;

public class Frontier
{
   private Queue<Uri> _queue = new();

   public void Add(Uri url)
   {
      _queue.Enqueue(url);
      Console.WriteLine($"{url}, added to queue");
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