using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    private void Start()
    {
        direction = new List<Vector2>
        {
            new Vector2(1, 0), new Vector2(-1, 0), new Vector2(0, 1), new Vector2(0, -1), new Vector2(1, 1),
            new Vector2(1, -1), new Vector2(-1, 1), new Vector2(-1, -1)
        };
    }
}
