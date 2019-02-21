using System;

namespace ConsoleAppCS.Variance
{
    public class Descendant : Base
    {
        public string Name { get; set; }
        
        public void DoMore()
        {
            Console.WriteLine("Doing even more...");
        }
    }
}