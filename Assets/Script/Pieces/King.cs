using System.Collections.Generic;
using UnityEngine;
using Script;

public class King : Piece
{
    #region Attributs

    private bool _firstMove = true;
    private bool _canLittleCastle = true;
    private bool _canBigCastle = true;
    private List<Position> _allowedPos;

    #endregion

    #region Constructor

    public King(Position position, Team team) : base(position, PieceType.KING, team)
    {
        direction = new List<Vector2>
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
            new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };
    }

    #endregion

    #region MoveAllowed

    protected override List<Position> PieceAllowedMove()
    {
        _allowedPos = Board.CheckMove(this, 1);


        // DONE : Gérer le cas où le roi peut roquer
        // TODO : Gérer le cas où le déplacement du roi met le roi en échec
        // TODO : Gérer le cas où le roi est en échec
        // TODO : Gérer le cas où le roi est en échec et mat
        // TODO : Gérer le cas où le roi est en pat ? (pas sûr) 


        return _allowedPos;
    }

    #region Castle

    private bool CanLittleCastle(Rock littleRock)
    {
        if (_firstMove && littleRock.FirstMove && Board.BoardArray[5, this.actualPosition.xIndex] == null &&
            Board.BoardArray[6, this.actualPosition.xIndex] == null)
            return true;
        return false;
    }

    private bool CanBigCastle(Rock littleRock)
    {
        if (_firstMove && littleRock.FirstMove && Board.BoardArray[3, this.actualPosition.xIndex] == null &&
            Board.BoardArray[2, this.actualPosition.xIndex] == null &&
            Board.BoardArray[1, this.actualPosition.xIndex] == null)
            return true;
        return false;
    }

    #endregion

    #endregion

    #region Movement

    protected override void PieceMovement(Position targetPosition)
    {
        if (_firstMove && targetPosition.xIndex == 6)
        {
            MoveLittleCastle((Rock)Board.BoardArray[7, this.actualPosition.yIndex]);
            DisableCastle();
        }
        else if (_firstMove && targetPosition.xIndex == 2)
        {
            MoveBigCastle((Rock)Board.BoardArray[0, this.actualPosition.yIndex]);
            DisableCastle();
        }
        else
        {
            base.PieceMovement(targetPosition);
        }
    }

    #region Castling

    private void MoveLittleCastle(Rock littleRock)
    {
        Board.RemovePiece(this.actualPosition.xIndex, this.actualPosition.yIndex);
        Board.AddPiece(this, 6, this.actualPosition.yIndex);
        Board.RemovePiece(littleRock.actualPosition.xIndex, littleRock.actualPosition.yIndex);
        Board.AddPiece(littleRock, 5, littleRock.actualPosition.yIndex);
    }

    private void MoveBigCastle(Rock bigRock)
    {
        Board.RemovePiece(this.actualPosition.xIndex, this.actualPosition.yIndex);
        Board.AddPiece(this, 2, this.actualPosition.yIndex);
        Board.RemovePiece(bigRock.actualPosition.xIndex, bigRock.actualPosition.yIndex);
        Board.AddPiece(bigRock, 3, bigRock.actualPosition.yIndex);
    }

    private void DisableCastle()
    {
        _firstMove = false;
        _canBigCastle = false;
        _canLittleCastle = false;
    }

    #endregion

    #endregion
}