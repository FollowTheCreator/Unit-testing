using System;
using Xunit;

namespace RoundRobinCollection.Tests.RoundRobinCollection
{
    public class RoundRobinCollectionTests
    {
        [Fact]
        public void EnumerateMoreTimesThanSequenceLength()
        {
            // Arrange
            var inputData = new int[] { 1, 2, 3, 4 };
            var collection = new RoundRobinCollection<int>(inputData);
            var instance = collection.GetEnumerator();

            // Act
            for(int i = 0; i < inputData.Length; i++)
            {
                instance.MoveNext();
            }

            // Assert
            Assert.Equal(inputData[0], instance.Current);
        }

        [Fact]
        public void EnumerateByMultipleGetEnumeratorInstances()
        {
            // Arrange
            var inputData = new int[] { 1, 2, 3, 4 };
            var collection = new RoundRobinCollection<int>(inputData);
            var firstInstance = collection.GetEnumerator();
            var secondInstance = collection.GetEnumerator();

            // Act and Assert
            for(int i = 0; i < inputData.Length; i++)
            {
                Assert.Equal(inputData[i], firstInstance.Current);
                Assert.Equal(inputData[i], secondInstance.Current);
                var a = i / 3 == 0 ? secondInstance.MoveNext() : firstInstance.MoveNext();
            }
        }

        [Fact]
        public void PassNullSequenceIntoParameters()
        {
            Assert.Throws<ArgumentNullException>(
                () => new RoundRobinCollection<int>(null)
            );
        }
    }
}
