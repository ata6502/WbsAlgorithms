using System.Diagnostics;

namespace WbsAlgorithms.Arithmetic
{
    public class MatrixMultiplication
    {
        /// <summary>
        /// Multiplies two square matrices using the Strassen algorithm.
        /// 
        /// The Strassen algorithm has one recursive call fewer than the recursive algorithm.
        /// Instead of that, the Strassen algorithm uses a constant number of matrix additions 
        /// and subtractions.
        /// 
        /// Saving one recursive call not only reduces the running time of the matrix multiplication 
        /// procedure by 12.5% but also the time savings are compounded i.e., the recursive call is 
        /// saved over and over again. This yields an algorithm with subcubic running time.
        ///
        /// Assumption: n is a power of 2
        /// Running time: O(n^log(7)) can be calculated from the Master Method
        /// 
        /// [AlgoIlluminated-1] p.71-77 3.3 Strassen's Matrix Multiplication Algorithm (Strassen p.75)
        /// </summary>
        /// <param name="n">The size of both input matrices n x n. The value of n has to be a power of 2.</param>
        /// <param name="x">An n x n matrix</param>
        /// <param name="y">An n x n matrix</param>
        /// <returns>A product matrix Z = X · Y</returns>
        public static double[,] MultiplyStrassen(int n, double[,] x, double[,] y)
        {
            if (n == 1)
                return new double[,] { { x[0, 0] * y[0, 0] } }; // 1x1 matrix
            else
            {
                n = n / 2;

                var a = SubMatrix(x, 0, 0, n, n);
                var b = SubMatrix(x, 0, n, n, n);
                var c = SubMatrix(x, n, 0, n, n);
                var d = SubMatrix(x, n, n, n, n);

                var e = SubMatrix(y, 0, 0, n, n);
                var f = SubMatrix(y, 0, n, n, n);
                var g = SubMatrix(y, n, 0, n, n);
                var h = SubMatrix(y, n, n, n, n);

                // Recursively compute seven products involving the matrices A,B,...,H
                var p1 = MultiplyStrassen(n, a, SubtractMatrices(f, h));
                var p2 = MultiplyStrassen(n, AddMatrices(a, b), h);
                var p3 = MultiplyStrassen(n, AddMatrices(c, d), e);
                var p4 = MultiplyStrassen(n, d, SubtractMatrices(g, e));
                var p5 = MultiplyStrassen(n, AddMatrices(a, d), AddMatrices(e, h));
                var p6 = MultiplyStrassen(n, SubtractMatrices(b, d), AddMatrices(g, h));
                var p7 = MultiplyStrassen(n, SubtractMatrices(a, c), AddMatrices(e, f));

                // Compute quadrants of the product matrix.
                var aebg = AddMatrices(SubtractMatrices(AddMatrices(p5, p4), p2), p6);
                var afbh = AddMatrices(p1, p2);
                var cedg = AddMatrices(p3, p4);
                var cfdh = SubtractMatrices(SubtractMatrices(AddMatrices(p1, p5), p3), p7);

                // Return the appropriate additions and subtractions of the matrices 
                // computed in the previous step.
                var xy = CombineMatrices(n, aebg, afbh, cedg, cfdh);

                return xy;
            }
        }

        /// <summary>
        /// Multiplies two square matrices using a divide-and-conquer approach.
        /// 
        /// Assumption: n is a power of 2
        /// Running time: O(n^3) can be calculated from the Master Method
        /// 
        /// An explanation of the shrinkage of the matrix's size vs. shrinkage of 
        /// the number of elements from the book [AlgoIlluminated-1]:
        /// 
        ///     "A 4x4 matrix has 16 elements. The size of that matrix is n=4. 
        ///      When we divide the matrix into 4 smaller submatrices, we get 4 
        ///      matrices with dimensions 2x2, with a total of 4 elements each. 
        ///      Note that the amount of elements in the submatrices shrinkage 
        ///      ratio is 1/4. However, the matrix size shrinkage ratio is 1/2."
        ///
        /// [AlgoIlluminated-1] p.73-75 3.3.4 Matrix Multiplication - A Divide-and-Conquer Approach (RecMatMult)
        /// </summary>
        /// <param name="n">The size of both input matrices n x n. The value of n has to be a power of 2.</param>
        /// <param name="x">An n x n matrix</param>
        /// <param name="y">An n x n matrix</param>
        /// <returns>A product matrix Z = X · Y</returns>
        public static double[,] MultiplyRecursive(int n, double[,] x, double[,] y)
        {
            // Base case.
            if (n == 1)
                return new double[,] { { x[0, 0] * y[0, 0] } }; // 1x1 matrix
            else
            {
                // Half the matrix size.
                n = n / 2;

                // Eight recursive calls, each on an input of half the size. 
                var a = SubMatrix(x, 0, 0, n, n);
                var b = SubMatrix(x, 0, n, n, n);
                var c = SubMatrix(x, n, 0, n, n);
                var d = SubMatrix(x, n, n, n, n);

                var e = SubMatrix(y, 0, 0, n, n);
                var f = SubMatrix(y, 0, n, n, n);
                var g = SubMatrix(y, n, 0, n, n);
                var h = SubMatrix(y, n, n, n, n);

                // Recursively compute the eight matrix products. 
                var ae = MultiplyRecursive(n, a, e);
                var bg = MultiplyRecursive(n, b, g);
                var af = MultiplyRecursive(n, a, f);
                var bh = MultiplyRecursive(n, b, h);
                var ce = MultiplyRecursive(n, c, e);
                var dg = MultiplyRecursive(n, d, g);
                var cf = MultiplyRecursive(n, c, f);
                var dh = MultiplyRecursive(n, d, h);

                // Since an n x n matrix has n^2 entries, and the number of operations needed 
                // to add two matrices is proportional to the number of entries, a recursive call 
                // on a pair of l x l matrices performs O(l^2) operations, not counting the work 
                // done by its own recursive calls.
                var aebg = AddMatrices(ae, bg); // A·E + B·G
                var afbh = AddMatrices(af, bh); // A·F + B·H
                var cedg = AddMatrices(ce, dg); // C·E + D·G
                var cfdh = AddMatrices(cf, dh); // C·F + D·H

                // Combines submatrices to a single matrix.
                var xy = CombineMatrices(n, aebg, afbh, cedg, cfdh);

                return xy;
            }
        }

