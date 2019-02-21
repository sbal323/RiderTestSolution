using System;
using System.Dynamic;
using Newtonsoft.Json;

namespace Test70_483.Tests
{
    public class DynamicTest
    {
        public void Test()
        {
            dynamic exp = new ExpandoObject();
            exp.Employee = "Sergey Balog";
            exp.Department = "Top";
            exp.Address = new ExpandoObject();
            exp.Address.Country = "Ukraine";
            exp.Address.Zip = "02232";
            var json = JsonConvert.SerializeObject(exp);
            Console.WriteLine(json);
            exp = JsonConvert.DeserializeObject(
                "{'Employee':'Sergey Balog new','Department':'Top','Address':{'Country':'Ukraine','Zip':'02232'}}");
            Console.WriteLine(exp.Employee);
        }
    }
}