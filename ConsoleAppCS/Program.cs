using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleAppCS.Variance;

namespace ConsoleAppCS
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var d = new Derived();
        }
    }

    public abstract class Base
    {
        protected Base()
        {
            Console.WriteLine("BAse constructor");
        }
    }

    public class Derived : Base
    {
        public Derived()
        {
            Console.WriteLine("Derived constructor");
        }
    }
}