using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Board : MonoBehaviour
{
    #region variables
    
    public static Piece[,] board = new Piece[8, 8];
    
    #endregion

    #region Initialisation
    
    private void Start()
    {
        for (int i = 2; i < 6; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                board[i, j] = null;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            AddPiece(new Pawn(new Position(1, i), Team.WHITE), 1, i);
            AddPiece(new Pawn(new Position(6, i), Team.BLACK), 6, i);
        }

        AddPiece(new Rock(new Position(0, 0), Team.WHITE), 0, 0);
        AddPiece(new Rock(new Position(0, 7), Team.WHITE), 0, 7);
        AddPiece(new Rock(new Position(7, 0), Team.BLACK), 7, 0);
        AddPiece(new Rock(new Position(7, 7), Team.BLACK), 7, 7);

        AddPiece(new Knight(new Position(0, 1), Team.WHITE), 0, 1);
        AddPiece(new Knight(new Position(0, 6), Team.WHITE), 0, 6);
        AddPiece(new Knight(new Position(7, 1), Team.BLACK), 7, 1);
        AddPiece(new Knight(new Position(7, 6), Team.BLACK), 7, 6);

        AddPiece(new Bishop(new Position(0, 2), Team.WHITE), 0, 2);
        AddPiece(new Bishop(new Position(0, 5), Team.WHITE), 0, 5);
        AddPiece(new Bishop(new Position(7, 2), Team.BLACK), 7, 2);
        AddPiece(new Bishop(new Position(7, 5), Team.BLACK), 7, 5);

        AddPiece(new Queen(new Position(0, 3), Team.WHITE), 0, 3);
        AddPiece(new Queen(new Position(7, 3), Team.BLACK), 7, 3);

        AddPiece(new King(new Position(0, 4), Team.WHITE), 0, 4);
        AddPiece(new King(new Position(7, 4), Team.BLACK), 7, 4);
    }
    
    #endregion

    #region Modification Board
    public static void AddPiece(Piece piece, int xIndex, int yIndex)
    {
        board[xIndex, yIndex] = piece;
    }

    public static void RemovePiece(int xIndex, int yIndex)
    {
        board[xIndex, yIndex] = null;
    }
    
    #endregion

    #region CheckMove
    
    public static List<Position> CheckMove(Piece inputPiece, List<Vector2> direction, int maxDistance = 7)
    {
        List<Position> possibleMove = new List<Position>();
        
        foreach( Vector2 dir in direction )
        {
            for (int i = 0; i <= maxDistance; i++)
            {
                int xMove = inputPiece.actualPosition.xIndex + (int)dir.x * i;
                int yMove = inputPiece.actualPosition.yIndex + (int)dir.y * i;
                
                if (xMove < 0 || xMove > 7 || yMove < 0 || yMove > 7)
                {
                    break;
                }
                
                if (board[xMove, yMove] != null)
                {
                    if (board[xMove, yMove].team != inputPiece.team)
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
        return possibleMove;
    }
    
    #endregion
}