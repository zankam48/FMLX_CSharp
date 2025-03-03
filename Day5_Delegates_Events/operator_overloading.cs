
// public struct Note
// {
//     int value;
//     public Note(int semitonesFromA)
//     {
//         value = semitonesFromA;
//     }

//     public static Note operator + (Note x, int semitones)
//     {
//         return new Note(x.value + semitones);
//     }


// }

// class Program
// {
//     static void Main(string[] args)
//     {
//         Note B = new Note(2);
//         Note C = B + 2;
//     }
// }

using System;
public class Point
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public static Point operator + (Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }
    
    public static Point operator - (Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y);
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Point p1 = new Point(3, 5);
        Point p2 = new Point(2, 1);

        Point p3 = p1 + p2;
        Console.WriteLine(p3);
    }
}