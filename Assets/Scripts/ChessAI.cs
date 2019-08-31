using System.Collections.Generic;
using System.Linq;
using Neomento;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;


struct ScoredMove
{
    public Move move;
    public float score;
}

public static class ChessAI
{
    static Board Simulate(Board board, Move move)
    {
        return new Board();
    }

    static public Move GetNextMove(Board board, Color color)
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

    static float  ScoreFunction(int depth, Board board, Color color, Move move)
    {
        float score = board.GetPiece(move.targetPosition) == null ? 0f : 1f; // Check hit

        if (depth == GameManager.Instance.searchDepth)
        {
            return score;
        }

        var newBoard = new Board(board);
        newBoard.ExecuteMove(move);
        var nextColor = color.NextColor();
        var pieces = new List<Piece>(board.GetPieces(nextColor));

        var admissible = new List<Move>().AsEnumerable();
        
        foreach (var piece in pieces)
        {
            admissible = admissible.Concat(newBoard.GetPossibleMoves(piece));
        }
        
        float sumOfPoints = admissible.Average(m => ScoreFunction(depth + 1, newBoard, nextColor, m))
            * (depth % 2 == 0 ? 1 : -1);
        
        return score + sumOfPoints;
    }
}