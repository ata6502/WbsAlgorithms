using NUnit.Framework;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    class MoveToFrontTest
    {
        [Test]
        public void OneElementTest()
        {
            var m = new MoveToFront<int>();

            m.Add(1);

            Assert.AreEqual(1, m.Head.Item);
            Assert.IsNull(m.Head.Next);

            m.Add(1);

            Assert.AreEqual(1, m.Head.Item);
            Assert.IsNull(m.Head.Next);
        }

        [Test]
        public void TwoElementsTest()
        {
            var m = new MoveToFront<int>();

            m.Add(1);

            Assert.AreEqual(1, m.Head.Item);
            Assert.IsNull(m.Head.Next);

            m.Add(2);

            Assert.AreEqual(2, m.Head.Item);
            Assert.AreEqual(1, m.Head.Next.Item);
            Assert.IsNull(m.Head.Next.Next);

            m.Add(1);

            Assert.AreEqual(1, m.Head.Item);
            Assert.AreEqual(2, m.Head.Next.Item);
            Assert.IsNull(m.Head.Next.Next);

            m.Add(2);

            Assert.AreEqual(2, m.Head.Item);
            Assert.AreEqual(1, m.Head.Next.Item);
            Assert.IsNull(m.Head.Next.Next);

            m.Add(2);

            Assert.AreEqual(2, m.Head.Item);
            Assert.AreEqual(1, m.Head.Next.Item);
            Assert.IsNull(m.Head.Next.Next);

            m.Add(1);

            Assert.AreEqual(1, m.Head.Item);
            Assert.AreEqual(2, m.Head.Next.Item);
            Assert.IsNull(m.Head.Next.Next);

            m.Add(1);

            Assert.AreEqual(1, m.Head.Item);
            Assert.AreEqual(2, m.Head.Next.Item);
            Assert.IsNull(m.Head.Next.Next);
        }

        [Test]
        public void ThreeElementsTest()
        {
            var m = new MoveToFront<int>();

            m.Add(1);
            m.Add(2);
            m.Add(3);
            m.Add(1);

            Assert.AreEqual(1, m.Head.Item);
            Assert.AreEqual(3, m.Head.Next.Item);
            Assert.AreEqual(2, m.Head.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next);

            m.Add(3);

            Assert.AreEqual(3, m.Head.Item);
            Assert.AreEqual(1, m.Head.Next.Item);
            Assert.AreEqual(2, m.Head.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next);

            m.Add(3);

            Assert.AreEqual(3, m.Head.Item);
            Assert.AreEqual(1, m.Head.Next.Item);
            Assert.AreEqual(2, m.Head.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next);
        }

        [Test]
        public void MixedTest()
        {
            var m = new MoveToFront<char>();

            m.Add('A');
            Assert.AreEqual('A', m.Head.Item);
            Assert.IsNull(m.Head.Next);

            m.Add('B');
            Assert.AreEqual('B', m.Head.Item);
            Assert.AreEqual('A', m.Head.Next.Item);
            Assert.IsNull(m.Head.Next.Next);

            m.Add('C');
            Assert.AreEqual('C', m.Head.Item);
            Assert.AreEqual('B', m.Head.Next.Item);
            Assert.AreEqual('A', m.Head.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next);

            m.Add('C');
            Assert.AreEqual('C', m.Head.Item);
            Assert.AreEqual('B', m.Head.Next.Item);
            Assert.AreEqual('A', m.Head.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next);

            m.Add('D');
            Assert.AreEqual('D', m.Head.Item);
            Assert.AreEqual('C', m.Head.Next.Item);
            Assert.AreEqual('B', m.Head.Next.Next.Item);
            Assert.AreEqual('A', m.Head.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next);

            m.Add('A');
            Assert.AreEqual('A', m.Head.Item);
            Assert.AreEqual('D', m.Head.Next.Item);
            Assert.AreEqual('C', m.Head.Next.Next.Item);
            Assert.AreEqual('B', m.Head.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next);

            m.Add('B');
            Assert.AreEqual('B', m.Head.Item);
            Assert.AreEqual('A', m.Head.Next.Item);
            Assert.AreEqual('D', m.Head.Next.Next.Item);
            Assert.AreEqual('C', m.Head.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next);

            m.Add('E');
            Assert.AreEqual('E', m.Head.Item);
            Assert.AreEqual('B', m.Head.Next.Item);
            Assert.AreEqual('A', m.Head.Next.Next.Item);
            Assert.AreEqual('D', m.Head.Next.Next.Next.Item);
            Assert.AreEqual('C', m.Head.Next.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next.Next);

            m.Add('B');
            Assert.AreEqual('B', m.Head.Item);
            Assert.AreEqual('E', m.Head.Next.Item);
            Assert.AreEqual('A', m.Head.Next.Next.Item);
            Assert.AreEqual('D', m.Head.Next.Next.Next.Item);
            Assert.AreEqual('C', m.Head.Next.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next.Next);

            m.Add('A');
            Assert.AreEqual('A', m.Head.Item);
            Assert.AreEqual('B', m.Head.Next.Item);
            Assert.AreEqual('E', m.Head.Next.Next.Item);
            Assert.AreEqual('D', m.Head.Next.Next.Next.Item);
            Assert.AreEqual('C', m.Head.Next.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next.Next);

            m.Add('C');
            Assert.AreEqual('C', m.Head.Item);
            Assert.AreEqual('A', m.Head.Next.Item);
            Assert.AreEqual('B', m.Head.Next.Next.Item);
            Assert.AreEqual('E', m.Head.Next.Next.Next.Item);
            Assert.AreEqual('D', m.Head.Next.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next.Next);

            m.Add('A');
            Assert.AreEqual('A', m.Head.Item);
            Assert.AreEqual('C', m.Head.Next.Item);
            Assert.AreEqual('B', m.Head.Next.Next.Item);
            Assert.AreEqual('E', m.Head.Next.Next.Next.Item);
            Assert.AreEqual('D', m.Head.Next.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next.Next);

            m.Add('A');
            Assert.AreEqual('A', m.Head.Item);
            Assert.AreEqual('C', m.Head.Next.Item);
            Assert.AreEqual('B', m.Head.Next.Next.Item);
            Assert.AreEqual('E', m.Head.Next.Next.Next.Item);
            Assert.AreEqual('D', m.Head.Next.Next.Next.Next.Item);
            Assert.IsNull(m.Head.Next.Next.Next.Next.Next);
        }
    }
}
