using System.Collections.Generic;
using System.Linq;
using Neomento;
using UnityEngine;


struct ScoredMove
{
    public Move move;
    public float score;
}

public class ChessAI : MonoBehaviour
{
    Board Simulate(Board board, Move move)
    {
        return new Board();
    }

    public Move GetNextMove(Board board, Color color)
    {
        var pieces = new List<Piece>(board.GetPieces(color));

        var admissible = new List<Move>().AsEnumerable();
        
        foreach (var piece in pieces)
        {
            admissible = admissible.Concat(board.GetPossibleMoves(piece));
        }
        
        var scores = admissible.Select(move => ScoreFunction(0, board, color, move)).ToList();
        float maxScore = scores.Max();
        var zipped = admissible.Zip(scores, (move, f) => new ScoredMove(){move = move, score = f});
        var bestMoves = from scoredMove in zipped where scoredMove.score == maxScore select scoredMove.move;

        return bestMoves.RandomElement();
    }

    float ScoreFunction(int depth, Board board, Color color, Move move)
    {
        var piece = board.GetPiece(move.targetPosition);
        if (piece == null)
        {
            return 0f;
        }
        else
        {
            return 1f;
        }

        // var newBoard = new Board(board);

        // newBoard.ExecuteMove(move);
    }
}