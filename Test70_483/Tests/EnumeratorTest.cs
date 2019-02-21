using System;
using System.Collections;
using System.Collections.Generic;
using Test70_483.Types;

namespace Test70_483.Tests
{
    public class EnumeratorTest
    {
        public IEnumerator<Employee> GetEnumerator()
        {
            yield return new Employee {FirstName = "Sergey", LastName = "Balog", Id = 25};
            yield return new Employee {FirstName = "Sergey", LastName = "Tiurin", Id = 23};
            yield return new Employee {FirstName = "Andrew", LastName = "Bobyr", Id = 252};
        }
    }
}