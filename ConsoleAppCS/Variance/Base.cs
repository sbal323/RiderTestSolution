using System;

namespace ConsoleAppCS.Variance
{
    public class Base
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public void Do()
        {
            Console.WriteLine("Doing something...");
        }
    }
}