using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Neomento;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

struct TreeNode
{
    public Board board;
    public List<TreeNode> children;
    public float value;
}

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
        return 0f;
    }
}