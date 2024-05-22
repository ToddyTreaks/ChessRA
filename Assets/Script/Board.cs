using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

namespace Script
{
    public class Board : MonoBehaviour
    {
        #region variables

        public static Piece[,] BoardArray;

        public static bool isCheck = false;
        public static bool isCheckMate = false;

        #endregion

        #region Initialisation

        private void Start()
        {
            for (var line = 0; line < 8; line++)
            {
                for (var col = 0; col < 8; col++)
                {
                    Team team = line < 4 ? Team.WHITE : Team.BLACK;
                    Position pos = new(line, col);
                    Piece piece = CreatePiece(line, col, team, pos);
                    AddPiece(piece, line, col);
                }
            }

            GameManager.Instance.setupBoard.Setup(BoardArray);
        }

        private Piece CreatePiece(int line, int col, Team team, Position pos)
        {
            switch (line)
            {
                case 0:
                case 7:
                    switch (col)
                    {
                        case 0:
                        case 7: return new Rock(pos, team);
                        case 1:
                        case 6: return new Knight(pos, team);
                        case 2:
                        case 5: return new Bishop(pos, team);
                        case 3: return new Queen(pos, team);
                        case 4: return new King(pos, team);
                    }

                    break;
                case 1:
                case 6: return new Pawn(pos, team);
            }

            return null;
        }

        #endregion

        #region Modification Board

        public static void AddPiece(Piece piece, int xIndex, int yIndex)
        {
            BoardArray[xIndex, yIndex] = piece;
        }

        public static void RemovePiece(int xIndex, int yIndex)
        {
            BoardArray[xIndex, yIndex] = null;
        }

        #endregion

        #region VerificationMove

        public static List<Position> CheckMove(Piece inputPiece, List<Vector2> direction, int maxDistance = 7)
        {
            List<Position> possibleMove = new List<Position>();

            foreach (Vector2 dir in direction)
            {
                for (int i = 0; i <= maxDistance; i++)
                {
                    int xMove = inputPiece.actualPosition.xIndex + (int)dir.x * i;
                    int yMove = inputPiece.actualPosition.yIndex + (int)dir.y * i;

                    if (xMove < 0 || xMove > 7 || yMove < 0 || yMove > 7)
                    {
                        break;
                    }

                    if (BoardArray[xMove, yMove] != null)
                    {
                        if (BoardArray[xMove, yMove].team != inputPiece.team)
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
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}