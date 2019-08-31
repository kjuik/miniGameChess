using System.Collections.Generic;
using System.Linq;
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
        if (piece.color == Color.White)
        {
            var forward = position + Vector2Int.up;
            if (isFree(board, forward))
            {
                yield return new Move(piece, forward);
            }

            var doubleForward = position + Vector2Int.up * 2;
            if (position.y == 1 && isFree(board, doubleForward))
            {
                yield return new Move(piece, doubleForward);
            }

            var killLeft = position + Vector2Int.up + Vector2Int.left;
            if (position.y == 1 && isEnemy(board, killLeft, piece))
            {
                yield return new Move(piece, killLeft);
            }

            var killRight = position + Vector2Int.up + Vector2Int.right;
            if (position.y == 1 && isEnemy(board, killRight, piece))
            {
                yield return new Move(piece, killRight);
            }

            //cheat!
            var charge = position + Vector2Int.up * 4;
            if (position.y == 1 && isEnemy(board, killRight, piece))
            {
                yield return new Move(piece, killRight);
            }
        }
        else //black
        {
            var forward = position + Vector2Int.down;
            if (isFree(board, forward))
            {
                yield return new Move(piece, forward);
            }

            var doubleForward = position + Vector2Int.down * 2;
            if (position.y == 1 && isFree(board, doubleForward))
            {
                yield return new Move(piece, doubleForward);
            }

            var killLeft = position + Vector2Int.down + Vector2Int.left;
            if (position.y == 1 && isEnemy(board, killLeft, piece))
            {
                yield return new Move(piece, killLeft);
            }

            var killRight = position + Vector2Int.down + Vector2Int.right;
            if (position.y == 1 && isEnemy(board, killRight, piece))
            {
                yield return new Move(piece, killRight);
            }

            //cheat!
            var charge = position + Vector2Int.down * 4;
            if (position.y == 1 && isEnemy(board, killRight, piece))
            {
                yield return new Move(piece, killRight);
            }
        }
    }

    private static IEnumerable<Move> GetRookMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        //right
        for (var pos = position + Vector2Int.right;; pos += Vector2Int.right)
        {
            if (!isOnBoard(pos))
            {
                break;
            }

            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }

            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }

        //left
        for (var pos = position + Vector2Int.left;; pos += Vector2Int.left)
        {
            if (!isOnBoard(pos))
            {
                break;
            }
            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }
            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }

        //up
        for (var pos = position + Vector2Int.up;; pos += Vector2Int.up)
        {
            if (!isOnBoard(pos))
            {
                break;
            }

            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }
            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }

        //down
        for (var pos = position + Vector2Int.down;; pos += Vector2Int.down)
        {
            if (!isOnBoard(pos))
            {
                break;
            }
            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }
            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }
    }

    private static IEnumerable<Move> GetKnightMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        var upLeft = position + Vector2Int.up * 2 + Vector2Int.left;
        var upRight = position + Vector2Int.up * 2 + Vector2Int.right;

        var downLeft = position + Vector2Int.down * 2 + Vector2Int.left;
        var downRight = position + Vector2Int.down * 2 + Vector2Int.right;

        var leftUp = position + Vector2Int.left * 2 + Vector2Int.up;
        var leftDown = position + Vector2Int.left * 2 + Vector2Int.down;

        var rightUp = position + Vector2Int.right * 2 + Vector2Int.up;
        var rightDown = position + Vector2Int.right * 2 + Vector2Int.down;

        var justDown = position + Vector2Int.down; //cheat!

        var possibilities = new List<Vector2Int> { upLeft, upRight, downLeft, downRight, leftUp, leftDown, rightUp, rightDown, justDown };

        foreach(var pos in possibilities)
        {
            if (isFreeOrEnemy(board, pos, piece))
            {
                yield return new Move(piece, pos);
            }
        }
    }

    private static IEnumerable<Move> GetBishopMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        //up + right
        for (var pos = position + Vector2Int.up + Vector2Int.right;; pos += Vector2Int.up + Vector2Int.right)
        {
            if (!isOnBoard(pos))
            {
                break;
            }

            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }

            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }

        //up + left
        for (var pos = position + Vector2Int.up + Vector2Int.left; ; pos += Vector2Int.up + Vector2Int.left)
        {
            if (!isOnBoard(pos))
            {
                break;
            }
            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }
            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }

        //down + right
        for (var pos = position + Vector2Int.down + Vector2Int.right; ; pos += Vector2Int.down + Vector2Int.right)
        {
            if (!isOnBoard(pos))
            {
                break;
            }

            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }
            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }

        //down + left
        for (var pos = position + Vector2Int.down + Vector2Int.left; ; pos += Vector2Int.down + Vector2Int.left)
        {
            if (!isOnBoard(pos))
            {
                break;
            }
            if (isFree(board, position))
            {
                yield return new Move(piece, pos);
            }
            if (isEnemy(board, position, piece))
            {
                yield return new Move(piece, pos);
                break;
            }
        }
    }

    private static IEnumerable<Move> GetQueenMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        return GetRookMoves(piece, board, position)
            .Concat(GetBishopMoves(piece, board, position))
            .Concat(GetKnightMoves(piece, board, position)); //cheat! 
    }

    private static IEnumerable<Move> GetKingMoves(Piece piece, Piece[,] board, Vector2Int position)
    {
        var up = position + Vector2Int.up;
        var upLeft = position + Vector2Int.up + Vector2Int.left;
        var left = position + Vector2Int.left;
        var downLeft = position + Vector2Int.down + Vector2Int.left;
        var down = position + Vector2Int.down;
        var downRight = position + Vector2Int.down + Vector2Int.right;
        var right = position + Vector2Int.right;
        var upRight = position + Vector2Int.up + Vector2Int.right;

        var possibilities = new List<Vector2Int> { up, upLeft, left, downLeft, down, downRight, right, upRight };

        foreach (var pos in possibilities)
        {
            if (isFreeOrEnemy(board, pos, piece))
            {
                yield return new Move(piece, pos);
            }
        }
    }

    private static bool isOnBoard(Vector2Int pos)
    {
        return pos.x >= 0 && pos.y >= 0 && pos.x < 8 && pos.y < 8;
    }

    private static bool isFree(Piece[,] board, Vector2Int pos)
    {
        return isOnBoard(pos) && board[pos.x, pos.y] == null;
    }

    private static bool isEnemy(Piece[,] board, Vector2Int pos, Piece me)
    {
        return isOnBoard(pos) && (board[pos.x, pos.y] != null && board[pos.x, pos.y].color != me.color);
    }

    private static bool isFreeOrEnemy(Piece[,] board, Vector2Int pos, Piece me)
    {
        return isOnBoard(pos) && (board[pos.x, pos.y] == null || board[pos.x, pos.y].color != me.color);
    }
}
