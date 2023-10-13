using MathNet.Numerics.LinearAlgebra;

namespace MathNetExample;

public static class RotateMatrixHelper
{
    public static Vector<double> RotateBoard(Vector<double> board)
    {
        var matrix = ConvertVectorToMatrix(board);
        var rotatedMatrix = RotateMatrix90Degrees(matrix);
        var rotatedBoard = ConvertMatrixToVector(rotatedMatrix);
        return rotatedBoard;
    }

    static double[,] ConvertVectorToMatrix(Vector<double> vector)
    {
        double[,] matrix = new double[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                matrix[i, j] = vector[3 * i + j];
            }
        }
        return matrix;
    }

    static double[,] RotateMatrix90Degrees(double[,] matrix)
    {
        Random random = new Random();
        int times = random.Next(0, 4);  // generates a random number between 1 and 3

        int n = matrix.GetLength(0);
        double[,] rotatedMatrix = new double[n, n];

        if (times == 0)
        {
            return matrix;
        }
        
        if(times == 1)
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    rotatedMatrix[i, j] = matrix[n - j - 1, i];
                }
            }
        
        if(times == 2)
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    rotatedMatrix[i, j] = matrix[j, i];
                }
            }
        
        if(times == 3)
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    rotatedMatrix[j, i] = matrix[i, n - j - 1];
                }
            }


        return rotatedMatrix;
    }

    static Vector<double> ConvertMatrixToVector(double[,] matrix)
    {
        double[] array = new double[9];
        int k = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                array[k++] = matrix[i, j];
            }
        }
        return Vector<double>.Build.DenseOfArray(array);
    }
}