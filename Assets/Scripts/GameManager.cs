using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Board Board { get; private set; }

    private void Awake()
    {
        Instance = this;
        Board = new Board();

        foreach(var piece in Board.GetPieces(Color.Black))
        {

        }
    }

    internal void InitializePiece(Piece piece)
    {
        Board.InitializePiece(piece, (int)piece.transform.position.x, (int)piece.transform.position.z);
    }
}
