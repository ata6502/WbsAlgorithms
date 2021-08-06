using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructs;

namespace WbsAlgorithmsTest.Collections
{
    [TestFixture]
    class RingBufferTest
    {
        [Test]
        public void EmptyBufferTest()
        {
            var buffer = new RingBuffer<int>(3);

            Assert.IsTrue(buffer.IsEmpty);
            Assert.AreEqual(0, buffer.Size);

            // If the buffer is empty, it should throw an exception.
            Assert.Throws<IndexOutOfRangeException>(() => buffer.Get());
        }

        [Test]
        public void SimpleTest()
        {
            var buffer = new RingBuffer<int>(3);

            buffer.Put(1);
            Assert.AreEqual(1, buffer.Get());
            Assert.IsTrue(buffer.IsEmpty);

            buffer.Put(1);
            buffer.Put(2);
            Assert.AreEqual(1, buffer.Get());
            Assert.AreEqual(2, buffer.Get());
            Assert.IsTrue(buffer.IsEmpty);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            Assert.AreEqual(1, buffer.Get());
            Assert.AreEqual(2, buffer.Get());
            Assert.AreEqual(3, buffer.Get());
            Assert.IsTrue(buffer.IsEmpty);
        }

        [Test]
        public void OverflowTest()
        {
            var buffer = new RingBuffer<int>(3);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1
            Assert.AreEqual(2, buffer.Get());
            Assert.AreEqual(3, buffer.Get());
            Assert.AreEqual(4, buffer.Get());
            Assert.IsTrue(buffer.IsEmpty);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1 
            buffer.Put(5); // overrides 2
            Assert.AreEqual(3, buffer.Get());
            Assert.AreEqual(4, buffer.Get());
            Assert.AreEqual(5, buffer.Get());
            Assert.IsTrue(buffer.IsEmpty);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1 
            buffer.Put(5); // overrides 2
            buffer.Put(6); // overrides 3 
            Assert.AreEqual(4, buffer.Get());
            Assert.AreEqual(5, buffer.Get());
            Assert.AreEqual(6, buffer.Get());
            Assert.IsTrue(buffer.IsEmpty);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1 
            buffer.Put(5); // overrides 2
            buffer.Put(6); // overrides 3 
            buffer.Put(7); // overrides 4
            Assert.AreEqual(5, buffer.Get());
            Assert.AreEqual(6, buffer.Get());
            Assert.AreEqual(7, buffer.Get());
            Assert.IsTrue(buffer.IsEmpty);
        }

        [Test]
        public void MixedTest()
        {
            var buffer = new RingBuffer<int>(3);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1
            
            Assert.AreEqual(2, buffer.Get());

            buffer.Put(5);

            Assert.AreEqual(3, buffer.Get());
            Assert.AreEqual(4, buffer.Get());

            buffer.Put(6);
            buffer.Put(7);

            Assert.AreEqual(5, buffer.Get());
            Assert.AreEqual(6, buffer.Get());

            buffer.Put(8);

            Assert.AreEqual(7, buffer.Get());
            Assert.AreEqual(8, buffer.Get());

            Assert.IsTrue(buffer.IsEmpty);

        }
    }
}
