// bikin struct
MyStruct mystruct1;
mystruct1.x = 1;
mystruct1.y = 2;

// bikin class
MyClass myclass1;
myclass1 = new MyClass();
myclass1.x = 1;
myclass1.y = 2;

Console.WriteLine(mystruct1.x); 
Console.WriteLine(mystruct1.y);
Console.WriteLine(myclass1.x);
Console.WriteLine(myclass1.y);

/***
Output 1 2 1 2, sebelum bikin instance baru 
***/

// bikin instance baru 
MyStruct mystruct2;
mystruct2 = mystruct1;

MyClass myclass2;
myclass2 = myclass1;

mystruct2.x = 5;
mystruct2.y = 7;
myclass2.x = 5;
myclass2.y = 7;

Console.WriteLine(mystruct1.x); 
Console.WriteLine(mystruct1.y);
Console.WriteLine(myclass1.x);
Console.WriteLine(myclass1.y);

/***
Output 1 2 5 7, yang class berubah karena reference type 
***/