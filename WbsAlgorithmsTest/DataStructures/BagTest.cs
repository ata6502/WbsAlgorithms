using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    public class BagTest
    {
        private const int RandomBagSize = 4;
        private const int RandomBagTestCaseCount = 100000;

        private Dictionary<string, int> _permutations = new Dictionary<string, int>()
        {
            {"1234", 0}, {"2134", 0}, {"3124", 0}, {"1324", 0}, {"2314", 0}, {"3214", 0},
            {"4213", 0}, {"2413", 0}, {"1423", 0}, {"4123", 0}, {"2143", 0}, {"1243", 0},
            {"1342", 0}, {"3142", 0}, {"4132", 0}, {"1432", 0}, {"3412", 0}, {"4312", 0},
            {"4321", 0}, {"3421", 0}, {"2431", 0}, {"4231", 0}, {"3241", 0}, {"2341", 0}
        };

        [Test]
        public void AddElementsTest()
        {
            var b = new BagLinkedList<int>();

            Assert.That(b.IsEmpty, Is.True);

            var elementsToAdd = new List<int> { 4, 1, 3, 6, 8 };

            foreach (var v in elementsToAdd)
                b.Add(v);

            Assert.That(b.IsEmpty, Is.False);
            Assert.That(b.Size, Is.EqualTo(5));

            var elementsInBag = new List<int>(b.Size);
            foreach (var v in b)
                elementsInBag.Add(v);

            // Compare both collections.
            Assert.That(
                elementsInBag.OrderBy(v => v), 
                Is.EqualTo(elementsToAdd.OrderBy(v => v)).AsCollection);
        }

        [Test]
        public void AllPermutationsShouldBeEquallyLikely()
        {
            var bag = new BagRandom<int>(RandomBagSize);

            bag.Add(1);
            bag.Add(2);
            bag.Add(3);
            bag.Add(4);

            var sb = new StringBuilder();
            for (var i = 0; i < RandomBagTestCaseCount; ++i)
            {
                sb.Clear();
                foreach (var n in bag)
                    sb.Append(n.ToString());

                _permutations[sb.ToString()]++;
            }

            // The expected value is TestCaseCount / # of permutations, for example: 
            // 1000 / 24 = 41.67
            // 10,000 / 24 = 416.67
            // 100,000 / 24 = 4166.67

            double expected = (double)RandomBagTestCaseCount / _permutations.Count;
            double delta = expected * 0.05; // +/- 5%
            foreach (var p in _permutations)
            {
                Assert.That(p.Value, Is.EqualTo(expected).Within(delta));
            }
        }
    }
}
