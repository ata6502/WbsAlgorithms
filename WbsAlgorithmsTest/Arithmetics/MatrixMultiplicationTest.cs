using NUnit.Framework;
using WbsAlgorithms.Arithmetics;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class MatrixMultiplicationTest
    {
        private int NumberOfDecimalPlaces = 5;

        // The input matrices have to be square and the size of each matrix has to to be a power of 2.
        [TestCase(2, "0,-1 ; 1, 0", "1,-1 ; -1,0.5", "1,-0.5 ; 1,-1", TestName = "2x2 Matrices Case# 1")]
        [TestCase(2, "1,-1 ; -1,0.5", "0,-1 ; 1, 0", "-1,-1 ; 0.5,1", TestName = "2x2 Matrices Case# 2")]
        [TestCase(2, "2,6 ; 4,12", "2,6 ; 4,12", "28,84 ; 56,168", TestName = "2x2 Matrices Case# 3")]
        [TestCase(2, "1,0 ; 0,-1", "0.1,0.5 ; 2,0", "0.1,0.5 ; -2,0", TestName = "2x2 Matrices Case# 4")]
        [TestCase(4, "2,1.2,3,2 ; -2,0,-1,3.1 ; 4.1,3,9,4 ; 3,2,7,-3", 
                     "-3,2,1.4,2 ; 3.3,-5,-0.1,0.3 ; 4,2,1,0.5 ; -4,-6,-1.2,3.3",
                     "1.96,-8,3.28,12.46 ; -10.4,-24.6,-7.52,5.73 ; 17.6,-12.8,9.64,26.8 ; 37.6,28,14.6,0.2", TestName = "4x4 Matrices Case #1")]
        [TestCase(4, "1,2,3,4 ; 5,6,7,8 ; 9,10,11,12 ; 13,14,25,16",
                     "4,2,4,3 ; 5,2,3,1 ; 7,5,6,2 ; 9,5,4,1",
                     "71,41,44,15 ; 171,97,112,43 ; 271,153,180,71 ; 441,259,308,119", TestName = "4x4 Matrices Case# 2")]
        [TestCase(8, "2,-1,4,3,8,-5,7,0 ; 3,6,-7,-3,6,3,-6,-1 ; 4,2,1,-7,2,-3,6,9 ; -7,-1,4,5,2,3,8,-8 ; 3,2,1,-5,3,-7,-2,6 ; 6,3,5,-2,-6,-2,6,-1 ; -5,-2,4,-5,6,1,4,2 ; 2,-5,-4,-6,-3,4,3,7",
                     "-5,2,3,1,-6,-3,0,4 ; -4,-2,-3,5,2,4,1,1 ; 8,7,6,5,-3,-2,-4,-1 ; 5,6,3,4,2,-3,-3,-6 ; 1,0,2,3,0,0,4,5 ; -4,5,-2,-5,-3,0,-1,1 ; 4,2,3,-5,-3,-2,0,1 ; 5,6,-2,0,3,2,-4,-5",
                     "97,41,89,43,-26,-41,11,27 ; -145,-76,-70,19,15,48,68,75 ; 28,20,1,-18,-19,21,-6,27 ; 78,29,59,-21,-19,-38,5,-2 ; 13,-24,-4,52,18,28,8,19 ; 9,25,45,0,-64,-24,-29,13 ; 68,17,18,-22,-5,10,12,29 ; -24,18,-40,-111,-22,8,-15,0", TestName = "8x8 Matrices")]
        public void MultiplyTest(int matrixSize, string inputMatrixX, string inputMatrixY, string expectedProductMatrix)
        {
            var X = DataConverter.ConvertStringToMatrix<double>(inputMatrixX);
            var Y = DataConverter.ConvertStringToMatrix<double>(inputMatrixY);
            var Z = expectedProductMatrix.Replace(" ", "");

            var productBruteForce = MatrixMultiplication.MultiplyBruteForce(matrixSize, X, Y);
            Assert.AreEqual(Z, DataConverter.ConvertMatrixToString(productBruteForce, NumberOfDecimalPlaces));

            var productRecursive = MatrixMultiplication.MultiplyRecursive(matrixSize, X, Y);
            Assert.AreEqual(Z, DataConverter.ConvertMatrixToString(productRecursive, NumberOfDecimalPlaces));

            var productStrassen = MatrixMultiplication.MultiplyStrassen(matrixSize, X, Y);
            Assert.AreEqual(Z, DataConverter.ConvertMatrixToString(productStrassen, NumberOfDecimalPlaces));
        }
    }
}
