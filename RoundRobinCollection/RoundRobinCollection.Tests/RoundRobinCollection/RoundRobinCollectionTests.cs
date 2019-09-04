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
            var collection = new RoundRobinCollection<int>(1, 2, 3, 4);
            var instance = collection.GetEnumerator();

            // Act
            for(int i = 0; i < 4; i++)
            {
                instance.MoveNext();
            }

            // Assert
            Assert.Equal(1, instance.Current);
        }

        [Fact]
        public void EnumerateByMultipleGetEnumeratorInstances()
        {
            // Arrange
            var collection = new RoundRobinCollection<int>(1, 2, 3, 4);
            var firstInstance = collection.GetEnumerator();
            var secondInstance = collection.GetEnumerator();

            // Act
            var firstItem = firstInstance.Current;
            firstInstance.MoveNext();
            var secondItem = secondInstance.Current;
            secondInstance.MoveNext();
            var thirdItem = firstInstance.Current;
            firstInstance.MoveNext();
            var fourthItem = firstInstance.Current;
            firstInstance.MoveNext();

            // Assert
            Assert.Equal(1, firstItem);
            Assert.Equal(2, secondItem);
            Assert.Equal(3, thirdItem);
            Assert.Equal(4, fourthItem);
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