        /// <summary>
        /// Multiplies two square matrices using a brute force approach. The MultiplyBruteForce
        /// method translates the mathematical definition into code.
        /// 
        /// Running time: Θ(n^3)
        /// 
        /// [AlgoIlluminated-1] p.72-73 3.3.3 Matrix Multiplication - The Straightforward Algorithm
        /// </summary>
        /// <param name="n">The size of both input matrices n x n.</param>
        /// <param name="x">An n x n matrix</param>
        /// <param name="y">An n x n matrix</param>
        /// <returns>A product matrix Z = X · Y</returns>
        public static double[,] MultiplyBruteForce(int n, double[,] x, double[,] y)
        {
            var z = new double[n, n];

            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    z[i, j] = 0;
                    for (int k = 0; k < n; ++k)
                    {
                        z[i, j] += x[i, k] * y[k, j];
                    }
                }
            }

            return z;
        }

        #region Helpers
        private static T[,] CombineMatrices<T>(int n, T[,] a, T[,] b, T[,] c, T[,] d)
        {
            var o = new T[n * 2, n * 2];

            for (int i = 0, k = 0; i < n; ++i, ++k)
                for (int j = 0, l = 0; j < n; ++j, ++l)
                    o[k, l] = a[i, j];

            for (int i = 0, k = 0; i < n; ++i, ++k)
                for (int j = 0, l = n; j < n; ++j, ++l)
                    o[k, l] = b[i, j];

            for (int i = 0, k = n; i < n; ++i, ++k)
                for (int j = 0, l = 0; j < n; ++j, ++l)
                    o[k, l] = c[i, j];

            for (int i = 0, k = n; i < n; ++i, ++k)
                for (int j = 0, l = n; j < n; ++j, ++l)
                    o[k, l] = d[i, j];

            return o;
        }

        // n - size of the matrices; both matrices must be n x n
        // a,b - input square matrices
        private static double[,] AddMatrices(double[,] a, double[,] b)
        {
            // Make sure both matrices are square.
            Debug.Assert(a.GetLength(0) == a.GetLength(1));
            Debug.Assert(b.GetLength(0) == b.GetLength(1));
            Debug.Assert(a.GetLength(0) == b.GetLength(0));

            int n = a.GetLength(0); // any dimension (# of columns or # of rows) would do

            var o = new double[n, n];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    o[i, j] = a[i, j] + b[i, j];
            return o;
        }

        private static double[,] SubtractMatrices(double[,] a, double[,] b)
        {
            // Make sure both matrices are square.
            Debug.Assert(a.GetLength(0) == a.GetLength(1));
            Debug.Assert(b.GetLength(0) == b.GetLength(1));
            Debug.Assert(a.GetLength(0) == b.GetLength(0));

            int n = a.GetLength(0); // any dimension (# of columns or # of rows) would do

            var o = new double[n, n];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    o[i, j] = a[i, j] - b[i, j];
            return o;
        }

        // m - input matrix
        // x,y - starting element
        // w,h - width and height of the output matrix
        private static T[,] SubMatrix<T>(T[,] m, int x, int y, int w, int h)
        {
            var o = new T[w, h];
            for (int i = x, k = 0; i < x + w; ++i, ++k)
                for (int j = y, l = 0; j < y + h; ++j, ++l)
                    o[k, l] = m[i, j];
            return o;
        }
        #endregion
    }
}

