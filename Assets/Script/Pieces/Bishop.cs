using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    #region Constructor

    private void Start()
    {
        direction = new List<Vector2>
            { new Vector2(1, 1), new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1) };
    }

    #endregion
}
