using System;
using System.Collections.Generic;
using AiTest.Tests;
using AiTest.Tests.IrisTest;
using AiTest.Tests.SalaryTest;
using Microsoft.ML;
using Microsoft.ML.Data;

namespace AiTest
{
    class Program
    {
        // Examples from here:
        // https://github.com/dotnet/machinelearning-samples/tree/master/samples/csharp/getting-started/
        //
        static void Main(string[] args)
        {
            var tests = new List<IAiTest>
            {
                //clustering
                //new IrisAiTest(),
                //regression
                //new SalaryAiTest(),
                new PriceAiTest()
                //binary classification
                //new SentimentAiTest(),
                //multi-class classification
                //new IssueAiTest()
            };
            foreach (var test in tests)
            {
                AiTester.SetTest(test).Execute();
            }
        }
    }
}