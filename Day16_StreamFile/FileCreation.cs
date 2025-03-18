using System;
using System.IO;
namespace FileHandlingDemo;

class Program
{
    static void Main(string[] args)
    {
        string path = "C:\Users\Batch 12\Documents\Zankam\FMLX_CSharp\myfile.txt";
        FileStream fileStream = new FileStream(path, FileMode.Create);
        fileStream.Close();
        Console.WriteLine("File created!");
    }
}