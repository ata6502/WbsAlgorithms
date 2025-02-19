using NUnit.Framework;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    public class TextEditorBufferTest
    {
        [Test]
        public void EmptyBufferTest()
        {
            var b = new TextEditorBuffer();
            Assert.That(b.Size, Is.EqualTo(0));
            Assert.That(b.ToString(), Is.EqualTo(" | "));

            b.Insert('A');
            Assert.That(b.Size, Is.EqualTo(1));

            Assert.That(b.Delete(), Is.EqualTo('A'));
            Assert.That(b.Size, Is.EqualTo(0));
            Assert.That(b.ToString(), Is.EqualTo(" | "));

            Assert.That(b.Delete(), Is.EqualTo(default(char)));
            Assert.That(b.Size, Is.EqualTo(0));
            Assert.That(b.ToString(), Is.EqualTo(" | "));
        }

        [Test]
        public void OneElementTest()
        {
            var b = new TextEditorBuffer();

            b.Insert('A');
            Assert.That(b.Size, Is.EqualTo(1));
            Assert.That(b.ToString(), Is.EqualTo("A | "));

            b.Right(1);
            Assert.That(b.ToString(), Is.EqualTo("A | "));

            b.Left(1);
            Assert.That(b.ToString(), Is.EqualTo(" | A"));

            b.Left(1);
            Assert.That(b.ToString(), Is.EqualTo(" | A"));

            b.Delete();
            Assert.That(b.Size, Is.EqualTo(1));
            Assert.That(b.ToString(), Is.EqualTo(" | A"));

            b.Right(1);
            Assert.That(b.ToString(), Is.EqualTo("A | "));

            Assert.That(b.Delete(), Is.EqualTo('A'));
            Assert.That(b.Size, Is.EqualTo(0));
            Assert.That(b.ToString(), Is.EqualTo(" | "));
        }

        [Test]
        public void TwoElementsTest()
        {
            var b = new TextEditorBuffer();

            b.Insert('A');
            b.Insert('B');
            Assert.That(b.Size, Is.EqualTo(2));
            Assert.That(b.ToString(), Is.EqualTo("A B | "));

            b.Right(1);
            Assert.That(b.ToString(), Is.EqualTo("A B | "));

            b.Left(1);
            Assert.That(b.ToString(), Is.EqualTo("A | B"));

            b.Left(1);
            Assert.That(b.ToString(), Is.EqualTo(" | A B"));

            Assert.That(b.Delete(), Is.EqualTo(default(char)));
            Assert.That(b.Size, Is.EqualTo(2));
            Assert.That(b.ToString(), Is.EqualTo(" | A B"));

            b.Right(1);
            Assert.That(b.ToString(), Is.EqualTo("A | B"));

            Assert.That(b.Delete(), Is.EqualTo('A'));
            Assert.That(b.Size, Is.EqualTo(1));
            Assert.That(b.ToString(), Is.EqualTo(" | B"));
        }

        [Test]
        public void MixedTest()
        {
            var b = new TextEditorBuffer();
            b.Insert('A');
            b.Insert('B');
            b.Insert('C');
            b.Insert('D');
            b.Insert('E');
            b.Insert('F');
            Assert.That(b.ToString(), Is.EqualTo("A B C D E F | "));

            b.Left(4);
            Assert.That(b.ToString(), Is.EqualTo("A B | C D E F"));

            b.Right(3);
            Assert.That(b.ToString(), Is.EqualTo("A B C D E | F"));

            b.Right(1);
            Assert.That(b.ToString(), Is.EqualTo("A B C D E F | "));

            b.Right(1); // try to move cursor beyond the last element
            Assert.That(b.ToString(), Is.EqualTo("A B C D E F | "));

            b.Left(3);
            Assert.That(b.ToString(), Is.EqualTo("A B C | D E F"));

            b.Right(10); // try to move cursor beyond the last element
            Assert.That(b.ToString(), Is.EqualTo("A B C D E F | "));

            b.Left(4);
            Assert.That(b.ToString(), Is.EqualTo("A B | C D E F"));

            b.Left(1);
            Assert.That(b.ToString(), Is.EqualTo("A | B C D E F"));

            b.Left(1);
            Assert.That(b.ToString(), Is.EqualTo(" | A B C D E F"));

            b.Left(1); // try to move cursor before the first element
            Assert.That(b.ToString(), Is.EqualTo(" | A B C D E F"));

            b.Right(3);
            Assert.That(b.ToString(), Is.EqualTo("A B C | D E F"));

            b.Left(15); // try to move cursor before the first element
            Assert.That(b.ToString(), Is.EqualTo(" | A B C D E F"));
            Assert.That(b.Delete(), Is.EqualTo(default(char)));

            b.Right(2);
            Assert.That(b.ToString(), Is.EqualTo("A B | C D E F"));
            Assert.That(b.Delete(), Is.EqualTo('B'));
            Assert.That(b.ToString(), Is.EqualTo("A | C D E F"));
            Assert.That(b.Get(), Is.EqualTo('A'));
            Assert.That(b.ToString(), Is.EqualTo("A | C D E F"));

            b.Right(1);
            Assert.That(b.ToString(), Is.EqualTo("A C | D E F"));
            Assert.That(b.Get(), Is.EqualTo('C'));

            b.Left(10);
            Assert.That(b.ToString(), Is.EqualTo(" | A C D E F"));
            Assert.That(b.Get(), Is.EqualTo(default(char)));
            Assert.That(b.Delete(), Is.EqualTo(default(char)));

            b.Right(10);
            Assert.That(b.ToString(), Is.EqualTo("A C D E F | "));
            Assert.That(b.Get(), Is.EqualTo('F'));
            Assert.That(b.Delete(), Is.EqualTo('F'));
            Assert.That(b.ToString(), Is.EqualTo("A C D E | "));
            Assert.That(b.Delete(), Is.EqualTo('E'));
            Assert.That(b.ToString(), Is.EqualTo("A C D | "));
            Assert.That(b.Delete(), Is.EqualTo('D'));
            Assert.That(b.ToString(), Is.EqualTo("A C | "));
            Assert.That(b.Delete(), Is.EqualTo('C'));
            Assert.That(b.ToString(), Is.EqualTo("A | "));
            Assert.That(b.Delete(), Is.EqualTo('A'));
            Assert.That(b.ToString(), Is.EqualTo(" | "));
            Assert.That(b.Size, Is.EqualTo(0));
        }
    }
}
