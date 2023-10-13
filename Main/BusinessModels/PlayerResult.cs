using MathNetExample.Population;

namespace MathNetExample;

public class PlayerResult(Player player, int score)
{
    public Player Player { get; } = player;
    public int Score { get; set; } = score;
}