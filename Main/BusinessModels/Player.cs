using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace MathNetExample.Population;

public class Player
{
    private Matrix<double> _matrix1;
    private Matrix<double> _matrix2;

    private Player()
    {
        _matrix1 = Matrix<double>.Build.DenseOfArray(
            MathHelper.GenerateRandomMatrix(9, 18));
        _matrix2 = Matrix<double>.Build.DenseOfArray(
            MathHelper.GenerateRandomMatrix(18, 9));
    }

    private Player(Matrix<double> matrix1, Matrix<double> matrix2)
    {
        _matrix1 = matrix1;
        _matrix2 = matrix2;
    }

    public static Player CreatePlayer() => new();

    public Player CreateChild(int precision)
        => new(
            _matrix1 + Matrix<double>.Build.DenseOfArray(MathHelper.GenerateRandomMatrix(9, 18, precision)),
            _matrix2 + Matrix<double>.Build.DenseOfArray(MathHelper.GenerateRandomMatrix(18, 9, precision)));
    
    public int Move(Vector<double> board)
    {
        var result = ((board.Add(0.001) *_matrix1 ).Map(Math.Tanh)*_matrix2).MaximumIndex();
        return result;
    }
}