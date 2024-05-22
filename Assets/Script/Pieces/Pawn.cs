using System.Collections.Generic;
using UnityEngine;
using Script;
using Unity.VisualScripting;


public class Pawn : Piece
{
    private bool firstMove { get; set; }
    private void Start()
    {
        firstMove = true;

        if (team == Team.WHITE)
        {
            direction = new List<Vector2> { new Vector2(1, 0) };
        }
        else
        {
            direction = new List<Vector2> { new Vector2(-1, 0) };
        }
    }

    public void Promote()
    {
        // TODO : Gérer le cas où le pion est promu en autre chose qu'une reine
        // TODO : Gérer la destruction du pion ( on fait hériter Piece de MonoBehaviour ou pas ? )
    }
}