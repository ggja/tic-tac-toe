global using Board = MathNet.Numerics.LinearAlgebra.Vector<double>;
global using Player = MathNet.Numerics.LinearAlgebra.Matrix<double>;

using MathNetExample;
const int generationSize = 128;

Board InversBoard = Board.Build.DenseOfArray(new double[]{-1,-1,-1,-1,-1,-1,-1,-1,-1});

var CreatePlayer = () => Player.Build.DenseOfArray(MathHelper.GenerateRandomMatrix(9, 9));

var CreateGenerationOffset = () => Player.Build.DenseOfArray(MathHelper.GenerateRandomMatrix(9, 9, 100));

var CreateChild = (Player player) =>player + CreateGenerationOffset();

var CreateGeneration = (Player doubles, int size) =>
{
    var generation = new Player[size];
    for (int i = 0; i < size; i++)
    {
        generation[i] = CreateChild(doubles);
    }

    return generation;
};

var CreateFirstGeneration = () =>Enumerable.Range(0,128).Select(_=>CreatePlayer()).ToArray();




var generation = CreateFirstGeneration();
PlayerResult[] result = null!;
for (int i = 0; i<1000000; i++)
{
    result = Game.PlayAndGetWinner(generation);

    if(i % 50 == 0)
    {
        Console.WriteLine($" Winner  score result {result[0].score}");
        Console.WriteLine($" 2nd  score result {result[1].score}");
    
        Match.Play(result[0], result[1] ,BoardExtension.Display);
        Console.WriteLine($"generation {i}");
    }
 
    generation = result.SelectMany(_=>CreateGeneration(_.player, 16)).ToArray();
}

ReadWrite.SaveMatrixToFile(result[0].player);















