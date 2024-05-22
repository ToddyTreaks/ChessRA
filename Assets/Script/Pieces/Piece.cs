using System;
using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEngine;

#region Enums PiceType and Team

public enum PieceType
{
    PAWN,
    ROCK,
    KNIGHT,
    BISHOP,
    QUEEN,
    KING
}

public enum Team
{
    WHITE,
    BLACK
}

#endregion

#region Class Position

public class Position
{
    public int xIndex;
    public int yIndex;

    public Position(int xIndex, int yIndex)
    {
        this.xIndex = xIndex;
        this.yIndex = yIndex;
    }
}

#endregion


public abstract class Piece : MonoBehaviour
{
    #region Attributs

    public Position actualPosition;
    public PieceType type;
    public Team team;
    public List<Vector2> direction;
    public List<Position> allowedPositionsForNextMove;
    [SerializeField] private GameObject piecePrefab;

    #endregion

    #region SelectionPhase

    public List<Position> GetMoveSelectedPiece()
    {
        allowedPositionsForNextMove = new List<Position>();
        List<Position> possiblePositions = PieceAllowedMove();
        foreach (Position targetPosition in possiblePositions)
        {
            allowedPositionsForNextMove.Add(targetPosition);
            // TODO : Enlever le commentaire suivant
            //if (Board.NotPuttingKingInCheck(this, targetPosition))
                //allowedPositionsForNextMove.Add(targetPosition);
        }

        return allowedPositionsForNextMove;
    }

    protected virtual List<Position> PieceAllowedMove()
    {
        return Board.CheckMove(this);
    }

    #endregion

    #region MovementPhase

    public void MovePiece(Position targetPosition)
    {
        if (allowedPositionsForNextMove.Contains(targetPosition))
            PieceMovement(targetPosition);
        else throw new Exception("This move is not allowed");
    }

    protected virtual void PieceMovement(Position targetPosition)
    {
        if (Board.BoardArray[targetPosition.xIndex, targetPosition.yIndex] != null)
        {
            Board.RemovePiece(targetPosition.xIndex, targetPosition.yIndex);
        }

        Board.AddPiece(this, targetPosition.xIndex, targetPosition.yIndex);
        Board.RemovePiece(this.actualPosition.xIndex, this.actualPosition.yIndex);
        this.actualPosition = targetPosition;
    }

    #endregion
}
