using MathNet.Numerics.LinearAlgebra;
using Turn = (int Player, int Oponnent);
namespace MathNetExample;

public class Match
{
    private readonly bool _trainGame;
    private readonly Action<Vector<double>>? _bordObserver;
    Turn _turn = (1, -1);
    private Vector<double> _board;

    public Match( bool trainGame,  Action<Vector<double>>? bordObserver, int firstStep = 4)
    {
        _trainGame = trainGame;
        _bordObserver = bordObserver;
        _board = CreateBoard();
        
        if (MathHelper.GetRandomBool())
        {
            SwitchTurn();
            _board[firstStep] = 1;
        }
    }
    
    public void Play(PlayerResult player1, PlayerResult player2)
    {
        var opponents = new Dictionary<int, PlayerResult>(){ {1, player1}, {-1, player2} };
        var setNum = 0;
        while (setNum<9)
        {
            if (PlaySet(opponents, _turn, setNum))
            {
                _bordObserver?.Invoke(_board);
                break; 
            }
            
            if (_trainGame)
            {
                _board = RotateMatrixHelper.RotateBoard(_board);
            }
            
            _bordObserver?.Invoke(_board);
            setNum++;
            SwitchTurn();
        }
    }

    private bool PlaySet(Dictionary<int, PlayerResult> opponents, Turn turn, int count)
    {
        var player = opponents[turn.Player];
        var opponent = opponents[turn.Oponnent];
        var setBoard = _board.Multiply(turn.Player);
            
        var playerMove = player.Player.Move(setBoard);
        
        if (_board[playerMove] != 0)
        {
            _board[playerMove] = 2;

            player.Score -= 1000 * (9 - count);
            return true;
        }
            
        setBoard[playerMove] = 1;
        _board[playerMove] = turn.Player;

        if (MathHelper.HasWinningLine(setBoard.ToArray())>0)
        {
            opponent.Score -= 5 * (9 - count);
            player.Score += 1000 * count;
            return true;
        }
            
        player.Score+=count*100;
        
        if(MathHelper.EndBoard(_board))
        {
            player.Score += 1;
            return true;
        }

        return false;
    }
    
    private void SwitchTurn()
    {
        (_turn.Player, _turn.Oponnent) = (_turn.Oponnent, _turn.Player);
    }
    static Vector<double> CreateBoard() => Vector<double>.Build.DenseOfArray(new double[9]);
}