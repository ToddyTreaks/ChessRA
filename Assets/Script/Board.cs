using System;
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
    
    #region CheckMoveDirection
    
    public static int CheckMoveUp(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (y + i > 7)
            {
                return i - 1; // Cas hors plateau
            }

            if (board[x, y + i] != null)
            {
                if (board[x, y + i].type == PieceType.PAWN)
                {
                    return i - 1;
                }
                else if (board[x, y + i].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveUp : déplacement impossible");
    }

    public static int CheckMoveDown(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (y - i < 0)
            {
                return i - 1;
            }

            if (board[x, y - i] != null)
            {
                if (board[x, y - i].type == PieceType.PAWN)
                {
                    return i - 1;
                }
                else if (board[x, y - i].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveDown : déplacement impossible");
    }

    public static int CheckMoveRight(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (x + i > 7)
            {
                return i - 1;
            }

            if (board[x + i, y] != null)
            {
                if (board[x + i, y].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveRight : déplacement impossible");
    }

    public static int CheckMoveLeft(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (x - i < 0)
            {
                return i - 1;
            }

            if (board[x - i, y] != null)
            {
                if (board[x - i, y].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveLeft : déplacement impossible");
    }

    public static int CheckMoveUpRight(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (x + i > 7 || y + i > 7)
            {
                return i - 1;
            }

            if (board[x + i, y + i] != null)
            {
                if (board[x + i, y + i].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveUpRight : déplacement impossible");
    }

    public static int CheckMoveUpLeft(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (x - i < 0 || y + i > 7)
            {
                return i - 1;
            }

            if (board[x - i, y + i] != null)
            {
                if (board[x - i, y + i].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveUpLeft : déplacement impossible");
    }

    public static int CheckMoveDownRight(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (x + i > 7 || y - i < 0)
            {
                return i - 1;
            }

            if (board[x + i, y - i] != null)
            {
                if (board[x + i, y - i].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveDownRight : déplacement impossible");
    }

    public static int CheckMoveDownLeft(Piece inputPiece, int maxDistance = 7)
    {
        int x = inputPiece.actualPosition.xIndex;
        int y = inputPiece.actualPosition.yIndex;

        for (int i = 1; i <= maxDistance; i++)
        {
            if (x - i < 0 || y - i < 0)
            {
                return i - 1;
            }

            if (board[x - i, y - i] != null)
            {
                if (board[x - i, y - i].team != inputPiece.team)
                {
                    return i;
                }

                return i - 1;
            }
        }

        throw new Exception("CheckMoveDownLeft : déplacement impossible");
    }
    
    #endregion
}