using System;
using System.Collections.Generic;
using WbsAlgorithms.Common;

namespace WbsAlgorithms.DataStructures
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
    /// [Sedgewick] 1.3.20 - Remove the first occurence of a node with the given value (RemoveByValue).
    /// [Sedgewick] 1.3.21 - Returns true if a node with a given value exists. Otherwise, returns false (DoesNodeExist / Find).
    /// [Sedgewick] 1.3.24 - Remove the node immediately following a given node (RemoveAfter).
    /// [Sedgewick] 1.3.25 - Insert a new node after the given one (InsertAfter).
    /// [Sedgewick] 1.3.26 - Remove all the nodes with a given value (RemoveAllByValue using a while loop and a for loop).
    /// [Sedgewick] 1.3.27 - Find the maximum value iteratively (FindMaxValueIteratively).
    /// [Sedgewick] 1.3.28 - Find the maximum value recursively (FindMaxValueRecursively).
    /// [Sedgewick] 1.3.30 - Reverse iteratively nodes in a linked list (ReverseIteratively).
    /// [Sedgewick] 1.3.30 - Reverse recursively nodes in a linked list (ReverseRecursively).
    /// [Leetcode] #206 - Reverse linked list (iteratively): https://leetcode.com/problems/reverse-linked-list/
    /// [CodingInterview] 2.1 p.94 - Remove duplicates from an unsorted linked list (RemoveDuplicates).
    /// [CodingInterview] 2.2 p.94 - Find the k-th to last element in a linked list (FindFromLast).
    /// [CodingInterview] 2.3 p.94 - Remove a node in the middle i.e., any node but the last one, not necessarily the exact middle (RemoveMiddleNode)
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
            // There is nothing to add.
            if (node == null)
                return head;

            // If the list is empty add the new node as the head.
            if (head == null)
            {
                head = node;
                return head;
            }

            // Find the last node in the list.
            var last = head;
            while (last.Next != null)
                last = last.Next;

            // Add the new node at the end of the linked list.
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

            ListNode<T> node = head;

            // Remove the head if the requested index is 0.
            if (index == 0)
                return node.Next;

            var i = 0;
            while(node.Next != null)
            {
                if (i == index - 1)
                {
                    node.Next = node.Next.Next;
                    return head; // the head remains the same
                }

                node = node.Next;
                ++i;
            }

            throw new ArgumentException($"The node with the given index {index} does not exist in the linked list.");
        }

        /// <summary>
        /// Removes the first occurence of a node with the given value.
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <param name="item">The item we want to remove</param>
        /// <returns>The head of the modified linked list</returns>
        public static ListNode<T> RemoveByValue<T>(ListNode<T> head, T item)
            where T : IComparable<T>
        {
            // If the list is empty, there is nothing to remove.
            if (head == null)
                return null;

            ListNode<T> node = head;

            // Check if the head has the input item. If so, return the next
            // node as the new head.
            if (node.Item.CompareTo(item) == 0)
                return node.Next;

            while(node.Next != null)
            {
                if (node.Next.Item.CompareTo(item) == 0)
                {
                    node.Next = node.Next.Next;
                    return head; // the head remains the same
                }

                // Proceed to the next node.
                node = node.Next;
            }

            throw new ArgumentException($"The value {item} not found in the linked list.");
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

        /// <summary>
        /// Reverses a linked list using an iterative approach. 
        /// 
        /// Example: 
        /// 
        /// Input:
        ///     first = [A] -> [B] -> [C] -> NULL
        ///     reversed = NULL
        /// 
        /// 1st iteration:
        ///     first = [A] -> [B] -> [C] -> NULL
        ///     second = [B] -> [C] -> NULL
        ///     first.Next = NULL
        ///         first = [A] -> NULL
        ///     reversed = first
        ///         reversed = [A] -> NULL
        ///     first = second
        ///         first = [B] -> [C] -> NULL
        ///         
        /// 2nd iteration:
        ///     first = [B] -> [C] -> NULL
        ///     second = [C] -> NULL
        ///     first.Next = [A] -> NULL
        ///         first = [B] -> [A] -> NULL
        ///     reversed = first
        ///         reversed = [B] -> [A] -> NULL
        ///     first = second
        ///         first = [C] -> NULL
        ///         
        /// 3nd iteration:
        ///     first = [C] -> NULL
        ///     second = NULL
        ///     first.Next = [B] -> [A] -> NULL
        ///         first = [C] -> [B] -> [A] -> NULL
        ///     reversed = first
        ///         reversed = [C] -> [B] -> [A] -> NULL
        ///     first = second
        ///         first = NULL
        ///         
        /// end of while loop
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <returns>The head of the reversed linked list</returns>
        public static ListNode<T> ReverseIteratively<T>(ListNode<T> head)
        {
            // Maintain references to three consecutive nodes in the linked list:
            // reversed, first, and second.
            // Also, maintain the invariant that:
            // - 'first' is the node of what's left of the original list
            // - 'second' is the second node of what's left of the original list
            // - 'reversed' is the first node of the resulting list.

            ListNode<T> reversed = null; // previous
            ListNode<T> first = head;    // current
            ListNode<T> second = null;   // next

            while (first != null)
            {
                // Keep the pointer to the second node as we're going to override it.
                second = first.Next;

                // Insert the 'first' node at the beginning of the reversed list.
                first.Next = reversed;
                reversed = first;

                // Advance to the next node by removing the first node from the original list.
                first = second;
            }

            return reversed;
        }

        /// <summary>
        /// Reverses a linked list using a recursive approach.
        /// Note: There is a different solution in Sedgewick p.166
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <returns>The head of the reversed linked list</returns>
        public static ListNode<T> ReverseRecursively<T>(ListNode<T> first, ListNode<T> reversed = null)
        {
            // base case
            if (first == null)
                return reversed;

            // This part of code is the same as in ReverseIteratively.
            var second = first.Next;
            first.Next = reversed;
            reversed = first;
            first = second;

            return ReverseRecursively(first, reversed);
        }

        /// <summary>
        /// Creates a singly-linked list and populates it with given items.
        /// </summary>
        /// <param name="items">Items to populate the linked list</param>
        /// <returns>The head of the newly created linked list</returns>
        public static ListNode<T> Create<T>(T[] items)
        {
            ListNode<T> head = null;
            ListNode<T> prev = null;

            foreach (var item in items)
            {
                var node = new ListNode<T>(item);

                if (prev == null)
                    head = node;
                else
                    prev.Next = node;

                prev = node;
            }

            return head;
        }

        /// <summary>
        /// Remove duplicates from an unsorted linked list. 
        /// Use a HashSet to keep track of duplicated items.
        /// Time complexity O(N), where N is the number of nodes in the linked list.
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <returns>The head of the linked list</returns>
        public static ListNode<T> RemoveDuplicates<T>(ListNode<T> head)
            where T : IComparable<T>
        {
            if (head == null)
                return head;

            var h = new HashSet<T>();

            var node = head;
            h.Add(node.Item);

            // Iterate over the linked list.
            while(node.Next != null)
            {
                if (h.TryGetValue(node.Next.Item, out _))
                    // Remove the node if it has a duplicated value.
                    node.Next = node.Next.Next;
                else
                {
                    // Keep track of the items.
                    h.Add(node.Next.Item);
                    node = node.Next;
                }
            }

            return head;
        }

        /// <summary>
        /// Remove duplicates from an unsorted linked list.
        /// Use a "runner technique" by using two pointers:
        /// - one pointer iterates through the linked list
        /// - another pointer (the runner) checks subsequent nodes for duplicates
        /// Time complexity O(N^2); space complexity O(1)
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <returns>The head of the linked list</returns>
        public static ListNode<T> RemoveDuplicatesUsingRunner<T>(ListNode<T> head)
            where T : IComparable<T>
        {
            if (head == null)
                return head;

            var node = head;

            // Iterate over the linked list.
            while (node != null)
            {
                var runner = node;

                // Iterate over subsequent nodes.
                while (runner.Next != null)
                {
                    if (node.Item.CompareTo(runner.Next.Item) == 0)
                        // Remove the node pointed by runner.Next if it has
                        // the same value as the node.
                        runner.Next = runner.Next.Next;
                    else
                        runner = runner.Next;
                }

                node = node.Next;
            }

            return head;
        }

        /// <summary>
        /// Finds the k-th to last node in a linked list. Uses an iterative approach.
        /// Time complexity: O(n); space: O(1)
        /// </summary>
        /// <param name="head">The head of the linked list</param>
        /// <param name="k">Specifies the position of a node to find. k = 1 returns the last node, k = 2 returns the second to the last node, etc.</param>
        /// <returns>The k-th to last node or null if the size of the list is less than k</returns>
        public static ListNode<T> FindFromLast<T>(ListNode<T> head, int k)
        {
            if (head == null)
                return null;

            var current = head;

            // Wind up the runner k nodes forward. This places the runner and
            // the current pointer k nodes apart.
            var i = 0;
            var runner = head;
            while(runner != null && i < k)
            {
                runner = runner.Next;
                ++i;
            }

            // i < k means that k is out of bounds: there are fewer nodes
            // in the list than k.
            if (i < k)
                return null;

            // Move both pointers at the same pace. If the runner hits the end
            // of the list after k steps, the current pointer will be len - k
            // nodes into the list where len is the number of nodes in the list.
            while(runner != null)
            {
                current = current.Next;
                runner = runner.Next;
            }

            // The current pointer is k nodes from the end of the list.
            return current;
        }

        /// <summary>
        /// Remove a node in the middle of a linked list i.e., any node but the last one, 
        /// not necessarily the exact middle. 
        /// </summary>
        /// <param name="node">A node to remove</param>
        /// <returns>A node after the removed node</returns>
        public static ListNode<T> DeleteMiddleNode<T>(ListNode<T> node)
        {
            if (node == null)
                return node;

            if (node.Next == null)
                throw new ArgumentException("Cannot remove the last node.");

            // Override the node we want to delete with the next node.
            node.Item = node.Next.Item;
            node.Next = node.Next.Next;

            return node;
        }
    }
}
