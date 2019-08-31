using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameUi : MonoBehaviour
{
    public static GameUi Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public InputField moveInput;
    public Text lastMove;
    public Button submit;

    public GameObject winPopup;
    public Text winText;

    string currentMoveString => moveInput.text;

    public void OnMovePlayed(Move move)
    {
        lastMove.text =
            $"Last move:\n{move.piece.color} {move.piece.type}\n" +
            $"from {PositionToString(GameManager.Instance.Board.GetPosition(move.piece))}" +
            $" to {PositionToString(move.targetPosition)}";
    }

    public void OnWon(Color color)
    {
        winPopup.SetActive(true);
        winText.text = $"{color} wins!";
    }

    public void OnSubmit()
    {
        var move = ParseMove(moveInput.text.Trim());

        Debug.Log(
            $"move:\n{move.piece.color} {move.piece.type}\n" +
            $"from {PositionToString(GameManager.Instance.Board.GetPosition(move.piece))}" +
            $" to {PositionToString(move.targetPosition)}");

        foreach (var pos in GameManager.Instance.Board.GetPossibleMoves(move.piece))
        {
            Debug.Log($"Possible move:\n{pos.piece.color} {pos.piece.type}\n" +
                        $"from {PositionToString(GameManager.Instance.Board.GetPosition(pos.piece))}" +
                        $" to {PositionToString(pos.targetPosition)}");
        }

        if (move != null &&
            GameManager.Instance.Board.GetPossibleMoves(move.piece).Any(m => m.Equals(move)))
        {
            GameManager.Instance.PlayMove(move);
        }
        else
        {
            Debug.Log("Move impossible");
        }
    }

    public void SetInteractible(bool interactible)
    {
        moveInput.interactable = interactible;
        submit.interactable = interactible;
    }

    private Move ParseMove(string str)
    {
        if (str.Length < 5)
            return null;

        Vector2Int? originPos = ParsePosition(str.Substring(0, 2));
        Vector2Int? targetPos = ParsePosition(str.Substring(3, 2));

        if (originPos.HasValue && targetPos.HasValue)
        {
            return new Move(GameManager.Instance.Board.GetPiece(originPos.Value), targetPos.Value);
        }
        return null;
    }

    private Vector2Int? ParsePosition(string pos)
    {
        if (new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' }.Contains(pos[0]) &&
            new List<char> { '1', '2', '3', '4', '5', '6', '7', '8' }.Contains(pos[1]))
        {
            return new Vector2Int(pos[0] - 'A', pos[1] - '1');
        }

        return null;
    }

    private string PositionToString(Vector2Int pos)
    {
        return "" + 
            new List<char> { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' }[pos.x] + 
            new List<char> { '1', '2', '3', '4', '5', '6', '7', '8' }[pos.y];
    }
}
