// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
Console.WriteLine("Hello, World!");

// while
Console.WriteLine("continue or break" );
string choice = Console.ReadLine();

while (choice == "continue")
{
    Console.WriteLine("Hello, World!");
    choice = Console.ReadLine();
}


// arr
int[] myArr = [1,2,3,4,5];

foreach (int i in myArr)
{
    Console.WriteLine(i);
}
