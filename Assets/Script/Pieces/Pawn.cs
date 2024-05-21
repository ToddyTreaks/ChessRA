using System.Collections.Generic;
using UnityEngine;
using Script;

#region Pawn

public class Pawn : Piece
{
    #region Attributs
    
    private bool firstMove = true;
    private bool canTake = false;
    private List<Position> allowedPos;
    
    #endregion
    
    #region Constructor

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
    
    #endregion
    
    #region MoveAllowed

    protected override List<Position> MoveAllowed()
    {
        // TODO : Gérer le cas où le pion est bloqué par une autre pièce
        // TODO : Gérer le cas où le pion peut prendre une pièce
        // TODO : Gérer le cas où le pion peut prendre en passant
        allowedPos = Board.CheckMove(this, direction, 1);

        return allowedPos;
    }
    
    #endregion
}

#endregion

