using System.Collections.Concurrent;
using System.Runtime;
using MathNetExample.Population;

namespace MathNetExample;

public static class Game
{
    public static PlayerResult[] PlayAndGetWinner(Player[] generation, int winners)
    {
        var players = new ConcurrentQueue<PlayerResult>(generation.Select(_ => new PlayerResult(_, 0)));
        var result = new ConcurrentBag<PlayerResult>();

        // Ensure thread safety for counters and shared resources
        int matchCounter = 0;

        Parallel.For(0, 16, i =>
        {
            // Clone players for thread safety
            var localPlayers = new ConcurrentQueue<PlayerResult>(players);

            // Thread-safe counter increment
            Interlocked.Increment(ref matchCounter);

            PlayerResult p1, p2;
            while (localPlayers.Count > 1)
            {
                // Safely dequeue players
                localPlayers.TryDequeue(out p1!);
                localPlayers.TryDequeue(out p2!);

                // Play match and store result thread-safely
                var match = new Match(true, null, i%9);
                match.Play(p1, p2);
                
                var match2 = new Match(true, null,i%9);
                match2.Play(p2, p1);
                
                var match3 = new Match(false, null);
                match3.Play(p1, p2);
                
                var match4 = new Match(false, null);
                match4.Play(p2, p1);
    

                result.Add(p1);
                result.Add(p2);
            }

            // Re-shuffle players: Ensure this operation is thread-safe and results are consistent
            players = new ConcurrentQueue<PlayerResult>(MathHelper.Shuffle(result.ToArray()));
        });

        // Find the best player, no need to be in a parallel block
        return players.OrderByDescending(_ => _.Score).Take(winners).ToArray();
    }
    
}