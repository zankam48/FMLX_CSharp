// declaration
int? myInt = null;

// struct nullable tar dah pusing 
public struct Nullable<T> where T : struct 
{

}

// implicit and explicit conversions

//implicit conversions
int? x = 5 // dari int ke int?

// explicit conversions
int? x = 5
int y = (int)x // dari int? ke int

// if x null -> invalid operation thats why you should check first
if (x.HasValue)
{
    int y = int(x)
}

/***
BOXING AND UNBOXING
***/

// boxing : value type -> ref type means placing val type in heap
int? x = 10
object ob = x;

// unboxing nullable type from object 
object o = "daokwoda";
int? x = o as int?;
print x.HasValue // false

/***
OPERATOR LIFITING NTAR
***/

