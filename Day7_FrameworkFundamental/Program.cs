﻿using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        string searchQuery = "C# programming tutorial";
        
        string encodedQuery = Uri.EscapeDataString(searchQuery);
        string chromePath = @"C:\Program Files\Google\Chrome\Application\chrome.exe";
        string url = "https://www.google.com/search?q=" + encodedQuery;
        
        Process.Start(chromePath, url);
    }
}
