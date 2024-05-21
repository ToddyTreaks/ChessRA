using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Piece[,] board = new Piece[8, 8];
    private static Board instance;

    private void Awake()
    {
        instance = this;
    }

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
        
        AddPiece(new Rock(new Position(0,0), Team.WHITE), 0, 0);
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

    public void AddPiece(Piece piece, int xIndex, int yIndex)
    {
        board[xIndex, yIndex] = piece;
    }

    public void RemovePiece(int xIndex, int yIndex)
    {
        board[xIndex, yIndex] = null;
    }
}