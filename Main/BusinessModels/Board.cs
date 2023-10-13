using MathNet.Numerics.LinearAlgebra;

namespace MathNetExample;

public static class Board
{
    public static Vector<double> InversionBoard(this Vector<double> board)
    {
        return board.Multiply(-1);
    }
    
    public static void Display(Vector<double> board)
    {
        if (board.Count != 9)
            throw new ArgumentException("Board must contain exactly 9 elements.");

        Console.WriteLine("Current Board:");
        for (int i = 0; i < 9; i += 3)
        {
            Console.WriteLine($" {GetSymbol(board[i])} | {GetSymbol(board[i + 1])} | {GetSymbol(board[i + 2])} ");
            if (i < 6)
                Console.WriteLine("-----------");
        }
    }
    public static string GetSymbol(double value)
    {
        return value switch
        {
            1 => "X",
            -1 => "O",
            2 => "E",
            _ => " "
        };
    }
}