using System;
using System.IO;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        string path = "C:\Users\Batch 12\Documents\Zankam\FMLX_CSharp\myfile.txt";
        FileStream fileStream = new FileStream(path, FileMode.Append);
        byte[] bdata = Encoding.Default.GetBytes("halo dunia!");
        fileStream.Write(bdata, 0, bdata.Length);
        fileStream.Close();
    }
}