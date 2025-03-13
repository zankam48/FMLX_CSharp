using System;

class Program
{
    static void Main()
    {
        Console.Write("Masukkan angka : ");
        int n = Convert.ToInt32(Console.ReadLine());

        for (int i = 1; i <= n; i++)
        {
            string output = "";

            if (i % 3 == 0)
                output += "foo";
            if (i % 4 == 0)
                output += "baz";
            if (i % 5 == 0)
                output += "bar";
            if (i % 7 == 0)
                output += "jazz";
            if (i % 9 == 0)
                output += "huzz";

            if (output == "")
                output = i.ToString();

            Console.Write(output);
            Console.Write(", ");
        }
    }

}
