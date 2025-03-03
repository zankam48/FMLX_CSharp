// Classnya dibawah
Magician harryPotter1 = new Magician{ Name = "Magician harry", MagicMana = 100};
Healer medic = new Healer{ Name = "Cyber Medic", HealingPower = 200};
Console.WriteLine(harryPotter1.Name);
Console.WriteLine(medic.Name);

// Upcasting -> convert subclass ref to base class ref
Character c = harryPotter1;
Console.WriteLine(c.Name);

// Downcasting -> convert base class ref to subclass ref
Magician medic2 = (Magician)c;
Console.WriteLine(medic2.MagicMana);


/**** 
Null-Coalescing (??)
****/ 
string? s1 = null; 
string s2 = s1 ?? "nothing"; 

int? i1 = null;
int i2 = i1?? 3;  
// Console.WriteLine(i2);

/**** 
Null-Coalescing Assignment (??)
dikasih default value semisal varnya null
****/ 
string? title = null;
double? rating = null; 
string? ada_valuenya = "Hello world";

title ??= "Breaking Bad";
rating ??= 9.50;
ada_valuenya ??= "Ini ga bakal ke print";

// Console.WriteLine($"Judul: {title} - {rating} - {ada_valuenya}"); // Breaking Bad - 9,5 - Hello world

/**** 
Null-Conditional and Null-Coleascing
****/
City? city = null;
string name = city?.Name?? "Unknown";
string location = city?.Location?? "Unknown";

// Console.WriteLine($"Name: {name}, Location: {location}");

class City
{
    public required string Name { get; set; }
    public required string Location { get; set; }
}


/**** 
Upcasting and Downcasting Inheritance
****/
// implementasi di paling atas
public class Character
{
    public required string Name;
}

public class Magician : Character
{
    public int MagicMana;
}

public class Healer : Character
{
    public int HealingPower;
}

