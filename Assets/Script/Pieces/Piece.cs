using System;
using System.Collections.Generic;
using Script;
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

#region MotherClass Piece

public abstract class Piece
{
    #region Attributs

    public Position actualPosition;
    public PieceType type;
    public Team team;
    public List<Vector2> direction;
    public List<Position> allowedPositionsForNextMove;
    [SerializeField] private GameObject piecePrefab;

    #endregion

    #region Constructor

    public Piece(Position position, PieceType type, Team team)
    {
        actualPosition = position;
        this.type = type;
        this.team = team;
    }

    #endregion

    #region SelectionPhase

    public List<Position> GetMoveSelectedPiece()
    {
        allowedPositionsForNextMove = new List<Position>();
        List<Position> possiblePositions = PieceAllowedMove();
        foreach (Position targetPosition in possiblePositions)
        {
            allowedPositionsForNextMove.Add(targetPosition);
            //if (Board.NotPuttingKingInCheck(this, targetPosition))
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

#endregion

#region Rock

public class Rock : Piece
{
    #region Attributs

    public readonly bool FirstMove = true;

    #endregion

    #region Constructor

    public Rock(Position position, Team team) : base(position, PieceType.ROCK, team)
    {
        direction = new List<Vector2> { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
    }

    #endregion
}

#endregion

#region Knight

public class Knight : Piece
{
    #region Constructor

    public Knight(Position position, Team team) : base(position, PieceType.KNIGHT, team)
    {
        direction = new List<Vector2>
        {
            new Vector2(2, 1), new Vector2(2, -1), new Vector2(-2, 1), new Vector2(-2, -1), new Vector2(1, 2),
            new Vector2(1, -2), new Vector2(-1, 2), new Vector2(-1, -2)
        };
    }

    #endregion

    #region MoveAllowed

    protected override List<Position> PieceAllowedMove()
    {
        return Board.CheckMove(this, 1);
    }

    #endregion
}

#endregion

#region Bishop

public class Bishop : Piece
{
    #region Constructor

    public Bishop(Position position, Team team) : base(position, PieceType.BISHOP, team)
    {
        direction = new List<Vector2>
            { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
    }

    #endregion
}

#endregion

#region Queen

public class Queen : Piece
{
    #region Constructor

    public Queen(Position position, Team team) : base(position, PieceType.QUEEN, team)
    {
        direction = new List<Vector2>
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
            new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };
    }

    #endregion
}

#endregion