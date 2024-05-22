using System.Collections.Generic;
using UnityEngine;
using Script;

public class King : Piece
{
    private bool _firstMove { get; set; }
    private bool _canLittleCastle { get; set; }
    private bool _canBigCastle { get; set; }
    
    private void Start()
    {
        _firstMove = true;
        _canLittleCastle = true;
        _canBigCastle = true;
        
        direction = new List<Vector2>
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
            new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };
    }
}