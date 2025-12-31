using System;
using System.Collections.Generic;

namespace ComputerLib
{
    public sealed class PowerSupply
    {
        public bool IsOn { get; private set; }
        public void On() => IsOn = true;
        public void Off() => IsOn = false;
    }

    public sealed class Bios
    {
        public bool IsReady { get; private set; }
        public void Init() => IsReady = true;
    }

    public sealed class OperatingSystem
    {
        public bool IsRunning { get; private set; }
        public void Boot() => IsRunning = true;
        public void Shutdown() => IsRunning = false;
    }

    public sealed class Printer
    {
        public string Print(string fileName) => $"Printed: {fileName}";
    }

    public sealed class Scanner
    {
        public string Scan() => "Scanned: OK";
    }

    public sealed class Computer
    {
        private readonly PowerSupply _power;
        private readonly Bios _bios;
        private readonly OperatingSystem _os;
        private readonly Printer _printer;
        private readonly Scanner _scanner;

        public Computer()
        {
            _power = new PowerSupply();
            _bios = new Bios();
            _os = new OperatingSystem();
            _printer = new Printer();
            _scanner = new Scanner();
        }

        public bool IsOn => _power.IsOn;
        public bool IsOsRunning => _os.IsRunning;

        public void Start()
        {
            _power.On();
            _bios.Init();
            _os.Boot();
        }

        public void Shutdown()
        {
            _os.Shutdown();
            _power.Off();
        }

        public string PrintFile(string fileName)
        {
            if (!_power.IsOn || !_os.IsRunning) throw new InvalidOperationException("Computer is not ready");
            return _printer.Print(fileName);
        }

        public string Scan()
        {
            if (!_power.IsOn || !_os.IsRunning) throw new InvalidOperationException("Computer is not ready");
            return _scanner.Scan();
        }
    }
}
