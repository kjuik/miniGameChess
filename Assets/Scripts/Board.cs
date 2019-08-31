using System;
using UnityEngine;

public class Board
{
    Piece[,] pieces;

    public Board()
    {
        pieces = new Piece[8,8];
    }

    internal void InitializePiece(Piece piece, int x, int z)
    {
        pieces[x, z] = piece;
    }

    public Board(Board toClone) : this()
    {
        for (var i = 0; i<8; ++i)
        {
            for (var j=0; j<8; ++j)
            {
                pieces[i, j] = toClone.pieces[i, j];
            }
        }
    }

    Vector2Int GetPosition(Piece piece)
    {
        for (var i = 0; i < 8; ++i)
        {
            for (var j = 0; j < 8; ++j)
            {
                if (pieces[i, j] == piece)
                    return new Vector2Int(i, j);
            }
        }

        return new Vector2Int(-1, -1);
    }

    void GetPossibleMoves(Piece piece)
    {
        throw new Exception("lol");
    }

    void ExecuteMove(Move move)
    {
        var sourcePosition = GetPosition(move.piece);

        pieces[sourcePosition.x, sourcePosition.y] = null;
        pieces[move.targetPosition.x, move.targetPosition.y] = move.piece;
    }

    public Board SimulateMove(Move m)
    {
        var board = new Board(this);
        board.ExecuteMove(m);
        return board;
    }
}
