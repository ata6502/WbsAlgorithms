using NUnit.Framework;
using System;
using System.Collections.Generic;
using WbsAlgorithms.Collections;

namespace WbsAlgorithmsTest.Collections
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

            Assert.AreEqual(5, q.Size);
            Assert.AreEqual(30, q.Delete(2)); // 10,20,30,40,50 --> 10,20,40,50
            Assert.AreEqual(50, q.Delete(3)); // 10,20,40,50 --> 10,20,40
            Assert.AreEqual(10, q.Delete(0)); // 10,20,40 --> 20,40
            Assert.AreEqual(40, q.Delete(1)); // 20,40 --> 20
            Assert.AreEqual(20, q.Delete(0)); // 20 --> empty
            Assert.AreEqual(0, q.Size);

            Assert.IsTrue(q.IsEmpty);
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
