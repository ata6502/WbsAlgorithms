using System;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// [Sedgewick] A linked list is a recursive data structure that is either empty or 
    /// a reference to a node having an item and a reference to a linked list.
    /// 
    /// Provided that we have access to the first element of a linked list (head)
    /// and the last element (tail) the following operations are easy to implement:
    /// - Insert a node at the beginning (InsertFirst)
    /// - Remove a node from the beginning (RemoveFirst)
    /// - Insert a node at the end (AddLast)
    /// 
    /// Other operations such as inserting or removing an arbitrary node are not trivial.
    /// 
    /// Linked-list exercises p.164-166
    /// [Sedgewick] 1.3.19 - Remove the last node (RemoveLast).
    /// [Sedgewick] 1.3.20 - Remove a node at a given index (RemoveByIndex).
    ///                      Remove the first occurence of a node with the given value (RemoveByValue).
    /// [Sedgewick] 1.3.21 - Returns true if a node with a given value exists. Otherwise, returns false (DoesNodeExist / Find).
    /// [Sedgewick] 1.3.24 - Remove the node immediately following a given node (RemoveAfter).
    /// [Sedgewick] 1.3.25 - Insert a new node after the given one (InsertAfter).
    /// [Sedgewick] 1.3.26 - Remove all the nodes with a given value (RemoveAllByValue using a while loop and a for loop).
    /// [Sedgewick] 1.3.27 - Find the maximum value iteratively (FindMaxValueIteratively).
    /// [Sedgewick] 1.3.28 - Find the maximum value recursively (FindMaxValueRecursively).
    /// </summary>
    public class SinglyLinkedList
    {
        /// <summary>
        /// Inserts a new node at the beginning of a linked list.
        /// </summary>
        /// <param name="head">The head of a linked list to modify</param>
        /// <param name="node">A node to insert</param>
        /// <returns>The head of the modified linked list</returns>
        public static ListNode<T> InsertFirst<T>(ListNode<T> head, ListNode<T> node)
        {
            // nothing to insert
            if (node == null)
                return head;

            node.Next = head;
            head = node;

            return head;
        }

        /// <summary>
        /// Removes a node from the beginning of a linked list.
        /// </summary>
        /// <param name="head">The head of a linked list to modify</param>
        /// <returns>The head of the modified linked list</returns>
        public static ListNode<T> RemoveFirst<T>(ListNode<T> head)
        {
            // nothing to remove
            if (head == null)
                return null;

            head = head.Next;

            return head;
        }

        /// <summary>
        /// Adds a given node at the end of a linked list.
        /// </summary>
        /// <param name="head">The head of a linked list to modify</param>
        /// <param name="node">A node to add</param>
        /// <returns>The head of the modified linked list</returns>
        public static ListNode<T> AddLast<T>(ListNode<T> head, ListNode<T> node)
        {
            // nothing to add
            if (node == null)
                return head;

            // add the input node to an empty list
            if (head == null)
            {
                head = node;
                return head;
            }

            // find the last node
            var last = head;
            while (last.Next != null)
                last = last.Next;

            // add the input node at the end of the linked list
            last.Next = node;

            return head;
        }

        /// <summary>
        /// Removes the last node from a linked list
        /// </summary>
        /// <param name="head">The head of a linked list to modify</param>
        /// <returns>The head of the modified linked list</returns>
        public static ListNode<T> RemoveLast<T>(ListNode<T> head)
        {
            // If the list is empty, there is nothing to remove.
            if (head == null)
                return null;

            // If the list has just one node (the head), remove it.
            if (head.Next == null)
            {
                head = null;
                return head;
            }

            // Find the second-last node i.e., the node before the last one.
            var node = head;
            while (node.Next.Next != null)
                node = node.Next;

            // Remove the link to the last node by modifying the second-last node.
            node.Next = null;

            return head;
        }

        /// <summary>
        /// Removes a node from a linked list at a given index, if the node exists.
        /// </summary>
        /// <param name="head">The head of a linked list to modify</param>
        /// <param name="index">The zero-based index of a node to delete</param>
        /// <returns>The head of the modified linked list</returns>
        public static ListNode<T> RemoveByIndex<T>(ListNode<T> head, int index)
        {
            // If the list is empty, there is nothing to remove.
            if (head == null)
                return null;

            // Try to find a node at the given index. Also, keep the node previous to the found one.
            ListNode<T> prev = null;
            ListNode<T> node = head;
            var i = 0;
            while (node != null && i < index)
            {
                prev = node;
                node = node.Next;
                ++i;
            }

            if (node == null)
                throw new ArgumentException($"The node with the given index {index} does not exist in the linked list.");
            else
            {
                head = RemoveNode(head, prev, node);
                return head;
            }
        }

        /// <summary>
        /// Removes the first occurence of a node with the given value.
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <param name="item">The value of the item we want to remove</param>
        /// <returns></returns>
        public static ListNode<T> RemoveByValue<T>(ListNode<T> head, T item)
            where T : IComparable<T>
        {
            // If the list is empty, there is nothing to remove.
            if (head == null)
                return null;

            // Try to find a node with the value 'item'. Also, keep the node previous to the found one.
            ListNode<T> prev = null;
            ListNode<T> node = head;

            while (node != null && node.Item.CompareTo(item) != 0)
            {
                prev = node;
                node = node.Next;
            }

            if (node == null || node.Item.CompareTo(item) != 0)
                throw new ArgumentException($"The value {item} not found in the linked list.");
            else
            {
                head = RemoveNode(head, prev, node);
                return head;
            }
        }

        /// <summary>
        /// A helper method used by RemoveByIndex and RemoveByValue.
        /// </summary>
        /// <param name="head">The head of a linked list to modify</param>
        /// <param name="prev">The node before the one to remove</param>
        /// <param name="node">The node to remove</param>
        /// <returns></returns>
        private static ListNode<T> RemoveNode<T>(ListNode<T> head, ListNode<T> prev, ListNode<T> node)
        {
            // There are four possibilities:

            // 1. The found node is the first one. Remove the first node.
            if (prev == null && node != null && node.Next != null)
            {
                head = node.Next;
            }
            // 2. The found node is the last one. Remove the last node.
            else if (prev != null && node != null && node.Next == null)
            {
                prev.Next = null;
            }
            // 3. The found node is somewhere in the middle of the list.
            else if (prev != null && node != null && node.Next != null)
            {
                prev.Next = node.Next;
            }
            // 4. Otherwise, there is only one node. Remove it.
            else if (prev == null && node != null && node.Next == null)
            {
                head = null;
            }

            return head;
        }

        /// <summary>
        /// Checks if a node with a given value exists in a linked list.
        /// </summary>
        /// <param name="head">The head of the linek list</param>
        /// <param name="item">The value of a node we want to find</param>
        /// <returns>Returns true if a node with a given value exists. Otherwise, returns false.</returns>
        public static bool DoesNodeExist<T>(ListNode<T> head, T item)
            where T : IComparable<T>
        {
            if (head == null)
                return false;

            var node = head;
            while (node != null)
            {
                if (node.Item.CompareTo(item) == 0)
                    return true;

                node = node.Next;
            }

            return false;
        }

        /// <summary>
        /// Removes a node immediately following a given node.
        /// </summary>
        /// <param name="node">A given node after which we want to delete a node</param>
        /// <returns>The input node</returns>
        public static ListNode<T> RemoveAfter<T>(ListNode<T> node)
        {
            // If the list is empty or the given node is the last one, there is nothing to remove.
            if (node == null || node.Next == null)
                return node;

            node.Next = node.Next.Next;

            return node;
        }

        /// <summary>
        /// Inserts the second node after the first one. 
        /// </summary>
        /// <param name="node1">The first node</param>
        /// <param name="node2">The second node</param>
        /// <returns>The first node</returns>
        public static ListNode<T> InsertAfter<T>(ListNode<T> node1, ListNode<T> node2)
        {
            // Do nothing if either argument is null.
            if (node1 == null || node2 == null)
                return node1;

            node2.Next = node1.Next;
            node1.Next = node2;

            return node1;
        }

        /// <summary>
        /// Removes all of the nodes in the list that have the value 'item'. Uses a while loop to accomplish the task.
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <param name="item">The value of the items we want to remove</param>
        /// <returns>The head of the linked list</returns>
        public static ListNode<T> RemoveAllByValueUsingWhileLoop<T>(ListNode<T> head, T item)
            where T : IComparable<T>
        {
            ListNode<T> prev = null; // previous node
            ListNode<T> node = head; // current node

            // Loop until the end of the linked list.
            while (node != null)
            {
                // Check if the current node has the value we are looking for.
                if (node.Item.CompareTo(item) == 0)
                {
                    // Check if the current node is the first node in the list.
                    if (prev == null)
                    {
                        // If so, remove the first node from the list.
                        head = node.Next;
                    }
                    else
                    {
                        // Otherwise, remove the current node.
                        prev.Next = node.Next;
                    }
                }
                else
                {
                    // Update a pointer to the previous node only if the current node's value
                    // is different from the one we are looking for.
                    prev = node;
                }

                // Set the next current node.
                node = node.Next;
            }

            return head;
        }

        /// <summary>
        /// Removes all of the nodes in the list that have the value 'item'. Uses a for loop to accomplish the task.
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <param name="item">The value of the items we want to remove</param>
        /// <returns>The head of the linked list</returns>
        public static ListNode<T> RemoveAllByValueUsingForLoop<T>(ListNode<T> head, T item)
            where T : IComparable<T>
        {
            // Return if the list is empty.
            if (head == null)
                return head;

            ListNode<T> prev = null;

            for (var node = head; node != null; node = node.Next)
            {
                if (node.Item.CompareTo(item) == 0)
                {
                    if (prev == null)
                    {
                        // Remove the first node.
                        head = head.Next;
                    }
                    else
                    {
                        // Remove the node.
                        prev.Next = prev.Next.Next;
                    }
                }
                else
                {
                    prev = node;
                }
            }

            return head;
        }

        /// <summary>
        /// Find the maximum value in a linked list using an iterative approach. 
        /// All items in the linked list are positive integers.
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <returns>The maximum value in the linked list. Zero, if the list is empty.</returns>
        public static int FindMaxValueIteratively(ListNode<int> head)
        {
            // Return 0 if the list is empty.
            if (head == null)
                return 0;

            var node = head;
            var max = node.Item;

            while(node != null)
            {
                max = Math.Max(max, node.Item);
                node = node.Next;
            }

            return max;
        }

        /// <summary>
        /// Find the maximum value in a linked list using a recursive approach. 
        /// All items in the linked list are positive integers.
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <returns>The maximum value in the linked list. Zero, if the list is empty.</returns>
        public static int FindMaxValueRecusively(ListNode<int> head)
        {
            // Return 0 if the list is empty.
            if (head == null)
                return 0;

            var node = head;
            return FindMaxValueRecusively(node, node.Item);
        }

        private static int FindMaxValueRecusively(ListNode<int> node, int max)
        {
            // base case
            if (node == null)
                return max;

            max = Math.Max(max, node.Item);
            return FindMaxValueRecusively(node.Next, max);
        }
    }
}
