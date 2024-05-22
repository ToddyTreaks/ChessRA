using System;
using UnityEngine;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public SetupBoard setupBoard;

        private bool playerTurn = false;
        private bool isAPieceSelected = false;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            // TODO : Si input && isAPieceSelected - Appeler Piece.GetMoveSelectedPiece() ssi la pièce appartient au joueur courant
            // TODO : Choisir une autre piece
            // TODO : Si Piece.MovePiece() est possible alors bouger sa pièce
            // TODO : Changer Player
        }

        private void Turn(bool playerTurn)
        {
            this.playerTurn = !playerTurn;
        }
    }
}