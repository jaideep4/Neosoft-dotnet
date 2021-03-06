# Language Enhancements in C# 3.0 (Group 9)
####1. Implicitly Typed Local variables
-	Local variables can be declared without giving an explicit type. 
-	The var keyword instructs the compiler to infer the type of the variable from the expression on the right side of the initialization statement. 
-	Var keyword was introduced in c# version 3.
-	An implicitly typed local variable is strongly typed just as if you had declared the type yourself.
-	Here the compiler will determine the type.
```
var i=10; //Implicitly Typed
var i=20; //Explicitly Typed
```
   
**Var can be used in the following different contexts:**
    ❖ Local variable in a function. 
    ❖ For loop  
    ```
    for (var x=1; x<10; x++) 
    ```
    ❖ Foreach loop 
    ```
    foreach (var item in list){...}
    ```
    ❖ Using statement
    ❖ As an anonymous type
 

**Key points about Var :**

- Var can only be declared and initialized in a single statement. Following is not valid:  
```
           Var i;
           i=10

```
- Var cannot be used as a field type at the class level
- Var cannot be used in an expression like :
```
           var i += 10;
```
- Multiple Vars cannot be declared and initialized in a single statement.
```
  
For example : var i = 10, j = 20;  is invalid.
```
Following example shows that how var keyword variables can be of different types based on their value.

![](.\images\akshay.png)

####2. Anonymous Type in C#

1.	In C#, an anonymous type is a type (class) without any name that can contain public read-only properties only. It cannot contain other members, such as fields, methods, events, etc.

2.	This feature gives you the flexibility to create an instance of a class without having to write code for the class.

3.	To create an anonymous type, the new operator is used with an anonymous object initializer.  The implicitly typed variable- var is used to hold the reference of anonymous types.


A .  For Example:
```
new {name="xyz", address="King street, Chicago", state="IL", zipcode="53703"};
```
B. The above line of code, with the help of the new keyword, gives you a new type that has four properties like name, address, state, zip code. Behind the scene, the C# compiler would create a class that looks like as follows:
```
class _Anonymous1
{
   		 private string _name = "xyz";
   		 private string _address = "King street, chicago";
   		 private string _state = "IL";
   		 private string _zipcode = "53703";
 
   		 private string name { get { return _name; } set { _name = value; } }
   		 private string address { get { return _address; } set { _address = value; } }
   		 private string state { get { return _state; } set { _state = value; } }
  		 private string zipcode { get { return _zipcode; } set { _zipcode = value; } }
} 

```
Now you have a class, but you still need something to hold an instance of the above class. This is where the "var" keyword comes in handy.  
```
var objtest = new {name="xyz", address="King street, Chicago", state="IL", zip_code="53703"}; 
```
4.	Each member of the anonymous type is a property inferred from the object initializer. The name of the anonymous type is automatically generated by the compiler and cannot be referenced from the user code.
5.	The properties of anonymous types are read-only and cannot be initialized with a null, anonymous function, or a pointer type. The properties can be accessed using dot (.) notation, same as object properties. However, you cannot change the values of properties as they are read-only.
6.	Nested anonymous type : An anonymous type's property can include another anonymous type.
```
var student = new[]
{ Id = 1,
 FirstName = "James",
 LastName = "Bond",
 Address = new { Id = 1, City = "London", Country = "UK" } };

```
7.  Array of Anonymous Types : You can create an array of anonymous types also.
Example:
```
var students = new[] {
 new { Id = 1, FirstName = "James", LastName = "Bond" },
 new { Id = 2, FirstName = "Steve", LastName = "Jobs" }, 
new { Id = 3, FirstName = "Bill", LastName = "Gates" } 
};

```
8.  An anonymous type will always be local to the method where it is defined. It cannot be returned from the method. However, an anonymous type can be passed to the method as object type parameter, but it is not recommended. If you need to pass it to another method, then use struct or class instead of an anonymous type. 
9.  You are allowed to use an anonymous type in LINQ. In LINQ, select clause generates anonymous type so that in a query you can include properties that are not defined in the class.
10.  Internally, all the anonymous types are directly derived from the System.Object class.The compiler generates a class with some auto-generated name and applies the appropriate type to each property based on the value expression. Although your code cannot access it. Use GetType() method to see the name.

**Some important points:**
1. It can contain one or more read only properties.
2.	It cannot be cast to any other type except an object.
3.	It is of reference type.
4.	You are not allowed to create a field, property, event, or return type if a method is of anonymous type.
5.	You are not allowed to declare formal parameters of a method, property, constructor, indexer as an anonymous type.



