using System.Runtime.InteropServices.ComTypes;

namespace MathNetExample;

public class Match
{
    public static PlayerResult[] Play(PlayerResult player1, PlayerResult player2, Action<Board>? bordObserver = null)
    {
        
        var board = CreateBoard();

        var turn = 1;
        if (bordObserver ==null)
        {
            turn = -1;
            board[MathHelper.Random.Next(0, 9)] = -1;
        }

        var playerQueue = new Queue<PlayerResult>(new[]{player1, player2});
    
   
        var count = 0;
        while (true)
        {
            count++;
            var player = playerQueue.Dequeue();
            var turnBoard = board.Multiply(turn);
            var playerMove = (player.player * turnBoard.Add(0.00001)).MaximumIndex();
        
            if (board[playerMove] != 0)
            {
                board[playerMove] = 2;
                bordObserver?.Invoke(board);
                return new []{playerQueue.Dequeue(), player with {score =  player.score- 100 * (9-count)}};
            }
            
            turnBoard[playerMove] = 1;
            board[playerMove] = turn;
            bordObserver?.Invoke(board);
            if (MathHelper.HasWinningLine(turnBoard.ToArray())>0)
            {
                var oponent = playerQueue.Dequeue();
                return new []{oponent with{ score = oponent.score -5*(10-count)}, player with {score =  player.score + 100*count}};
            }
            
            player = player with {score =  player.score + count*100};
            
            if (bordObserver == null)
            {
                board = RotateMatrixHelper.RotateBoard(board);
            }
            if(MathHelper.EndBoard(board))
            {
                return new []{playerQueue.Dequeue(), player with {score =  player.score + 1}};
            }
        
            playerQueue.Enqueue(player);
            turn*=-1;
        }
    }
    
    static Board CreateBoard() => Board.Build.DenseOfArray(new double[9]);
}