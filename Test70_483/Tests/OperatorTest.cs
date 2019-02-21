using System;

namespace Test70_483.Tests
{
    public class OperatorTest
    {
        public void Test()
        {
            var f = new First(10,20);
            Second s = f;
            Console.WriteLine(s.X);
            s.X = 100;
            f = (First)s;
            Console.WriteLine(f.X);
            // Delegates
            Action<int,int> action = (x, y) => Console.WriteLine($"Sum is {x + y}");
            action(23, 45);
            action = (x, y) => Console.WriteLine($"Mult is {x * y}");
            action(23, 45);
        }
    }

    class First
    {
        public int X { get; set; }
        public int Y { get; set; }

        public First(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static implicit operator Second(First f)
        {
            var s = new Second {X = f.X};
            return s;
        }
    }

    class Second
    {
        public int X { get; set; }

        public static explicit operator First(Second s)
        {
            var f = new First(s.X,0);
            return f;
        }
    }
}