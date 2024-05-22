using System.Collections.Generic;
using Script;
using Script.Boards;
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
        Piece inputPiece = PhysicalBoard.Instance.Array[positionSelected.xIndex, positionSelected.yIndex].GetComponent<Piece>();

        if (inputPiece == null)
        {
            return possibleMove;
        }

        if (inputPiece is Pawn)
        {
            PawnMoveAllowed(positionSelected, inputPiece, possibleMove);
        }
        else
        {
            if (inputPiece is King)
            {
                maxDistance = 1;
                CastleMoveAllowed(inputPiece, possibleMove);
            }

            if (inputPiece is Knight)
            {
                maxDistance = 1;
            }
            DirectionMoveAllowed(positionSelected, possibleMove, maxDistance);
        }
        
        return KingNotInCheck(inputPiece, possibleMove);
    }

    private void DirectionMoveAllowed(Position positionSelected, List<Position> possibleMove, int maxDistance)
    {
        Piece inputPiece = PhysicalBoard.Instance.Array[positionSelected.xIndex, positionSelected.yIndex].GetComponent<Piece>();

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

    
    
    
    
    

    public static bool NotPuttingKingInCheck(Piece piece, Position targetPosition)
    {
        Piece savedPiece = BoardArray[targetPosition.xIndex, targetPosition.yIndex];

        BoardArray[targetPosition.xIndex, targetPosition.yIndex] = piece;
        BoardArray[piece.actualPosition.xIndex, piece.actualPosition.yIndex] = null;

        if (!StillInCheck(piece.team))
        {
            BoardArray[piece.actualPosition.xIndex, piece.actualPosition.yIndex] = piece;
            BoardArray[targetPosition.xIndex, targetPosition.yIndex] = savedPiece;
            return true;
        }

        BoardArray[piece.actualPosition.xIndex, piece.actualPosition.yIndex] = piece;
        BoardArray[targetPosition.xIndex, targetPosition.yIndex] = savedPiece;
        return false;
    }

    public static bool StillInCheck(Team team)
    {
        foreach (Piece piece in BoardArray)
        {
            if (piece != null && piece.team != team)
            {
                List<Position> attackPos = piece.GetMoveSelectedPiece();
                foreach (Position pos in attackPos)
                {
                    if (BoardArray[pos.xIndex, pos.yIndex] != null &&
                        BoardArray[pos.xIndex, pos.yIndex].type == PieceType.KING)
                        return true;
                }
            }
        }

        return false;
    }
}