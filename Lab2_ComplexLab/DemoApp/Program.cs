using System;
using MathLib;

class Program
{
    static void Main()
    {
        var a = new Complex(3, 4);
        var b = new Complex(1, -2);

        Console.WriteLine((object)a);
        Console.WriteLine((object)b);
        Console.WriteLine((object)(a + b));
        Console.WriteLine((object)(a - b));
        Console.WriteLine((object)(a * b));
        Console.WriteLine((object)(a / b));

        if (a)
            Console.WriteLine("a is true");

        double m = a;
        Console.WriteLine(m);
    }
}
