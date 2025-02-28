using System;

public delegate int Comparison<T>(T value1, T value2);

public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }
}

public class SortingPerson
{
    public void Sort(Person[] people, Comparison<Person> comparison)
    {
        for (int i=0; i<people.Length; i++)
        {
            for (int j=i+1; j<people.Length; j++)
            {
                if (comparison(people[i], people[j]) > 0)
                {
                    Person temp = people[i];
                    people[i] = people[j];
                    people[j] = temp;
                }
            }
        }
    }
}

class Program
{
    static int CompareName(Person x, Person y)
    {
        return x.Name.CompareTo(y.Name);
    }
    
    static void Main(string[] args)
    {
        Person[] people = {
            new Person { Name = "Budi", Age = 30 },
            new Person { Name = "Dika", Age = 25 },
            new Person { Name = "Lisa", Age = 28 }
        };

        SortingPerson sortingPerson = new SortingPerson();
        sortingPerson.Sort(people, (p1, p2) => p1.Age - p2.Age);
        foreach (Person p in people)
        {
            Console.WriteLine($"{p.Name} - {p.Age}");
        }

        Console.WriteLine("\n");
        sortingPerson.Sort(people, CompareName);
        foreach (Person p in people)
        {
            Console.WriteLine($"{p.Name} - {p.Age}");
        }
    }
}