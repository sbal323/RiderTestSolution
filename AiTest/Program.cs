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
        static void Main(string[] args)
        {
            var tests = new List<IAiTest>
            {
//                //clustering
//                new IrisAiTest(),
//                //regression
//                new SalaryAiTest(),
                //binary classification
                new SentimentAiTest(),
                //multiclass classification
                new IssueAiTest()
            };
            var tester = new AiTester();
            foreach (var test in tests)
            {
                tester.SetTest(test).Execute();
            }
        }
    }
}