using System;
using System.Collections.Generic;
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
    public Position actualPosition;
    public PieceType type;
    public Team team;

    public Piece(Position position, PieceType type, Team team)
    {
        actualPosition = position;
        this.type = type;
        this.team = team;
    }

    public List<Position> SelectedPiece()
    {
        return MoveAllowed();
    }

    protected virtual List<Position> MoveAllowed()
    {
        return new List<Position>();
    }

    public void MovePiece(Piece piece, Position targetPosition)
    {
        /*Attention si ça pète c'est ici*/
        Board.AddPiece(piece, targetPosition.xIndex, targetPosition.yIndex);
        Board.RemovePiece(piece.actualPosition.xIndex, piece.actualPosition.yIndex);
        Debug.Log("Move Piece : Pièce déplacé & ça ne pète pas");
    }
}

#endregion

#region Pawn

public class Pawn : Piece
{
    private bool firstMove = true;
    private bool canTake = false;

    public Pawn(Position position, Team team) : base(position, PieceType.PAWN, team)
    {
    }

    protected override List<Position> MoveAllowed()
    {
        List<Position> targetablePos = new List<Position>();

        if (team == Team.WHITE)
        {
            if (firstMove)
            {
                int distance = Board.CheckMoveUp(this, 2);
                for (int i = 1; i < distance; i++)
                {
                    targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex));
                }

                firstMove = false;
            }
            else
            {
                int distance = Board.CheckMoveUp(this, 1);
                if (distance == 1)
                {
                    targetablePos.Add(new Position(this.actualPosition.xIndex + 1, this.actualPosition.yIndex));
                }
            }
        }
        else
        {
            if (firstMove)
            {
                int distance = Board.CheckMoveDown(this, 2);
                for (int i = 1; i < distance; i++)
                {
                    targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex));
                }

                firstMove = false;
            }
            else
            {
                int distance = Board.CheckMoveDown(this, 1);
                if (distance == 1)
                {
                    targetablePos.Add(new Position(this.actualPosition.xIndex - 1, this.actualPosition.yIndex));
                }
            }
        }

        return targetablePos;
    }
}

#endregion

#region Rock

public class Rock : Piece
{
    private bool firstMove = true;

    public Rock(Position position, Team team) : base(position, PieceType.ROCK, team)
    {
    }

    protected override List<Position> MoveAllowed()
    {
        List<Position> targetablePos = new List<Position>();

        int distanceUp = Board.CheckMoveUp(this);
        int distanceDown = Board.CheckMoveDown(this);
        int distanceLeft = Board.CheckMoveLeft(this);
        int distanceRight = Board.CheckMoveRight(this);

        for (int i = 1; i < distanceUp; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex));
        }

        for (int i = 1; i < distanceDown; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex));
        }

        for (int i = 1; i < distanceLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex, this.actualPosition.yIndex + i));
        }

        return targetablePos;
    }
}

#endregion

#region Knight

public class Knight : Piece
{
    public Knight(Position position, Team team) : base(position, PieceType.KNIGHT, team)
    {
    }

    protected override List<Position> MoveAllowed()
    {
        List<Position> targetablePos = new List<Position>();

        List<Position> knightPos = KnightPos();

        foreach (var pos in knightPos)
        {
            if (Board.board[pos.xIndex, pos.yIndex] == null || Board.board[pos.xIndex, pos.yIndex].team != this.team)
                targetablePos.Add(pos);
        }

        return targetablePos;
    }

