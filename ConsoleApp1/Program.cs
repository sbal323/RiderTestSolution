using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var clients = new List<Client>
            {
                new Client{Id = 124, Title = "Bret Achtenhagen's Seasonal Services"},
                new Client{Id = 456, Title = "BretServices"},
            };
            var lson = JsonConvert.SerializeObject(clients);
            lson = lson.Replace("\"", "\\\"");
            lson = lson.Replace("\\\"", "\"");
            Console.WriteLine(lson);
            var clientsq = JsonConvert.DeserializeObject<List<Client>>(lson);
        }
    }
    public class Client
    {
        public string Title { get; set; }
        public int Id { get; set; }
    }
}