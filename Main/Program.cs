global using Board = MathNet.Numerics.LinearAlgebra.Vector<double>;
using MathNetExample;
using MathNetExample.Population;

const int offspring = 64;
const int winners = 8;
const int generationSize = offspring * winners;
var generation = PopulationFactory.CreateGeneration(Player.CreatePlayer(), generationSize, 100);

PlayerResult[] result = null!;
for (int i = 1; i<1000000; i++)
{
    var precision = ((int)Math.Log10(i)+1);
    result = Game.PlayAndGetWinner(generation, winners);
    if (result[0].Score > 0)
    {
        precision *= 10;
    }

    if(i % 50 == 0)
    {
        var match = new Match(true, MathNetExample.Board.Display, MathHelper.Random.Next(0,9));
        match.Play(result[0], result[1]);
        Console.WriteLine($"Generation {i} | Score {result[0].Score}| Precision:{precision}");
    }
    
    generation = result.SelectMany(_=>
    {
        return PopulationFactory.CreateGeneration(_.Player, offspring, precision);
    }).ToArray();
}

// ReadWrite.SaveMatrixToFile(result[0].Score);

















