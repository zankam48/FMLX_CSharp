using System;

// trying to learn about pass by reference as well 
// pake ref datanya keubah final data : DlrXow olleh
// no ref final data : hello world
public class DataProcessor
{
    public delegate void DataProcessing( string data);

    static void reverseData( string data)
    {
        char[] charArray = data.ToCharArray();
        Array.Reverse(charArray);
        data = new string(charArray);
    }

    static void addCharAtFirstIdx( string data, char c)
    {
        data = c + data.Substring(1);
    }

    static void addCharAtIdx( string data, char c, int idx)
    {
        data = data.Substring(0, idx) + c + data.Substring(idx);
    }

    public void ProcessData( string data)
    {
        DataProcessing dataHandlers = reverseData;
        DataProcessing addFirst = ( string s) => addCharAtFirstIdx( s, 'D');
        DataProcessing addAtIdx = ( string s) => addCharAtIdx( s, 'X', 3);

        dataHandlers += addFirst;
        dataHandlers += addAtIdx;
        dataHandlers( data);
    }
}

class Program
{
    static void Main()
    {

        string data = "hello world";

        DataProcessor processor = new DataProcessor();
        processor.ProcessData( data);

        Console.WriteLine("Final Data: " + data);
    }
}