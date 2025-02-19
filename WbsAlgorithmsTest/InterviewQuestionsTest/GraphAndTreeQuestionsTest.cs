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
            Assert.That(lists.Count, Is.EqualTo(4));

            var L0 = lists[0];
            Assert.That(L0.Count, Is.EqualTo(1));
            Assert.That(L0.First.Value.Name, Is.EqualTo("A"));

            var L1 = lists[1];
            Assert.That(L1.Count, Is.EqualTo(2));
            Assert.That(L1.First.Value.Name, Is.EqualTo("B"));
            Assert.That(L1.First.Next.Value.Name, Is.EqualTo("C"));

            var L2 = lists[2];
            Assert.That(L2.Count, Is.EqualTo(4));
            Assert.That(L2.First.Value.Name, Is.EqualTo("D"));
            Assert.That(L2.First.Next.Value.Name, Is.EqualTo("E"));
            Assert.That(L2.First.Next.Next.Value.Name, Is.EqualTo("F"));
            Assert.That(L2.First.Next.Next.Next.Value.Name, Is.EqualTo("G"));

            var L3 = lists[3];
            Assert.That(L3.Count, Is.EqualTo(3));
            Assert.That(L3.First.Value.Name, Is.EqualTo("H"));
            Assert.That(L3.First.Next.Value.Name, Is.EqualTo("I"));
            Assert.That(L3.First.Next.Next.Value.Name, Is.EqualTo("J"));
        }
    }
}
