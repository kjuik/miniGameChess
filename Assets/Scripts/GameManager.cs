using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Board Board { get; private set; }

    public Color currentTurn = Color.White;

    private void Awake()
    {
        Instance = this;
        Board = new Board();
    }

    internal void InitializePiece(Piece piece)
    {
        Board.InitializePiece(piece, (int)piece.transform.position.x, (int)piece.transform.position.z);
    }

    public void PlayMove(Move move)
    {
        Board.ExecuteMove(move);
        GameUi.Instance.OnMovePlayed(move);

        if (currentTurn == Color.White)
        {
            currentTurn = Color.Black;
            PlayMove(ChessAI.GetNextMove(Board, Color.Black));
        }
        else
        {
            currentTurn = Color.White;
        }
    }
}
