using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    #region Constructor

    private void Start()
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