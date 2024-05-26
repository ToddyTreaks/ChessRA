using System.Collections.Generic;
using System.Net.NetworkInformation;
using Script;
using Script.Boards;
using Unity.VisualScripting;
using UnityEngine;

/*
 * Attributs : tableau de enum de type de pièce & team
 *
 * Initialise le tableau d'enum
 * Fonction MoveVerification qui regarde si une liste de positions est possible ( légalité des positions ), cas de l'échec et cas du mat
 *      renvoie une liste de positions légaux
 *
 */


public class CalculBoard : MonoBehaviour
{
    public static CalculBoard _instance;
    Piece inputPiece;

    // Start is called before the first frame update
    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public List<Position> MoveAllowed(Position positionSelected)
    {
        int maxDistance = 7;
        List<Position> possibleMove = new List<Position>();
        inputPiece = PhysicalBoard.Instance.Array[positionSelected.xIndex, positionSelected.yIndex]
            .GetComponent<Piece>();
        Debug.Log(inputPiece);

        if (inputPiece == null)
        {
            return possibleMove;
        }

        if (inputPiece is Pawn)
        {
            PawnMoveAllowed(positionSelected, possibleMove);
        }
        else
        {
            if (inputPiece is King)
            {
                maxDistance = 1;
                CastleMoveAllowed(positionSelected, possibleMove);
            }

            if (inputPiece is Knight)
            {
                maxDistance = 1;
            }

            DirectionMoveAllowed(positionSelected, possibleMove, maxDistance);
        }

        return possibleMove;
    }

    private void PawnMoveAllowed(Position positionSelected, List<Position> possibleMove)
    {
        int distance = 1;
        Pawn pawn = (Pawn)inputPiece;

        if (pawn.firstMove)
            distance = 2;

        for (int i = 1; i <= distance; i++)
        {
            int xMove = positionSelected.xIndex + (int)inputPiece.direction[0].x * i;
            int yMove = positionSelected.yIndex + (int)inputPiece.direction[0].y * i;
            if (xMove < 0 || yMove < 0)
            {
                break;
            }

            if (PhysicalBoard.Instance.Array[xMove, yMove] != null)
            {
                break;
            }

            possibleMove.Add(new Position(xMove, yMove));
        }

        PawnCaptureMoveAllowed(positionSelected, possibleMove);
    }

    private void PawnCaptureMoveAllowed(Position positionSelected, List<Position> possibleMove)
    {
        // TODO : Verifier si c'est le bon sens il se peux que X et Y doivent être inversé

        int xMove = positionSelected.xIndex + (int)inputPiece.direction[0].x;
        int yMove = positionSelected.yIndex + (int)inputPiece.direction[0].y + 1;
        if (yMove < 0 || yMove > 7) return;
        if (PhysicalBoard.Instance.Array[xMove, yMove] != null &&
            PhysicalBoard.Instance.Array[xMove, yMove].GetComponent<Piece>().team != inputPiece.team)
        {
            possibleMove.Add(new Position(xMove, yMove));
        }

        yMove = positionSelected.yIndex + (int)inputPiece.direction[0].y - 1;
        if (yMove < 0 || yMove > 7) return;
        if (PhysicalBoard.Instance.Array[xMove, yMove] != null &&
            PhysicalBoard.Instance.Array[xMove, yMove].GetComponent<Piece>().team != inputPiece.team)
        {
            possibleMove.Add(new Position(xMove, yMove));
        }
    }

    private void CastleMoveAllowed(Position positionSelected, List<Position> possibleMove)
    {
        King king = (King)inputPiece;
        if (king.firstMove)
        {
            if (PhysicalBoard.Instance.Array[positionSelected.xIndex, 0] != null)
            {
                Piece piece = PhysicalBoard.Instance.Array[positionSelected.xIndex, 0].GetComponent<Piece>();

                if (piece is Rook)
                {
                    Rook rook = (Rook)piece;
                    if (rook.firstMove && PhysicalBoard.Instance.Array[positionSelected.xIndex, 1] == null &&
                        PhysicalBoard.Instance.Array[positionSelected.xIndex, 2] == null &&
                        PhysicalBoard.Instance.Array[positionSelected.xIndex, 3] == null)
                    {
                        possibleMove.Add(new Position(positionSelected.xIndex, 2));
                        king.mayCastle = true;
                    }
                }
            }

            if (PhysicalBoard.Instance.Array[positionSelected.xIndex, 7] != null)
            {
                Piece piece = PhysicalBoard.Instance.Array[positionSelected.xIndex, 7].GetComponent<Piece>();
                if (piece is Rook)
                {
                    Rook rook = (Rook)piece;
                    if (rook.firstMove &&
                        PhysicalBoard.Instance.Array[positionSelected.xIndex, 5] == null &&
                        PhysicalBoard.Instance.Array[positionSelected.xIndex, 6] == null)
                    {
                        possibleMove.Add(new Position(positionSelected.xIndex, 6));
                        king.mayCastle = true;
                    }
                }
            }
        }
        else
            king.mayCastle = false;
    }