    private List<Position> KnightPos()
    {
        List<Position> knightPos = new List<Position>();

        if (this.actualPosition.xIndex + 2 < 8 && this.actualPosition.yIndex + 1 < 8)
            knightPos.Add(new Position(this.actualPosition.xIndex + 2, this.actualPosition.yIndex + 1));
        if (this.actualPosition.xIndex + 2 < 8 && this.actualPosition.yIndex - 1 >= 0)
            knightPos.Add(new Position(this.actualPosition.xIndex + 2, this.actualPosition.yIndex - 1));
        if (this.actualPosition.xIndex - 2 >= 0 && this.actualPosition.yIndex + 1 < 8)
            knightPos.Add(new Position(this.actualPosition.xIndex - 2, this.actualPosition.yIndex + 1));
        if (this.actualPosition.xIndex - 2 >= 0 && this.actualPosition.yIndex - 1 >= 0)
            knightPos.Add(new Position(this.actualPosition.xIndex - 2, this.actualPosition.yIndex - 1));
        if (this.actualPosition.xIndex + 1 < 8 && this.actualPosition.yIndex + 2 < 8)
            knightPos.Add(new Position(this.actualPosition.xIndex + 1, this.actualPosition.yIndex + 2));
        if (this.actualPosition.xIndex + 1 < 8 && this.actualPosition.yIndex - 2 >= 0)
            knightPos.Add(new Position(this.actualPosition.xIndex + 1, this.actualPosition.yIndex - 2));
        if (this.actualPosition.xIndex - 1 >= 0 && this.actualPosition.yIndex + 2 < 8)
            knightPos.Add(new Position(this.actualPosition.xIndex - 1, this.actualPosition.yIndex + 2));
        if (this.actualPosition.xIndex - 1 >= 0 && this.actualPosition.yIndex - 2 >= 0)
            knightPos.Add(new Position(this.actualPosition.xIndex - 1, this.actualPosition.yIndex - 2));

        return KnightPos();
    }
}

#endregion

#region Bishop

public class Bishop : Piece
{
    public Bishop(Position position, Team team) : base(position, PieceType.BISHOP, team)
    {
    }

    protected override List<Position> MoveAllowed()
    {
        List<Position> targetablePos = new List<Position>();

        int distanceUpLeft = Board.CheckMoveUpLeft(this);
        int distanceUpRight = Board.CheckMoveUpRight(this);
        int distanceDownLeft = Board.CheckMoveDownLeft(this);
        int distanceDownRight = Board.CheckMoveDownRight(this);

        for (int i = 1; i < distanceUpLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceUpRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex + i));
        }

        for (int i = 1; i < distanceDownLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceDownRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex + i));
        }

        return targetablePos;
    }
}

#endregion

#region Queen

public class Queen : Piece
{
    public Queen(Position position, Team team) : base(position, PieceType.QUEEN, team)
    {
    }

    protected override List<Position> MoveAllowed()
    {
        List<Position> targetablePos = new List<Position>();

        int distanceUp = Board.CheckMoveUp(this);
        int distanceDown = Board.CheckMoveDown(this);
        int distanceLeft = Board.CheckMoveLeft(this);
        int distanceRight = Board.CheckMoveRight(this);
        int distanceUpLeft = Board.CheckMoveUpLeft(this);
        int distanceUpRight = Board.CheckMoveUpRight(this);
        int distanceDownLeft = Board.CheckMoveDownLeft(this);
        int distanceDownRight = Board.CheckMoveDownRight(this);

        for (int i = 1; i < distanceUp; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex));
        }

        for (int i = 1; i < distanceDown; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex));
        }

        for (int i = 1; i < distanceLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex, this.actualPosition.yIndex + i));
        }

        for (int i = 1; i < distanceUpLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceUpRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex + i));
        }

        for (int i = 1; i < distanceDownLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceDownRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex + i));
        }

        return targetablePos;
    }
}

#endregion

#region King

public class King : Piece
{
    private bool firstMove = true;

    public King(Position position, Team team) : base(position, PieceType.KING, team)
    {
    }

    protected override List<Position> MoveAllowed()
    {
        List<Position> targetablePos = new List<Position>();

        int distanceUp = Board.CheckMoveUp(this, 1);
        int distanceDown = Board.CheckMoveDown(this, 1);
        int distanceLeft = Board.CheckMoveLeft(this, 1);
        int distanceRight = Board.CheckMoveRight(this, 1);
        int distanceUpLeft = Board.CheckMoveUpLeft(this, 1);
        int distanceUpRight = Board.CheckMoveUpRight(this, 1);
        int distanceDownLeft = Board.CheckMoveDownLeft(this, 1);
        int distanceDownRight = Board.CheckMoveDownRight(this, 1);

        for (int i = 1; i < distanceUp; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex));
        }

        for (int i = 1; i < distanceDown; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex));
        }

        for (int i = 1; i < distanceLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex, this.actualPosition.yIndex + i));
        }

        for (int i = 1; i < distanceUpLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceUpRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex + i, this.actualPosition.yIndex + i));
        }

        for (int i = 1; i < distanceDownLeft; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex - i));
        }

        for (int i = 1; i < distanceDownRight; i++)
        {
            targetablePos.Add(new Position(this.actualPosition.xIndex - i, this.actualPosition.yIndex + i));
        }

        return targetablePos;
    }
}

#endregion