using System;

namespace WbsAlgorithms.Collections
{
    /// <summary>
    /// [Sedgewick] 1.3.44 p.170 - Develop a data type for a buffer in 
    /// a text editor that implements the following API:
    /// 
    /// int  Size()          // return the number of characters in the buffer
    /// void Insert(char c)  // insert a character c at the cursor position
    /// char Get()           // get the character at the cursor position
    /// char Delete()        // delete and return the character at the cursor position
    /// void Left(int k)     // move the cursor k positions to the left
    /// void Right(int k)    // move the cursor k positions to the right
    /// </summary>
    public class TextEditorBuffer
    {
        // Stores the characters on the left from the cursor position.
        // The top of the _left stack refers to the current cursor position.
        private StackLinkedList<char> _left;

        // Stores the character on the right from the cursor position.
        // The top of the _right stack refers to the first character on the right from the cursor.
        private StackLinkedList<char> _right;

        /// <summary>
        /// Returns the number of characters in the buffer.
        /// </summary>
        public int Size => _left.Size + _right.Size;

        public TextEditorBuffer()
        {
            _left = new StackLinkedList<char>();
            _right = new StackLinkedList<char>();
        }

        /// <summary>
        /// Inserts a character at the cursor position.
        /// </summary>
        /// <param name="c">The character to insert</param>
        public void Insert(char c)
        {
            _left.Push(c);
        }

        /// <summary>
        /// Gets the character at the cursor position.
        /// </summary>
        /// <returns>The character at the cursor position. Returns '\0' if the buffer is empty.</returns>
        public char Get()
        {
            if (_left.Size == 0)
                return default(char);

            return _left.Peek();
        }

        /// <summary>
        /// Deletes and returns the character at the cursor position.
        /// </summary>
        /// <returns>The character at the cursor position. Returns '\0' if the cursor is at the beginning of the buffer.</returns>
        public char Delete()
        {
            if (_left.Size == 0)
                return default(char);

            return _left.Pop();
        }

        /// <summary>
        /// Move the cursor k positions to the left.
        /// </summary>
        /// <param name="k">The number of positions to move the cursor</param>
        public void Left(int k)
        {
            if (Size == 0)
                return;

            // Determine the number of positions the cursor has to be moved.
            // Take into account that there may be not enough characters on
            // the left to move the cursor.
            var n = Math.Min(_left.Size, k);

            // Move the cursor to the left.
            for (var i = 0; i < n; ++i)
                _right.Push(_left.Pop());
        }

        public void Right(int k)
        {
            if (Size == 0)
                return;

            // Determine the number of positions the cursor has to be moved.
            // Take into account that there may be not enough characters on
            // the right to move the cursor.
            var n = Math.Min(_right.Size, k);

            // Move the cursor to the right.
            for (var i = 0; i < n; ++i)
                _left.Push(_right.Pop());
        }

        /// <summary>
        /// Shows the characters in the buffer. The character '|' marks the current
        /// position of the cursot.
        /// </summary>
        /// <returns>Formatted content of the buffer</returns>
        public override string ToString()
        {
            return string.Join(' ', ReverseString(_left.ToString())) + " | " + string.Join(' ', _right.ToString());
        }

        private string ReverseString(string s)
        {
            char[] a = s.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }
    }
}
