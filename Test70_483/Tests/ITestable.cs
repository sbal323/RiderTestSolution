using System;
using System.Runtime.InteropServices;

namespace Test70_483.Tests
{
    interface ITestable<T>
    {
        int ErrorCode { get; }
        void Test();
        string TestCode(int code);
        bool IsTestable();
        event Action<int> Completed;
        string this[int index] { get; }
        T GetResult();
    }

    class TestableOne: ITestable<string>
    {
        int ITestable.ErrorCode => 25;

        public int ErrorCode { get; }

        void ITestable.Test()
        {
            this.Completed += x => { Console.WriteLine($"Completed with {x} code"); };
        }

        public string TestCode(int code)
        {
            throw new NotImplementedException();
        }

        public void Test()
        {
            throw new NotImplementedException();
        }

        string ITestable.TestCode(int code)
        {
            Completed?.Invoke(0);
            return string.Empty;
        }

        public bool IsTestable()
        {
            return true;
        }

        public event Action<int> Completed;

        public string this[int index] => string.Empty;
        public string GetResult()
        {
            throw new NotImplementedException();
        }
    }
}