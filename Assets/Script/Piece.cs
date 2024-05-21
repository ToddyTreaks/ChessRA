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
    public Position actualPosition;
    public PieceType type;
    public Team team;
    public List<Vector2> direction;

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
        return Board.CheckMove(this, direction);
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
        if (team == Team.WHITE)
        {
            direction = new List<Vector2> { new Vector2(1, 0) };
        }
        else
        {
            direction = new List<Vector2> { new Vector2(-1, 0) };
        }
    }

    protected override List<Position> MoveAllowed()
    {
        // TODO : Gérer le cas où le pion est bloqué par une autre pièce
        // TODO : Gérer le cas où le pion peut prendre une pièce
        // TODO : Gérer le cas où le pion peut prendre en passant
        List<Position> allowedPos = Board.CheckMove(this, direction, 1);

        return allowedPos;
    }
}

#endregion

#region Rock

public class Rock : Piece
{
    public bool firstMove = true;

    public Rock(Position position, Team team) : base(position, PieceType.ROCK, team)
    {
        direction = new List<Vector2> { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
    }
}

#endregion

#region Knight

public class Knight : Piece
{
    public Knight(Position position, Team team) : base(position, PieceType.KNIGHT, team)
    {
        direction = new List<Vector2>
        {
            new Vector2(2, 1), new Vector2(2, -1), new Vector2(-2, 1), new Vector2(-2, -1), new Vector2(1, 2),
            new Vector2(1, -2), new Vector2(-1, 2), new Vector2(-1, -2)
        };
    }

    protected override List<Position> MoveAllowed()
    {
        return Board.CheckMove(this, direction, 1);
    }
}

#endregion

#region Bishop

public class Bishop : Piece
{
    public Bishop(Position position, Team team) : base(position, PieceType.BISHOP, team)
    {
        direction = new List<Vector2>
            { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
    }
}

#endregion

#region Queen

public class Queen : Piece
{
    public Queen(Position position, Team team) : base(position, PieceType.QUEEN, team)
    {
        direction = new List<Vector2>
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
            new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };
    }
}

#endregion

#region King

public class King : Piece
{
    private bool firstMove = true;

    public King(Position position, Team team) : base(position, PieceType.KING, team)
    {
        direction = new List<Vector2>
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
            new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };
    }

    protected override List<Position> MoveAllowed()
    {
        List<Position> allowedPos = Board.CheckMove(this, direction, 1);

        // TODO : Gérer le cas où le déplacement du roi met le roi en échec
        // TODO : Gérer le cas où le roi peut roquer
        // TODO : Gérer le cas où le roi est en échec
        // TODO : Gérer le cas où le roi est en échec et mat
        // TODO : Gérer le cas où le roi est en pat


        return allowedPos;
    }

    private void Rock()
    {
        // TODO FINIR LE ROCK
        if (firstMove)
        {
            if (Board.BoardArray[0, actualPosition.yIndex].type == PieceType.ROCK) ;
            {
                Rock rock = (Rock)Board.BoardArray[0, actualPosition.yIndex];
                if (rock.firstMove)
                {
                    if (Board.BoardArray[1, actualPosition.yIndex] == null &&
                        Board.BoardArray[2, actualPosition.yIndex] == null && Board.BoardArray[3, actualPosition.yIndex] == null)
                    {
                        MovePiece(this, new Position(2, actualPosition.yIndex));
                        MovePiece(rock, new Position(3, actualPosition.yIndex));
                    }
                }
            }
        }
    }

    private void BigRock()
    {
        // TODO : Gérer le cas où le roi peut roquer grand roque
    }
}

#endregion