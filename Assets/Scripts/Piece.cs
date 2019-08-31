using UnityEngine;

public class Piece : MonoBehaviour
{
    public enum Type
    {
        Pawn, 
        Rook,
        Knight,
        Bishop, 
        Queen,
        King
    }

    public Color color;
    public Type type;

    protected void Start()
    {
        GameManager.Instance.InitializePiece(this);
    }
}
