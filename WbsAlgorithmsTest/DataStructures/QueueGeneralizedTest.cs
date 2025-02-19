using NUnit.Framework;
using System;
using System.Collections.Generic;
using WbsAlgorithms.DataStructures;

namespace WbsAlgorithmsTest.DataStructures
{
    [TestFixture]
    class QueueGeneralizedTest
    {
        [TestCaseSource(nameof(QueueGeneralizedDataSource))]
        public void QueueGeneralizedUsingLinkedListTest(IQueueGeneralized<int> q)
        {
            q.Insert(10); // index 0
            q.Insert(20); // index 1
            q.Insert(30); // index 2
            q.Insert(40); // index 3
            q.Insert(50); // index 4

            Assert.That(q.Size, Is.EqualTo(5));
            Assert.That(q.Delete(2), Is.EqualTo(30)); // 10,20,30,40,50 --> 10,20,40,50
            Assert.That(q.Delete(3), Is.EqualTo(50)); // 10,20,40,50 --> 10,20,40
            Assert.That(q.Delete(0), Is.EqualTo(10)); // 10,20,40 --> 20,40
            Assert.That(q.Delete(1), Is.EqualTo(40)); // 20,40 --> 20
            Assert.That(q.Delete(0), Is.EqualTo(20)); // 20 --> empty
            Assert.That(q.Size, Is.EqualTo(0));

            Assert.That(q.IsEmpty, Is.True);
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Delete(0));

            q.Insert(60);
            Assert.Throws<ArgumentOutOfRangeException>(() => q.Delete(8));
        }

        private static IEnumerable<TestCaseData> QueueGeneralizedDataSource()
        {
            yield return new TestCaseData(new QueueGeneralizedUsingArray<int>(3)).SetName(nameof(QueueGeneralizedUsingArray<int>) + "Test");
            yield return new TestCaseData(new QueueGeneralizedUsingLinkedList<int>()).SetName(nameof(QueueGeneralizedUsingLinkedList<int>) + "Test");
        }
    }
}
