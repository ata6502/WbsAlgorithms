using WbsAlgorithms.Common;

namespace WbsAlgorithms.Arithmetic
{
    /// <summary>
    /// You are given two non-empty linked lists representing two non-negative integers.
    /// The digits are stored in reverse order and each of their nodes contain a single digit.
    /// Add the two numbers and return it as a linked list.
    /// 
    /// You may assume the two numbers do not contain any leading zero, except the number 0 itself.
    /// 
    /// Example:
    /// 
    /// Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
    /// Output: 7 -> 0 -> 8
    /// Explanation: 342 + 465 = 807.
    /// 
    /// [Leetcode] #2 - Add two numbers given as two linked lists: https://leetcode.com/problems/add-two-numbers/
    /// </summary>
    public class AddTwoNumbersLinkedList
    {
        /// <summary>
        /// This algorithm does not use a dummy head but it has to maintain a previous node.
        /// </summary>
        /// <param name="l1">The head of a linked-list containing the first number</param>
        /// <param name="l2">The head of a linked-list containing the second number</param>
        /// <returns>The head of the linked-list containing the result</returns>
        public static ListNode<int> AddTwoNumbers(ListNode<int> l1, ListNode<int> l2)
        {
            ListNode<int> curr = new ListNode<int>(0);
            ListNode<int> prev = null;
            ListNode<int> result = curr;
            var carry = 0;

            while (l1 != null || l2 != null)
            {
                var sum =
                    (l1 != null ? l1.Item : 0) +
                    (l2 != null ? l2.Item : 0) + carry;

                curr.Item = sum % 10;
                carry = sum / 10;

                if (prev != null)
                    prev.Next = curr;

                prev = curr;
                curr = new ListNode<int>(0);

                if (l1 != null)
                    l1 = l1.Next;
                if (l2 != null)
                    l2 = l2.Next;
            }

            // Take care about a possible carry.
            if (carry > 0 && prev != null)
            {
                curr.Item = carry;
                prev.Next = curr;
            }

            return result;
        }

        /// <summary>
        /// This algorithm uses a dummy head which somewhat simplifies the algorithm.
        /// </summary>
        /// <param name="l1">The head of a linked-list containing the first number</param>
        /// <param name="l2">The head of a linked-list containing the second number</param>
        /// <returns>The head of the linked-list containing the result</returns>
        public static ListNode<int> AddTwoNumbersDummyHead(ListNode<int> l1, ListNode<int> l2)
        {
            ListNode<int> dummyHead = new ListNode<int>(0);
            ListNode<int> p = l1, q = l2, curr = dummyHead;
            var carry = 0;
            var sum = 0;

            while (p != null || q != null)
            {
                var x = (p != null ? p.Item : 0);
                var y = (q != null ? q.Item : 0);

                sum = x + y + carry;

                carry = sum / 10;

                curr.Next = new ListNode<int>(sum % 10);
                curr = curr.Next;

                if (p != null)
                    p = p.Next;

                if (q != null)
                    q = q.Next;
            }

            // Take care about a possible carry.
            if (carry == 1)
            {
                curr.Next = new ListNode<int>(1);
            }

            return dummyHead.Next;
        }
    }
}
