using System;
using System.Collections;

class Program
{
    static void Main()
    {
        BitArray bits = new BitArray(4);
        bits[0] = true;
        bits[1] = false;
        bits[2] = true;
        bits[3] = false;
        BitArray bits2 = new BitArray(new bool[] {true, false, false, true});

        BitArray AndResult = BitwiseOperator(bits, bits2, (a, b) => a & b);
        BitArray OrResult = BitwiseOperator(bits, bits2, (a, b) => a | b);

        for (int i = 0; i <AndResult.Length; i++)
        {
            Console.Write(AndResult[i]);
            Console.WriteLine();
        }

        for (int i=0; i<OrResult.Length; i++)
        {
            Console.Write(OrResult[i]);
            Console.WriteLine();
        }
    }

    static BitArray BitwiseOperator(BitArray bits1, BitArray bits2, Func<bool,bool,bool> operation)
    {
        BitArray result = new BitArray(bits1.Length);

        for (int i = 0; i < bits1.Length; i++)
        {
            result[i] = operation(bits1[i], bits2[i]);
        }
        return result;
    }
}