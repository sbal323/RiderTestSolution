using System;
using System.Collections;

namespace Test70_483.Types
{
    public class EmployeeCollection: ICollection
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsSynchronized { get; }
        public object SyncRoot { get; }
    }
}