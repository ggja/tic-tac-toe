
namespace MathNetExample.Population;

public static class PopulationFactory
{
    public static Player[] CreateGeneration(Player doubles, int size, int precision)
    {
        var generation = new Player[size];
        for (int i = 0; i < size; i++)
        {
            generation[i] = doubles.CreateChild(precision);
        }

        return generation;
    }
}
    
