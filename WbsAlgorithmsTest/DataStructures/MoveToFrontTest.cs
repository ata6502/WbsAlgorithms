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

            Assert.That(m.Head.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next, Is.Null);

            m.Add(1);

            Assert.That(m.Head.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next, Is.Null);
        }

        [Test]
        public void TwoElementsTest()
        {
            var m = new MoveToFront<int>();

            m.Add(1);

            Assert.That(m.Head.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next, Is.Null);

            m.Add(2);

            Assert.That(m.Head.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Next, Is.Null);

            m.Add(1);

            Assert.That(m.Head.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Next, Is.Null);

            m.Add(2);

            Assert.That(m.Head.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Next, Is.Null);

            m.Add(2);

            Assert.That(m.Head.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Next, Is.Null);

            m.Add(1);

            Assert.That(m.Head.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Next, Is.Null);

            m.Add(1);

            Assert.That(m.Head.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Next, Is.Null);
        }

        [Test]
        public void ThreeElementsTest()
        {
            var m = new MoveToFront<int>();

            m.Add(1);
            m.Add(2);
            m.Add(3);
            m.Add(1);

            Assert.That(m.Head.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Item, Is.EqualTo(3));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Next.Next, Is.Null);

            m.Add(3);

            Assert.That(m.Head.Item, Is.EqualTo(3));
            Assert.That(m.Head.Next.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Next.Next, Is.Null);

            m.Add(3);

            Assert.That(m.Head.Item, Is.EqualTo(3));
            Assert.That(m.Head.Next.Item, Is.EqualTo(1));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo(2));
            Assert.That(m.Head.Next.Next.Next, Is.Null);
        }

        [Test]
        public void MixedTest()
        {
            var m = new MoveToFront<char>();

            m.Add('A');
            Assert.That(m.Head.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next, Is.Null);

            m.Add('B');
            Assert.That(m.Head.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next, Is.Null);

            m.Add('C');
            Assert.That(m.Head.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next.Next, Is.Null);

            m.Add('C');
            Assert.That(m.Head.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next.Next, Is.Null);

            m.Add('D');
            Assert.That(m.Head.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next.Next.Next, Is.Null);

            m.Add('A');
            Assert.That(m.Head.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Next.Next, Is.Null);

            m.Add('B');
            Assert.That(m.Head.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Next.Next, Is.Null);

            m.Add('E');
            Assert.That(m.Head.Item, Is.EqualTo('E'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Next.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Next.Next.Next, Is.Null);

            m.Add('B');
            Assert.That(m.Head.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('E'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Next.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Next.Next.Next, Is.Null);

            m.Add('A');
            Assert.That(m.Head.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('E'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Next.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Next.Next.Next, Is.Null);

            m.Add('C');
            Assert.That(m.Head.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('E'));
            Assert.That(m.Head.Next.Next.Next.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Next.Next.Next, Is.Null);

            m.Add('A');
            Assert.That(m.Head.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('E'));
            Assert.That(m.Head.Next.Next.Next.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Next.Next.Next, Is.Null);

            m.Add('A');
            Assert.That(m.Head.Item, Is.EqualTo('A'));
            Assert.That(m.Head.Next.Item, Is.EqualTo('C'));
            Assert.That(m.Head.Next.Next.Item, Is.EqualTo('B'));
            Assert.That(m.Head.Next.Next.Next.Item, Is.EqualTo('E'));
            Assert.That(m.Head.Next.Next.Next.Next.Item, Is.EqualTo('D'));
            Assert.That(m.Head.Next.Next.Next.Next.Next, Is.Null);
        }
    }
}
