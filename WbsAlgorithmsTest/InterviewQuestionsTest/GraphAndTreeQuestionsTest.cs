using NUnit.Framework;
using System.Collections.Generic;
using WbsAlgorithms.InterviewQuestions;

namespace WbsAlgorithmsTest.InterviewQuestionsTest
{
    [TestFixture]
    public class GraphAndTreeQuestionsTest
    {
        [Test]
        public void GetLevelsAsLinkedListsTest()
        {
            var tree = new TreeNode("A");
            tree.Left = new TreeNode("B");
            tree.Right = new TreeNode("C");
            tree.Left.Left = new TreeNode("D");
            tree.Left.Right = new TreeNode("E");
            tree.Right.Left = new TreeNode("F");
            tree.Right.Right = new TreeNode("G");
            tree.Left.Left.Left = new TreeNode("H");
            tree.Left.Left.Right = new TreeNode("I");
            tree.Left.Right.Left = new TreeNode("J");

            AssertLevels(GraphAndTreeQuestions.GetLevelsAsLinkedListsUsingDFS(tree));
            AssertLevels(GraphAndTreeQuestions.GetLevelsAsLinkedListsUsingBFS(tree));
        }

        private void AssertLevels(List<LinkedList<TreeNode>> lists)
        {
            Assert.AreEqual(4, lists.Count);

            var L0 = lists[0];
            Assert.AreEqual(1, L0.Count);
            Assert.AreEqual("A", L0.First.Value.Name);

            var L1 = lists[1];
            Assert.AreEqual(2, L1.Count);
            Assert.AreEqual("B", L1.First.Value.Name);
            Assert.AreEqual("C", L1.First.Next.Value.Name);

            var L2 = lists[2];
            Assert.AreEqual(4, L2.Count);
            Assert.AreEqual("D", L2.First.Value.Name);
            Assert.AreEqual("E", L2.First.Next.Value.Name);
            Assert.AreEqual("F", L2.First.Next.Next.Value.Name);
            Assert.AreEqual("G", L2.First.Next.Next.Next.Value.Name);

            var L3 = lists[3];
            Assert.AreEqual(3, L3.Count);
            Assert.AreEqual("H", L3.First.Value.Name);
            Assert.AreEqual("I", L3.First.Next.Value.Name);
            Assert.AreEqual("J", L3.First.Next.Next.Value.Name);
        }
    }
}
