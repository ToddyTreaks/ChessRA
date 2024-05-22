using System.Collections.Generic;
using UnityEngine;
using Script;

public class King : Piece
{
    public bool firstMove;
    public bool mayCastle;
    
    private void Start()
    {
        firstMove = true;
        mayCastle = false;
        
        direction = new List<Vector2>
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
            new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };
    }
}