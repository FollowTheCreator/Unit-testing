using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RoundRobinCollection
{
    public class RoundRobinCollection<T> : IEnumerable
    {
        private static readonly IEnumerable _sequence;

        public static RoundRobinCollection(IEnumerable sequence)
        {
            _sequence = sequence;
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
