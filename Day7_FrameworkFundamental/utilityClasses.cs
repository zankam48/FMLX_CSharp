using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string searchQuery = "C# programming tutorial";
        
        string encodedQuery = Uri.EscapeDataString(searchQuery);
        
        string url = "https://www.google.com/search?q=" + encodedQuery;
        
        Process.Start("chrome", url);
    }
}
