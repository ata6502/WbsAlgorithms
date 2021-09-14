using NUnit.Framework;
using WbsAlgorithms.Collections;

namespace WbsAlgorithmsTest.Collections
{
    [TestFixture]
    public class TextEditorBufferTest
    {
        [Test]
        public void EmptyBufferTest()
        {
            var b = new TextEditorBuffer();
            Assert.AreEqual(0, b.Size);
            Assert.AreEqual(" | ", b.ToString());

            b.Insert('A');
            Assert.AreEqual(1, b.Size);

            Assert.AreEqual('A', b.Delete());
            Assert.AreEqual(0, b.Size);
            Assert.AreEqual(" | ", b.ToString());

            Assert.AreEqual(default(char), b.Delete());
            Assert.AreEqual(0, b.Size);
            Assert.AreEqual(" | ", b.ToString());
        }

        [Test]
        public void OneElementTest()
        {
            var b = new TextEditorBuffer();

            b.Insert('A');
            Assert.AreEqual(1, b.Size);
            Assert.AreEqual("A | ", b.ToString());

            b.Right(1);
            Assert.AreEqual("A | ", b.ToString());

            b.Left(1);
            Assert.AreEqual(" | A", b.ToString());

            b.Left(1);
            Assert.AreEqual(" | A", b.ToString());

            b.Delete();
            Assert.AreEqual(1, b.Size);
            Assert.AreEqual(" | A", b.ToString());

            b.Right(1);
            Assert.AreEqual("A | ", b.ToString());

            Assert.AreEqual('A', b.Delete());
            Assert.AreEqual(0, b.Size);
            Assert.AreEqual(" | ", b.ToString());
        }

        [Test]
        public void TwoElementsTest()
        {
            var b = new TextEditorBuffer();

            b.Insert('A');
            b.Insert('B');
            Assert.AreEqual(2, b.Size);
            Assert.AreEqual("A B | ", b.ToString());

            b.Right(1);
            Assert.AreEqual("A B | ", b.ToString());

            b.Left(1);
            Assert.AreEqual("A | B", b.ToString());

            b.Left(1);
            Assert.AreEqual(" | A B", b.ToString());

            Assert.AreEqual(default(char), b.Delete());
            Assert.AreEqual(2, b.Size);
            Assert.AreEqual(" | A B", b.ToString());

            b.Right(1);
            Assert.AreEqual("A | B", b.ToString());

            Assert.AreEqual('A', b.Delete());
            Assert.AreEqual(1, b.Size);
            Assert.AreEqual(" | B", b.ToString());
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
            Assert.AreEqual("A B C D E F | ", b.ToString());

            b.Left(4);
            Assert.AreEqual("A B | C D E F", b.ToString());

            b.Right(3);
            Assert.AreEqual("A B C D E | F", b.ToString());

            b.Right(1);
            Assert.AreEqual("A B C D E F | ", b.ToString());

            b.Right(1); // try to move cursor beyond the last element
            Assert.AreEqual("A B C D E F | ", b.ToString());

            b.Left(3);
            Assert.AreEqual("A B C | D E F", b.ToString());

            b.Right(10); // try to move cursor beyond the last element
            Assert.AreEqual("A B C D E F | ", b.ToString());

            b.Left(4);
            Assert.AreEqual("A B | C D E F", b.ToString());

            b.Left(1);
            Assert.AreEqual("A | B C D E F", b.ToString());

            b.Left(1);
            Assert.AreEqual(" | A B C D E F", b.ToString());

            b.Left(1); // try to move cursor before the first element
            Assert.AreEqual(" | A B C D E F", b.ToString());

            b.Right(3);
            Assert.AreEqual("A B C | D E F", b.ToString());

            b.Left(15); // try to move cursor before the first element
            Assert.AreEqual(" | A B C D E F", b.ToString());
            Assert.AreEqual(b.Delete(), default(char));

            b.Right(2);
            Assert.AreEqual("A B | C D E F", b.ToString());
            Assert.AreEqual(b.Delete(), 'B');
            Assert.AreEqual("A | C D E F", b.ToString());
            Assert.AreEqual(b.Get(), 'A');
            Assert.AreEqual("A | C D E F", b.ToString());

            b.Right(1);
            Assert.AreEqual("A C | D E F", b.ToString());
            Assert.AreEqual(b.Get(), 'C');

            b.Left(10);
            Assert.AreEqual(" | A C D E F", b.ToString());
            Assert.AreEqual(b.Get(), default(char));
            Assert.AreEqual(b.Delete(), default(char));

            b.Right(10);
            Assert.AreEqual("A C D E F | ", b.ToString());
            Assert.AreEqual(b.Get(), 'F');
            Assert.AreEqual(b.Delete(), 'F');
            Assert.AreEqual("A C D E | ", b.ToString());
            Assert.AreEqual(b.Delete(), 'E');
            Assert.AreEqual("A C D | ", b.ToString());
            Assert.AreEqual(b.Delete(), 'D');
            Assert.AreEqual("A C | ", b.ToString());
            Assert.AreEqual(b.Delete(), 'C');
            Assert.AreEqual("A | ", b.ToString());
            Assert.AreEqual(b.Delete(), 'A');
            Assert.AreEqual(" | ", b.ToString());
            Assert.AreEqual(0, b.Size);
        }
    }
}