####3. Extension methods in C# 
-  Extension methods enable you to "add" methods to existing types without creating a new derived type, recompiling, or otherwise modifying the original type.
- Extension methods are static methods, but they're called as if they were instance methods on the extended type.

**To define and call the extension method**
1. Define a static class to contain the extension method.
2.  Implement the extension method as a static method with at least the same visibility as the containing class.
3.  The first parameter of the method specifies the type that the method operates on it must be preceded with the this modifier.
4.  In the calling code, add a using directive to specify the namespace that contains the extension method class.
5. Call the methods as if they were instance methods on the type.
- Note that the first parameter is not specified by calling code because it represents the type on which the operator is being applied, and the compiler already knows the type of your object. You only have to provide arguments.

**Example -** 
The following example shows an extension method defined for the System.String class. It's defined inside a non-nested, non-generic static class:
```
namespace ExtensionMethods
{
    public static class MyExtensions
    {
        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}

```
The WordCount extension method can be brought into scope with this using directive:
```
C#
using ExtensionMethods;
```

And it can be called from an application by using this syntax:
```
C#
string s = "Hello Extension Methods";
int i = s.WordCount();

```
You invoke the extension method in your code with instance method syntax. The intermediate language (IL) generated by the compiler translates your code into a call on the static method. The principle of encapsulation is not really being violated. Extension methods cannot access private variables in the type they are extending.



**Benefits of extension methods:**

- Extension methods allow existing classes to be extended without relying on inheritance or having to change the class's source code.
- If the class is sealed than there in no concept of extending its functionality. For this a new concept is introduced, in other words extension methods.
- This feature is important for all developers, especially if you would like to use the dynamism of the C# enhancements in your class's design.


**Important points for the use of extension methods** 
- An extension method must be defined in a top-level static class.
- An extension method with the same name and signature as an instance method will not be called.
- Extension methods cannot be used to override existing methods.
- The concept of extension methods cannot be applied to fields, properties or events.
- Overuse of extension methods is not a good style of programming.






#### 4. Object Initializer in C#

Object initializers let you assign values to any accessible fields or properties of an object without having to invoke a constructor followed by lines of assignment statements.

The following example shows how to use an object initializer with a named type, Cat and how to invoke the parameter less constructor.
```
public class Cat
{
	// Auto-implemented properties.
	Public int Age { get; set; }
	public string Name { get; set; }

	public Cat()
	{
	}
	public Cat(string name)
	{
    	this.Name = name;
	}
}
 

```
```
Object initializing eg –
Cat cat = new Cat { Age = 10, Name = "Fluffy" };
Cat sameCat = new Cat("Fluffy"){ Age = 10 };
```



The object initializers syntax allows you to create an instance, and after that it assigns the newly created object, with its assigned properties, to the variable in the assignment.

####Object Initializers with anonymous types
Although object initializers can be used in any context, they are especially useful in LINQ query expressions. Query expressions make frequent use of anonymous types, which can only be initialized by using an object initializer, as shown in the following declaration.
```
var pet = new { Age = 10, Name = "Fluffy" };
```

####5. Collection Initializer in C#
- An object and collection initializer is an interesting and very useful feature of C# language. Collection Initializer allows us to initialize a collection type that implements IEnumerable interface.

- This requirement is very important - since we are using collection initializers, compiler expects that we will initialize some kind of collection and collections ought to implement IEnumerable interface.

- This feature provides a different way to initialize an object of a class or a collection. This feature is introduced in C# 3.0 or above. 

- The main advantages of using these are to make your code more readable, provide an easy way to add elements in collections, and are mostly used in multi-threading.

- Collection initializer is also similar to object initializers. The collections are initialized similarly like objects are initialized using an object initializer.

- In other words, generally, we used the Add() method to add elements in collections, but using a collection initializer you can add elements without using Add() method.
```

using System;  
using System.Collections.Generic;  
namespace CSharpFeatures  
{  
    class Student  
    {  
        public int ID { get; set; }  
        public string Name { get; set; }  
        public string Email { get; set; }  
    }  
    class ObjectInitializer  
    {  
        public static void Main(string[] args)  
        {  
            List<Student> students = new List<Student> {  
                new Student { ID=101, Name="Rahul", Email="rahul@example.com" },  
                new Student { ID=102, Name="Peter", Email="peter@abc.com" },  
                new Student { ID=103, Name="Irfan", Email="irfan@example.com" }  
            };  
            foreach (Student student in students)  
            {  
                Console.Write(student.ID+" ");  
                Console.Write(student.Name+" ");  
                Console.Write(student.Email+" ");  
                Console.WriteLine();  
            }  
        }  
    }  
} 

```


