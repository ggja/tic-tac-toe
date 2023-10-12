namespace MathNetExample;
using Board = MathNet.Numerics.LinearAlgebra.Vector<double>;

public static class MathHelper
{
    public static Random Random = new Random();
    public static double[,] GenerateRandomMatrix(int rows, int cols, int precision=1)
    {
        var data = new double[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                data[i, j] = (Random.NextDouble() * 2.0 - 1.0)/precision;
            }
        }

        return data;
    }
    
    
    static int[][] winLines = new int[][]
    {
        new int[] {0, 1, 2},
        new int[] {3, 4, 5},
        new int[] {6, 7, 8},
        new int[] {0, 3, 6},
        new int[] {1, 4, 7},
        new int[] {2, 5, 8},
        new int[] {0, 4, 8},
        new int[] {2, 4, 6}
    };

    public static int HasWinningLine(double[] board)
    {
        double epsilon = 0.000001;  // Tolerance level
        var lines = 0;
        foreach (var line in winLines)
        {
            if (Math.Abs(board[line[0]] - 1.0) < epsilon &&
                Math.Abs(board[line[1]] - 1.0) < epsilon &&
                Math.Abs(board[line[2]] - 1.0) < epsilon)
            {
                lines++;
            }
        }
        return lines;
    }
    
    public static bool EndBoard(Board board)
    {
        double epsilon = 0.000001;
        return board.Exists(_ => _ < epsilon) == false;
    }
    
    public static T[] Shuffle<T>(T[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            // Generate a random index from 0 to i
            int j = Random.Next(i + 1);

            // Swap array[i] and array[j]
            (array[i], array[j]) = (array[j], array[i]);
        }

        return array;
    }
    

}
