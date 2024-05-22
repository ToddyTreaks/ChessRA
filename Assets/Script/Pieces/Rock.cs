using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    public bool firstMove;
    private void Start()
    {
        firstMove = true;
        direction = new List<Vector2> { new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1) };
    }
}