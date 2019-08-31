using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Board Board { get; private set; }

    public Color currentTurn = Color.White;

    public int searchDepth = 0;

    private void Awake()
    {
        Instance = this;
        Board = new Board();
    }

    internal void InitializePiece(Piece piece)
    {
        Board.InitializePiece(piece, Mathf.RoundToInt(piece.transform.position.x), Mathf.RoundToInt(piece.transform.position.z));
    }

    public void PlayMove(Move move)
    {
        Board.ExecuteMove(move);
        GameUi.Instance.OnMovePlayed(move);

        move.piece.transform.position = new Vector3(
            move.targetPosition.x,
            move.piece.transform.position.y,
            move.targetPosition.y);

        if (currentTurn == Color.White)
        {
            currentTurn = Color.Black;
        }
        else
        {
            currentTurn = Color.White;
        }

        var winner = Board.GetWinner();
        if (winner.HasValue)
        {
            GameUi.Instance.OnWon(winner.Value);
        }
        else if (currentTurn == Color.Black)
        {
            PlayMove(ChessAI.GetNextMove(Board, Color.Black));
        }
    }
}
