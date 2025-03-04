using System;
using System.Security.Cryptography;

// RandomNumberGenerator RNG = RandomNumberGenerator.Create();
// byte[] bytes = new byte[32];
// RNG.GetBytes(bytes);

using System;
using System.Security.Cryptography;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("ordinary random:");
        OrdinaryRandom();
        
        Console.WriteLine("\ncrypto random:");
        CryptoRandom();
    }

    static void OrdinaryRandom()
    {
        Random random = new Random();
        
        for (int i = 0; i < 5; i++)
        {
            int randomValue = random.Next();
            Console.WriteLine($"Random Value {i + 1}: {randomValue}");
        }
    }
    static void CryptoRandom()
    {
        RandomNumberGenerator rng = RandomNumberGenerator.Create();

        for (int i = 0; i < 5; i++)
        {
            byte[] randomBytes = new byte[4]; 
            rng.GetBytes(randomBytes);
            
            int randomValue = BitConverter.ToInt32(randomBytes, 0);
            Console.WriteLine($"random Value {i + 1}: {randomValue}");
        }
    }
}
