using NUnit.Framework;
using WbsAlgorithms.Arithmetics;
using WbsAlgorithmsTest.Utilities;

namespace WbsAlgorithmsTest.Arithmetics
{
    [TestFixture]
    public class MatrixMultiplicationTest
    {
        // The input matrices have to be square.
        [TestCase(2, "0,-1 ; 1, 0", "1,-1 ; -1,0.5", "1,-0.5 ; 1,-1", TestName = "2x2 Matrices Case# 1")]
        [TestCase(2, "1,-1 ; -1,0.5", "0,-1 ; 1, 0", "-1,-1 ; 0.5,1", TestName = "2x2 Matrices Case# 2")]
        public void MultiplyTest(int matrixSize, string inputMatrixX, string inputMatrixY, string expectedProductMatrix)
        {
            var X = DataConverter.ConvertStringToMatrix<double>(inputMatrixX);
            var Y = DataConverter.ConvertStringToMatrix<double>(inputMatrixY);
            var Z = expectedProductMatrix.Replace(" ", "");

            var productStrassen = MatrixMultiplication.MultiplyStrassen(matrixSize, X, Y);
            Assert.AreEqual(Z, DataConverter.ConvertMatrixToString(productStrassen));

            var productBruteForce = MatrixMultiplication.MultiplyBruteForce(matrixSize, X, Y);
            Assert.AreEqual(Z, DataConverter.ConvertMatrixToString(productBruteForce));

            var productRecursive = MatrixMultiplication.MultiplyRecursive(matrixSize, X, Y);
            Assert.AreEqual(Z, DataConverter.ConvertMatrixToString(productRecursive));
        }
    }
}
