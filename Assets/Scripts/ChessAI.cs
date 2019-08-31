using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Neomento;
using UnityEngine;

struct TreeNode
{
    public Board board;
    public List<TreeNode> children;
    public float value;
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

        return admissible.RandomElement();
    }

    void RecFunction(int depth, Board board, Color color)
    {
        
    }
}