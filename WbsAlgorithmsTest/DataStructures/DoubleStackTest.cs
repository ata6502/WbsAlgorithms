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

            Assert.IsTrue(ds.IsLeftEmpty);
            Assert.AreEqual(0, ds.LeftSize);

            Assert.IsTrue(ds.IsRightEmpty);
            Assert.AreEqual(0, ds.RightSize);

            Assert.Throws<ArgumentException>(() => ds.PopLeft());
            Assert.Throws<ArgumentException>(() => ds.PopRight());
        }

        [Test]
        public void OneElementDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushRight(1);
            Assert.IsFalse(ds.IsRightEmpty);
            Assert.IsTrue(ds.IsLeftEmpty);
            Assert.AreEqual(1, ds.PopRight());
            Assert.IsTrue(ds.IsRightEmpty);

            ds.PushLeft(1);
            Assert.IsFalse(ds.IsLeftEmpty);
            Assert.IsTrue(ds.IsRightEmpty);
            Assert.AreEqual(1, ds.PopLeft());
            Assert.IsTrue(ds.IsLeftEmpty);
        }

        [Test]
        public void TwoElementsDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushLeft(1);
            ds.PushRight(2);
            Assert.AreEqual(1, ds.LeftSize);
            Assert.AreEqual(1, ds.RightSize);

            Assert.AreEqual(1, ds.PopLeft());
            Assert.AreEqual(2, ds.PopRight());
        }

        [Test]
        public void ThreeElementsDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushLeft(2);
            ds.PushRight(3);
            ds.PushLeft(1);
            Assert.AreEqual(2, ds.LeftSize);
            Assert.AreEqual(1, ds.RightSize);

            Assert.AreEqual(1, ds.PopLeft());
            Assert.AreEqual(3, ds.PopRight());
            Assert.AreEqual(2, ds.PopLeft());
        }

        [Test]
        public void MixedElementsDoubleStackTest()
        {
            var ds = new DoubleStack<int>();

            ds.PushLeft(3);
            ds.PushLeft(2);
            ds.PushLeft(1);
            Assert.AreEqual(3, ds.LeftSize);

            Assert.AreEqual(1, ds.PopLeft());
            Assert.AreEqual(2, ds.LeftSize);

            Assert.AreEqual(2, ds.PopLeft());
            Assert.AreEqual(1, ds.LeftSize);

            ds.PushRight(4);
            ds.PushRight(5);
            Assert.AreEqual(2, ds.RightSize);

            Assert.AreEqual(3, ds.PopLeft());
            Assert.AreEqual(0, ds.LeftSize);

            Assert.AreEqual(5, ds.PopRight());
            Assert.AreEqual(1, ds.RightSize);

            Assert.AreEqual(4, ds.PopRight());
            Assert.AreEqual(0, ds.RightSize);

            Assert.IsTrue(ds.IsLeftEmpty);
            Assert.IsTrue(ds.IsRightEmpty);
        }
    }
}
