using System;
using ComputerLib;

var pc = new Computer();

pc.Start();
Console.WriteLine(pc.PrintFile("report.pdf"));
Console.WriteLine(pc.Scan());
pc.Shutdown();

Console.WriteLine("Done");
