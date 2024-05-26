using System.Collections.Generic;
using UnityEngine;
using Script;
using Unity.VisualScripting;


public class Pawn : Piece
{
    public bool firstMove { get; set; }
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
        // TODO : Créer un menu pour choisir la promotion
        // TODO : Renvoyer la pièce choisie
        // set active true canvas named CanvasPromotion
        GameManager.Instance.canClick = false;
        GameObject canvasPromotion = GameManager.Instance.CanvasPromotion;
        if (!canvasPromotion.IsUnityNull())
        {
            canvasPromotion.SetActive(true);
        }
    }
}