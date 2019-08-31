using UnityEngine;

public class Move
{
    public readonly Piece piece;
    public readonly Vector2Int targetPosition;

    public Move(Piece piece, Vector2Int targetPosition)
    {
        this.piece = piece;
        this.targetPosition = targetPosition;
    }

    public override string ToString() => $"{piece} to {targetPosition}";

    public override bool Equals(object obj)
    {
        if (obj is Move otherMove)
        {
            return otherMove.piece == piece && otherMove.targetPosition == targetPosition;
        }
        return false;
    }
}
