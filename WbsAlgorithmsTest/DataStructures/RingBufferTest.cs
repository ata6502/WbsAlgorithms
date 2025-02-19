using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructs;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    class RingBufferTest
    {
        [Test]
        public void EmptyBufferTest()
        {
            var buffer = new RingBuffer<int>(3);

            Assert.That(buffer.IsEmpty, Is.True);
            Assert.That(buffer.Size, Is.EqualTo(0));

            // If the buffer is empty, it should throw an exception.
            Assert.Throws<IndexOutOfRangeException>(() => buffer.Get());
        }

        [Test]
        public void SimpleTest()
        {
            var buffer = new RingBuffer<int>(3);

            buffer.Put(1);
            Assert.That(buffer.Get(), Is.EqualTo(1));
            Assert.That(buffer.IsEmpty, Is.True);

            buffer.Put(1);
            buffer.Put(2);
            Assert.That(buffer.Get(), Is.EqualTo(1));
            Assert.That(buffer.Get(), Is.EqualTo(2));
            Assert.That(buffer.IsEmpty, Is.True);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            Assert.That(buffer.Get(), Is.EqualTo(1));
            Assert.That(buffer.Get(), Is.EqualTo(2));
            Assert.That(buffer.Get(), Is.EqualTo(3));
            Assert.That(buffer.IsEmpty, Is.True);
        }

        [Test]
        public void OverflowTest()
        {
            var buffer = new RingBuffer<int>(3);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1
            Assert.That(buffer.Get(), Is.EqualTo(2));
            Assert.That(buffer.Get(), Is.EqualTo(3));
            Assert.That(buffer.Get(), Is.EqualTo(4));
            Assert.That(buffer.IsEmpty, Is.True);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1 
            buffer.Put(5); // overrides 2
            Assert.That(buffer.Get(), Is.EqualTo(3));
            Assert.That(buffer.Get(), Is.EqualTo(4));
            Assert.That(buffer.Get(), Is.EqualTo(5));
            Assert.That(buffer.IsEmpty, Is.True);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1 
            buffer.Put(5); // overrides 2
            buffer.Put(6); // overrides 3 
            Assert.That(buffer.Get(), Is.EqualTo(4));
            Assert.That(buffer.Get(), Is.EqualTo(5));
            Assert.That(buffer.Get(), Is.EqualTo(6));
            Assert.That(buffer.IsEmpty, Is.True);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1 
            buffer.Put(5); // overrides 2
            buffer.Put(6); // overrides 3 
            buffer.Put(7); // overrides 4
            Assert.That(buffer.Get(), Is.EqualTo(5));
            Assert.That(buffer.Get(), Is.EqualTo(6));
            Assert.That(buffer.Get(), Is.EqualTo(7));
            Assert.That(buffer.IsEmpty, Is.True);
        }

        [Test]
        public void MixedTest()
        {
            var buffer = new RingBuffer<int>(3);

            buffer.Put(1);
            buffer.Put(2);
            buffer.Put(3);
            buffer.Put(4); // overrides 1
            
            Assert.That(buffer.Get(), Is.EqualTo(2));

            buffer.Put(5);

            Assert.That(buffer.Get(), Is.EqualTo(3));
            Assert.That(buffer.Get(), Is.EqualTo(4));

            buffer.Put(6);
            buffer.Put(7);

            Assert.That(buffer.Get(), Is.EqualTo(5));
            Assert.That(buffer.Get(), Is.EqualTo(6));

            buffer.Put(8);

            Assert.That(buffer.Get(), Is.EqualTo(7));
            Assert.That(buffer.Get(), Is.EqualTo(8));

            Assert.That(buffer.IsEmpty, Is.True);

        }
    }
}
