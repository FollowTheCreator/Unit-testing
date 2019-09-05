using System;
using System.Collections;
using System.Collections.Generic;

namespace RoundRobinCollection
{
    public class RoundRobinCollection<T> : IEnumerable<T>
    {
        private static readonly Queue<T> _queue = new Queue<T>();
        private T _first;

        public RoundRobinCollection(params T[] sequence)
        {
            if(sequence == null)
            {
                throw new ArgumentNullException(nameof(sequence));
            }

            _queue.Clear();
            foreach (var item in sequence)
            {
                _queue.Enqueue(item);
            }

            _first = _queue.Dequeue();
            _queue.Enqueue(_first);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new RoundRobinCollectionEnumerator(_first);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public class RoundRobinCollectionEnumerator : IEnumerator<T>
        {
            public RoundRobinCollectionEnumerator(T first)
            {
                Temp = first;
            }

            public static T Temp { get; set; }

            public T Current
            {
                get
                {
                    return Temp;
                }
            }

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                Temp = _queue.Dequeue();
                _queue.Enqueue(Temp);

                return true;
            }

            public void Reset()
            { }

            public void Dispose()
            { }
        }
    }
}
