// exercise
int n = Convert.ToInt32(Console.ReadLine());

for (int i = 1; i <= n; i++)
{
    if (i%15 == 0){
        Console.WriteLine("FooBar");
    } else if (i%5 == 0){
        Console.WriteLine("Bar");
    } else if (i%3 == 0){
        Console.WriteLine("Foo");
    } else {
        Console.WriteLine(i);
    }
}
