using System;
using System.Collections.Generic;

namespace Test70_483.Tests
{
    public class ExceptionsTest
    {
        public void AggregateTest()
        {
            try
            {
                Operation();
            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message + " -- " + ex.StackTrace);
//            }
            catch (AggregateException ex)
            {
                Console.WriteLine("Following errors occured:");
                foreach (var exception in ex.Flatten().InnerExceptions)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void Operation()
        {
            var exceptions = new List<Exception>();
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    CallMethod(i);
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            if (exceptions.Count > 0)
                throw new AggregateException(exceptions.ToArray());
        }

        private void CallMethod(int i)
        {
            Console.WriteLine($"Processing {i} iteration.");
            if (i % 2 == 0)
            {
                throw new Exception($"Operation {i} failed.");
            }
        }
    }
}