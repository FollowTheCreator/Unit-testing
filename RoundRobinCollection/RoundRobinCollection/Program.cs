using System;

namespace RoundRobinCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            var collection = new RoundRobinCollection<int>(1, 2, 3, 4);
            var firstInstance = collection.GetEnumerator();
            var secondInstance = collection.GetEnumerator();
            var thirdInstance = collection.GetEnumerator();

            Console.WriteLine(firstInstance.Current);
            firstInstance.MoveNext();
            Console.WriteLine(secondInstance.Current);
            secondInstance.MoveNext();
            Console.WriteLine(firstInstance.Current);
            firstInstance.MoveNext();

            var collection2 = new RoundRobinCollection<int>(5, 6, 7, 8);

            var firstInstance2 = collection2.GetEnumerator();
            var secondInstance2 = collection2.GetEnumerator();

            Console.WriteLine(firstInstance2.Current);
            firstInstance2.MoveNext();
            Console.WriteLine(secondInstance2.Current);
            secondInstance2.MoveNext();
            Console.WriteLine(firstInstance2.Current);
            firstInstance2.MoveNext();
        }
    }
}
