using Xunit;
using Matrix;
using System;

namespace TestMatrix
{
    public class UnitTest
    {
        [Fact]
        public void AdditionTest()
        {
            // 1
            double[,] arrayFrst = {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0}
            };

            double[,] arrayScnd = {
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1}
            };

            double[,] arrayExpectedResult = {
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1}
            };

            MyMatrix matrixFrst = new MyMatrix(arrayFrst);
            MyMatrix matrixScnd = new MyMatrix(arrayScnd);
            MyMatrix matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            MyMatrix matrixActualResult = matrixFrst + matrixScnd;

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);

            // 2
            arrayFrst = new double[,]{
                { 34, 231, 42},
                { 2, -6, 23},
                { 123, 0, 0},
                { -10, 23, 0},
                { 61, 70, 1}
            };

            arrayScnd = new double[,]{
                { 10, 1, 20},
                { 1, 23, 1},
                { 78, 11, 1},
                { 1, 222, 1},
                { -5, 1, 24}
            };

            arrayExpectedResult = new double[,]{
                { 44, 232, 62},
                { 3, 17, 24},
                { 201, 11, 1},
                { -9, 245, 1},
                { 56, 71, 25}
            };

            matrixFrst = new MyMatrix(arrayFrst);
            matrixScnd = new MyMatrix(arrayScnd);
            matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            matrixActualResult = matrixFrst + matrixScnd;

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);

            // 3
            arrayFrst = new double[,]{
                { 34, 231, 42},
                { 2, -6, 23},
                { 123, 0, 0},
                { -10, 23, 0},
                { 61, 70, 1}
            };

            arrayScnd = new double[,]{
                { 10, 1, 20},
                { 1, 23, 1},
                { -5, 1, 24}
            };

            matrixFrst = new MyMatrix(arrayFrst);
            matrixScnd = new MyMatrix(arrayScnd);

            Assert.Throws<Exception>(() => matrixFrst + matrixScnd);
        }

        [Fact]
        public void DifferenceTest()
        {
            // 1
            double[,] arrayFrst = {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0}
            };

            double[,] arrayScnd = {
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1}
            };

            double[,] arrayExpectedResult = {
                { -1, -1, -1},
                { -1, -1, -1},
                { -1, -1, -1},
                { -1, -1, -1},
                { -1, -1, -1}
            };

            MyMatrix matrixFrst = new MyMatrix(arrayFrst);
            MyMatrix matrixScnd = new MyMatrix(arrayScnd);
            MyMatrix matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            MyMatrix matrixActualResult = matrixFrst - matrixScnd;

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);

            // 2
            arrayFrst = new double[,]{
                { 34, 231, 42},
                { 2, -6, 23},
                { 123, 0, 0},
                { -10, 23, 0},
                { 61, 70, 1}
            };

            arrayScnd = new double[,]{
                { 10, 1, 20},
                { 1, 23, 1},
                { 78, 11, 1},
                { 1, 222, 1},
                { -5, 1, 24}
            };

            arrayExpectedResult = new double[,]{
                { 24, 230, 22},
                { 1, -29, 22},
                { 45, -11, -1},
                { -11, -199, -1},
                { 66, 69, -23}
            };

            matrixFrst = new MyMatrix(arrayFrst);
            matrixScnd = new MyMatrix(arrayScnd);
            matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            matrixActualResult = matrixFrst - matrixScnd;

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);

            // 3
            arrayFrst = new double[,]{
                { 34, 231, 42},
                { 2, -6, 23},
                { 123, 0, 0},
                { -10, 23, 0},
                { 61, 70, 1}
            };

            arrayScnd = new double[,]{
                { 10, 1, 20},
                { 1, 23, 1},
                { -5, 1, 24}
            };

            matrixFrst = new MyMatrix(arrayFrst);
            matrixScnd = new MyMatrix(arrayScnd);

            Assert.Throws<Exception>(() => matrixFrst - matrixScnd);
        }

        [Fact]
        public void MultiplicationOnScalarTest()
        {
            // 1
            double[,] arrayFrst = {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0}
            };

            double scalar = 2.5;

            double[,] arrayExpectedResult = {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0}
            };

            MyMatrix matrixFrst = new MyMatrix(arrayFrst);
            MyMatrix matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            MyMatrix matrixActualResult = matrixFrst * scalar;

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);

            // 2
            arrayFrst = new double[,]{
                { 100, 20, 25 },
                { -100, -20, -25},
                { 0, 0, 0},
                { -1, 1, 10},
                { -125, 125, 1}
            };

            scalar = 3;

            arrayExpectedResult = new double[,]{
                { 300, 60, 75},
                { -300, -60, -75},
                { 0, 0, 0},
                { -3, 3, 30},
                { -375, 375, 3}
            };

            matrixFrst = new MyMatrix(arrayFrst);
            matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            matrixActualResult = matrixFrst * scalar;

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);

            // 3
            arrayFrst = new double[,]{
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1},
                { 1, 1, 1}
            };

            scalar = 2.7;

            arrayExpectedResult = new double[,]{
                { 2.7, 2.7, 2.7},
                { 2.7, 2.7, 2.7},
                { 2.7, 2.7, 2.7},
                { 2.7, 2.7, 2.7},
                { 2.7, 2.7, 2.7}
            };

            matrixFrst = new MyMatrix(arrayFrst);
            matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            matrixActualResult = matrixFrst * scalar;

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);
        }

        [Fact]
        public void TranspositionTest()
        {
            // 1
            double[,] arrayFrst = {
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0},
                { 0, 0, 0}
            };

            double[,] arrayExpectedResult = {
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0},
                { 0, 0, 0, 0, 0}
            };

            MyMatrix matrixFrst = new MyMatrix(arrayFrst);
            MyMatrix matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            MyMatrix matrixActualResult = MyMatrix.Transpose(matrixFrst);

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);

            // 2
            arrayFrst = new double[,]{
                { 1, 2, 3 },
                { 4, 5, 6},
                { 7, 8, 9},
                { 10, 11, 12},
                { 13, 14, 15}
            };

            arrayExpectedResult = new double[,]{
                { 1, 4, 7, 10, 13},
                { 2, 5, 8, 11, 14},
                { 3, 6, 9, 12, 15},
            };

            matrixFrst = new MyMatrix(arrayFrst);
            matrixExpectedResult = new MyMatrix(arrayExpectedResult);
            matrixActualResult = MyMatrix.Transpose(matrixFrst);

            Assert.Equal(matrixExpectedResult.data, matrixActualResult.data);
        }
    }
}