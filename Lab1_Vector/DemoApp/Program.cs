using System;
using VectorLib;

var a = new VectorN(new[] { 1.0, 2.0, 3.0 });
var b = new VectorN(new[] { 4.0, 5.0, 6.0 });

Console.WriteLine($"a = {a}");
Console.WriteLine($"b = {b}");
Console.WriteLine($"a + b = {a.Add(b)}");
Console.WriteLine($"a - b = {a.Subtract(b)}");
Console.WriteLine($"a * 2 = {a.Multiply(2)}");
Console.WriteLine($"|a| = {a.Magnitude()}");
Console.WriteLine($"a · b = {a.Dot(b)}");
