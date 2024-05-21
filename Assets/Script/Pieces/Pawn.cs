using System.Collections.Generic;
using UnityEngine;
using Script;
using UnityEngine.UIElements;


public class Pawn : Piece
{
    #region Attributs

    private bool firstMove = true;
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
        // TODO : Gérer le cas où le pion est bloqué par une autre pièce
        // DONE : Gérer le cas où le pion peut prendre une pièce
        // TODO : Gérer le cas où le pion peut prendre en passant
        allowedPos = Board.CheckMove(this, direction, 1);
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

    public override bool CanCaptureOpponentKing()
    {
        if (Board.BoardArray[(int)(this.actualPosition.xIndex + captureLeft.x),
                (int)(this.actualPosition.yIndex + captureLeft.y)].type == PieceType.KING ||
            Board.BoardArray[(int)(this.actualPosition.xIndex + captureRight.x),
                (int)(this.actualPosition.yIndex + captureRight.y)].type == PieceType.KING)
        {
            return true;
        }

        return false;
    }

    #endregion
}