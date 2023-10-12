using System.Collections.Concurrent;

namespace MathNetExample;

public static class Game
{
    public static PlayerResult[] PlayAndGetWinner(Player[] generation)
    {
        var players = new ConcurrentQueue<PlayerResult>(generation.Select(_ => new PlayerResult(_, 0)));
        var result = new ConcurrentBag<PlayerResult>();

        // Ensure thread safety for counters and shared resources
        int matchCounter = 0;

        Parallel.For(0L, 4L, i =>
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
                var matchResult = Match.Play(p1, p2);
                foreach (var res in matchResult)
                {
                    result.Add(res);
                }
            }

            // Re-shuffle players: Ensure this operation is thread-safe and results are consistent
            players = new ConcurrentQueue<PlayerResult>(MathHelper.Shuffle(result.ToArray()));
        });

        // Find the best player, no need to be in a parallel block
        return players.OrderByDescending(_ => _.score).Take(8).ToArray();
    }
    
}