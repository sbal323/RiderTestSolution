using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Test70_483.Tests;
using Test70_483.Types;

namespace Test70_483
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
            var data = new List<string>()
            {
                "sdsd",
                "sdsdsd",
                "sdsdsd",
                "3333",
                "www"
            };
//            while(data.Count!=0)
//                data.RemoveAt(0);
            for (int i = 0; i <= data.Count; i++)
            {
                Console.WriteLine($"i = {i} count = {data.Count}");
                data.RemoveAt(i);
            }
                
            Console.WriteLine(data.Count);
        }

        
        
    }
}