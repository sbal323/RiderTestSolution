using System;

namespace ConsoleAppCS
{
    public interface IFatInterface
    {
        void A();
        void B();
        void C();
    }

    public class Client1 : IFatInterface
    {
        public void A()
        {
            // Do something
        }
        public void B()
        {
            // Do something
        }
        public void C()
        {
            // Do something
        }
    }

    public class Client2 : IFatInterface
    {
        public void A()
        {
            // Do something
        }
        public void B()
        {
            throw new NotImplementedException();
        }
        public void C()
        {
            throw new NotImplementedException();
        }
    }

    public interface INarrowInterface
    {
        void A();
        void B();
    }

    public class FatAdapter: INarrowInterface
    {
        private readonly IFatInterface _fat;

        public FatAdapter(IFatInterface fat)
        {
            _fat = fat;
        }

        public void A()
        {
            _fat.A();
        }

        public void B()
        {
            _fat.B();
        }
    }
}