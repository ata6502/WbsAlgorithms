using System.Collections.Generic;

namespace WbsAlgorithms.InterviewQuestions
{
    // Questions:
    // 1. How to create linked lists of all the nodes in a binary tree at each depth? [ListOfDepths/GetLevels] [CodingInterview] 4.3 p.109
    public class GraphAndTreeQuestions
    {
        // Given a binary tree, design an algorithm which creates a linked list of all
        // the nodes at each depth e.g., if you have a tree with depth d, you will have
        // d linked lists.

        // This implementation uses the pre-order tree traversal and depth-first search.
        public static List<LinkedList<TreeNode>> GetLevelsAsLinkedListsUsingDFS(TreeNode root)
        {
            var lists = new List<LinkedList<TreeNode>>();
            GetLevelsRecursive(root, 0);
            return lists;

            void GetLevelsRecursive(TreeNode treeNode, int level)
            {
                // Base case
                if (treeNode == null)
                    return;

                LinkedList<TreeNode> list;

                // Check if the level is already in the list of linked lists.
                if (level < lists.Count)
                {
                    // If so, obtain the linked list for the given level.
                    list = lists[level];
                }
                else
                {
                    // If not, and this is the first time we have visited level n,
                    // we must have seen levels 0,1,...,n-1 because levels are traversed
                    // in order in the pre-order tree traversal approach.
                    // It means, we can simply add the level at the end.
                    list = new LinkedList<TreeNode>();
                    lists.Add(list);
                }

                // Follow the pre-order tree traversal.
                list.AddLast(treeNode);
                GetLevelsRecursive(treeNode.Left, level + 1);
                GetLevelsRecursive(treeNode.Right, level + 1);
            }
        }

        // This implementation visits the input tree level-by-level. It uses breath-first search.
        public static List<LinkedList<TreeNode>> GetLevelsAsLinkedListsUsingBFS(TreeNode root)
        {
            var lists = new List<LinkedList<TreeNode>>();
            var currentLevel = new LinkedList<TreeNode>();

            if (root != null)
                currentLevel.AddLast(root);

            while (currentLevel.Count > 0)
            {
                // Keep the previous level.
                lists.Add(currentLevel);

                // Go to the next level.
                LinkedList<TreeNode> parents = currentLevel;

                // This linked list will contain all the nodes from the given level.
                currentLevel = new LinkedList<TreeNode>();

                foreach(var parent in parents)
                {
                    // Add the children to the current level linked list.
                    if (parent.Left != null)
                        currentLevel.AddLast(parent.Left);
                    if (parent.Right != null)
                        currentLevel.AddLast(parent.Right);
                }
            }

            return lists;
        }
    }

    public class TreeNode
    {
        public string Name { get; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(string name) => Name = name;

        public override string ToString() => Name;
    }
}