    private void DirectionMoveAllowed(Position positionSelected, List<Position> possibleMove, int maxDistance)
    {
        foreach (Vector2 dir in inputPiece.direction)
        {
            for (int i = 1; i <= maxDistance; i++)
            {
                int xMove = positionSelected.xIndex + (int)dir.x * i;
                int yMove = positionSelected.yIndex + (int)dir.y * i;
                if (xMove < 0 || xMove > 7 || yMove < 0 || yMove > 7)
                {
                    break;
                }

                if (PhysicalBoard.Instance.Array[xMove, yMove] != null)
                {
                    if (PhysicalBoard.Instance.Array[xMove, yMove].GetComponent<Piece>().team != inputPiece.team)
                    {
                        possibleMove.Add(new Position(xMove, yMove));
                    }

                    break;
                }
                else
                {
                    possibleMove.Add(new Position(xMove, yMove));
                }
            }
        }
    }


    public List<Position> KingNotInCheck(Position selectedPosition, List<Position> possiblePosition)
    {
        List<Position> legalMove = new List<Position>();
        GameObject pieceGameObject = PhysicalBoard.Instance.Array[selectedPosition.xIndex, selectedPosition.yIndex];
        Debug.Log("GameObject in PieceGameObject : " + pieceGameObject);
        foreach (Position targetPosition in possiblePosition)
        {
            GameObject lastPiece = PhysicalBoard.Instance.Array[targetPosition.xIndex, targetPosition.yIndex];
            PhysicalBoard.Instance.Array[targetPosition.xIndex, targetPosition.yIndex] = pieceGameObject;
            PhysicalBoard.Instance.Array[selectedPosition.xIndex, selectedPosition.yIndex] = null;
            
            Debug.Log("Test 1 : On target position is now : " + PhysicalBoard.Instance.Array[targetPosition.xIndex, targetPosition.yIndex]);
            Debug.Log("Test 1 : On start position is now : " + PhysicalBoard.Instance.Array[selectedPosition.xIndex, selectedPosition.yIndex]);
            
            if (!PutInCheck(pieceGameObject.GetComponent<Piece>().team))
            {
                legalMove.Add(targetPosition);
            }

            PhysicalBoard.Instance.Array[targetPosition.xIndex, targetPosition.yIndex] = lastPiece;
            PhysicalBoard.Instance.Array[selectedPosition.xIndex, selectedPosition.yIndex] = pieceGameObject;
            
            Debug.Log("Test 2 : On target position is now : " + PhysicalBoard.Instance.Array[targetPosition.xIndex, targetPosition.yIndex]);
            Debug.Log("Test 2 : On start position is now : " + PhysicalBoard.Instance.Array[selectedPosition.xIndex, selectedPosition.yIndex]);
        } 

        return legalMove;
    }

    public static bool PutInCheck(Team team)
    {
        foreach (GameObject pieceGameObject in PhysicalBoard.Instance.Array)
        {
            if (pieceGameObject.IsUnityNull()) continue;
            Piece piece = pieceGameObject.GetComponent<Piece>();
            if (pieceGameObject != null && piece.team != team)
            {
                List<Position> attackPos =
                    _instance.MoveAllowed(PhysicalBoard.Instance.WhatIsPosition(pieceGameObject));
                foreach (Position pos in attackPos)
                {
                    if (PhysicalBoard.Instance.Array[pos.xIndex, pos.yIndex] != null &&
                        PhysicalBoard.Instance.Array[pos.xIndex, pos.yIndex].GetComponent<Piece>() is King)
                        return true;
                }
            }
        }

        return false;
    }
}