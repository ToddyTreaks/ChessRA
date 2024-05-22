using System.Collections.Generic;
using UnityEngine;
using Script;
using UnityEngine.UIElements;


public class Pawn : Piece
{
    #region Attributs

    public bool firstMove = true;
    private bool canTake = false;
    private List<Position> allowedPos;
    private Vector2 captureRight;
    private Vector2 captureLeft;

    #endregion

    #region Constructor

    public Pawn(Position position, Team team) : base(position, PieceType.PAWN, team)
    {
        if (team == Team.WHITE)
        {
            direction = new List<Vector2> { new Vector2(1, 0) };
            captureRight = new Vector2(1, 1);
            captureLeft = new Vector2(1, -1);
        }
        else
        {
            direction = new List<Vector2> { new Vector2(-1, 0) };
            captureRight = new Vector2(-1, 1);
            captureLeft = new Vector2(-1, -1);
        }
    }

    #endregion

    #region MoveAllowed

    protected override List<Position> MoveAllowed()
    {
        // DONE : Gérer le cas où le pion peut avancer de 2 cases
        // DONE : Gérer le cas où le pion est bloqué par une autre pièce
        // DONE : Gérer le cas où le pion peut prendre une pièce
        // TODO : Gérer le cas où le pion peut prendre en passant
        // TODO : Gérer le cas où le pion est promu
        allowedPos = Board.CheckMove(this, this.firstMove ? 2 : 1, false);
        CanCapture(captureLeft);
        CanCapture(captureRight);
        return allowedPos;
    }

    #endregion

    #region Capture

    public void CanCapture(Vector2 direction)
    {
        if (Board.BoardArray[(int)(this.actualPosition.xIndex + direction.x),
                (int)(this.actualPosition.yIndex + direction.y)] != null)
        {
            allowedPos.Add(new Position((int)(this.actualPosition.xIndex + direction.x),
                (int)(this.actualPosition.yIndex + direction.y)));
        }
    }

    #endregion
    
}