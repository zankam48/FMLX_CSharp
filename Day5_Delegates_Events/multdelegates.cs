using System;

// trying to learn about pass by reference as well 
public class DataProcessor
{
    public delegate void DataProcessing(ref string data);

    static void reverseData(ref string data)
    {
        char[] charArray = data.ToCharArray();
        Array.Reverse(charArray);
        data = new string(charArray);
    }

    static void addCharAtFirstIdx(ref string data, char c)
    {
        data = c + data.Substring(1);
    }

    static void addCharAtIdx(ref string data, char c, int idx)
    {
        data = data.Substring(0, idx) + c + data.Substring(idx);
    }

    public void ProcessData(ref string data)
    {
        DataProcessing dataHandlers = reverseData;
        DataProcessing addFirst = (ref string s) => addCharAtFirstIdx(ref s, 'D');
        DataProcessing addAtIdx = (ref string s) => addCharAtIdx(ref s, 'X', 3);

        dataHandlers += addFirst;
        dataHandlers += addAtIdx;
        dataHandlers(ref data);
    }
}

class Program
{
    static void Main()
    {

        string data = "hello world";

        DataProcessor processor = new DataProcessor();
        processor.ProcessData(ref data);

        Console.WriteLine("Final Data: " + data);
    }
}