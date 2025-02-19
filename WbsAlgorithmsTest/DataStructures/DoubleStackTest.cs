using NUnit.Framework;
using System;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    class DoubleStackTest
    {
        [Test]
        public void EmptyDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            Assert.That(ds.IsLeftEmpty, Is.True);
            Assert.That(ds.LeftSize, Is.EqualTo(0));

            Assert.That(ds.IsRightEmpty, Is.True);
            Assert.That(ds.RightSize, Is.EqualTo(0));

            Assert.Throws<ArgumentException>(() => ds.PopLeft());
            Assert.Throws<ArgumentException>(() => ds.PopRight());
        }

        [Test]
        public void OneElementDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushRight(1);
            Assert.That(ds.IsRightEmpty, Is.False);
            Assert.That(ds.IsLeftEmpty, Is.True);
            Assert.That(ds.PopRight(), Is.EqualTo(1));
            Assert.That(ds.IsRightEmpty, Is.True);

            ds.PushLeft(1);
            Assert.That(ds.IsLeftEmpty, Is.False);
            Assert.That(ds.IsRightEmpty, Is.True);
            Assert.That(ds.PopLeft(), Is.EqualTo(1));
            Assert.That(ds.IsLeftEmpty, Is.True);
        }

        [Test]
        public void TwoElementsDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushLeft(1);
            ds.PushRight(2);
            Assert.That(ds.LeftSize, Is.EqualTo(1));
            Assert.That(ds.RightSize, Is.EqualTo(1));

            Assert.That(ds.PopLeft(), Is.EqualTo(1));
            Assert.That(ds.PopRight(), Is.EqualTo(2));
        }

        [Test]
        public void ThreeElementsDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushLeft(2);
            ds.PushRight(3);
            ds.PushLeft(1);
            Assert.That(ds.LeftSize, Is.EqualTo(2));
            Assert.That(ds.RightSize, Is.EqualTo(1));

            Assert.That(ds.PopLeft(), Is.EqualTo(1));
            Assert.That(ds.PopRight(), Is.EqualTo(3));
            Assert.That(ds.PopLeft(), Is.EqualTo(2));
        }

        [Test]
        public void MixedElementsDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushLeft(3);
            ds.PushLeft(2);
            ds.PushLeft(1);
            Assert.That(ds.LeftSize, Is.EqualTo(3));

            Assert.That(ds.PopLeft(), Is.EqualTo(1));
            Assert.That(ds.LeftSize, Is.EqualTo(2));

            Assert.That(ds.PopLeft(), Is.EqualTo(2));
            Assert.That(ds.LeftSize, Is.EqualTo(1));

            ds.PushRight(4);
            ds.PushRight(5);
            Assert.That(ds.RightSize, Is.EqualTo(2));

            Assert.That(ds.PopLeft(), Is.EqualTo(3));
            Assert.That(ds.LeftSize, Is.EqualTo(0));

            Assert.That(ds.PopRight(), Is.EqualTo(5));
            Assert.That(ds.RightSize, Is.EqualTo(1));

            Assert.That(ds.PopRight(), Is.EqualTo(4));
            Assert.That(ds.RightSize, Is.EqualTo(0));

            Assert.That(ds.IsLeftEmpty, Is.True);
            Assert.That(ds.IsRightEmpty, Is.True);
        }
    }
}
