using System;
using System.Collections.Generic;
using UnityEngine;

public class RulesSolver
{
    internal static IEnumerator<Move> GetPossibleMoves(Piece[,] board, Piece piece, Vector2Int position)
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

    private static IEnumerator<Move> GetPawnMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }


    private static IEnumerator<Move> GetRookMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerator<Move> GetKnightMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerator<Move> GetBishopMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerator<Move> GetQueenMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }

    private static IEnumerator<Move> GetKingMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        throw new NotImplementedException();
    }
}
