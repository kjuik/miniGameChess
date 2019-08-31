using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board
{
    public readonly Piece[,] pieces;

    public Board()
    {
        pieces = new Piece[8,8];
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

    public void InitializePiece(Piece piece, int x, int z)
    {
        pieces[x, z] = piece;
    }

    public IEnumerable<Piece> GetPieces(Color c)
    {
        for (var i = 0; i < 8; ++i)
        {
            for (var j = 0; j < 8; ++j)
            {
                if (pieces[i, j] != null && pieces[i, j].color == c)
                    yield return pieces[i, j];
            }
        }
    }

    internal Piece GetPiece(Vector2Int? originPos)
    {
        throw new NotImplementedException();
    }

    public Vector2Int GetPosition(Piece piece)
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

    public Piece GetPiece(Vector2Int position) => pieces[position.x, position.y];
    
    public IEnumerable<Move> GetPossibleMoves(Piece piece)
    {
        return RulesSolver.GetPossibleMoves(pieces, piece, GetPosition(piece));
    }

    public Color? GetWinner()
    {
        bool whiteKingFound = false;
        bool blackKingFound = false;

        for (var i = 0; i < 8; ++i)
        {
            for (var j = 0; j < 8; ++j)
            {
                if (pieces[i, j] == null) continue;
                
                if (pieces[i, j].type == Piece.Type.King)
                {
                    if(pieces[i, j].color == Color.Black)
                    {
                        blackKingFound = true;
                    }
                    else
                    {
                        whiteKingFound = true;
                    }
                }
            }
        }

        if (!blackKingFound)
            return Color.White;

        if (!whiteKingFound)
            return Color.Black;

        return null;
    }

    public void ExecuteMove(Move move)
    {
        var sourcePosition = GetPosition(move.piece);
        pieces[sourcePosition.x, sourcePosition.y] = null;

        if (pieces[move.targetPosition.x, move.targetPosition.y] != null)
        {
            pieces[move.targetPosition.x, move.targetPosition.y].gameObject.SetActive(false);
        }

        pieces[move.targetPosition.x, move.targetPosition.y] = move.piece;
    }

    public Board SimulateMove(Move m)
    {
        var board = new Board(this);
        board.ExecuteMove(m);
        return board;
    }
}
