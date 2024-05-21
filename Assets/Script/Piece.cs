using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

public abstract class Piece
{
    public Position actualPosition;
    public PieceType type;
    public Team team;

    public Piece(Position position, PieceType type, Team team)
    {
        actualPosition = position;
        this.type = type;
        this.team = team;
    }

    public void Move(Position targetPosition)
    {
        IsPositionInBoard(targetPosition);

        CanGoTo(targetPosition);
    }

    protected virtual bool CanGoTo(Position targetPosition)
    {
        return false;
    }

    private void IsPositionInBoard(Position targetPosition)
    {
        if (targetPosition.xIndex < 0 || targetPosition.xIndex > 7)
        {
            throw new Exception("Invalid x index");
        }

        if (targetPosition.yIndex < 0 || targetPosition.yIndex > 7)
        {
            throw new Exception("Invalid y index");
        }
    }
}


public class Pawn : Piece
{
    public Pawn(Position position, Team team) : base(position, PieceType.PAWN, team)
    {
    }
}

public class Rock : Piece
{
    public Rock(Position position, Team team) : base(position, PieceType.ROCK, team)
    {
    }
}

public class Knight : Piece
{
    public Knight(Position position, Team team) : base(position, PieceType.KNIGHT, team)
    {
    }
}

public class Bishop : Piece
{
    public Bishop(Position position, Team team) : base(position, PieceType.BISHOP, team)
    {
    }
}

public class Queen : Piece
{
    public Queen(Position position, Team team) : base(position, PieceType.QUEEN, team)
    {
    }
}

public class King : Piece
{
    public King(Position position, Team team) : base(position, PieceType.KING, team)
    {
    }
}