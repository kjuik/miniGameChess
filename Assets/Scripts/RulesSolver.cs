using System;
using System.Collections.Generic;
using UnityEngine;

public class RulesSolver
{
    internal static IEnumerable<Move> GetPossibleMoves(Piece[,] board, Piece piece, Vector2Int position)
    {
        switch (piece.type)
        {
            case Piece.Type.Pawn:
                return GetPawnMoves(piece, board, position);
            case Piece.Type.Rook:
                return GetRookMoves(piece, board, position);
            case Piece.Type.Knight:
                return GetKnightMoves(piece, board, position);
            case Piece.Type.Bishop:
                return GetBishopMoves(piece, board, position);
            case Piece.Type.Queen:
                return GetQueenMoves(piece, board, position);
            case Piece.Type.King:
                return GetKingMoves(piece, board, position);
            default:
                return null;
        }
    }

    private static IEnumerable<Move> GetPawnMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }


    private static IEnumerable<Move> GetRookMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<Move> GetKnightMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<Move> GetBishopMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<Move> GetQueenMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<Move> GetKingMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }
}
