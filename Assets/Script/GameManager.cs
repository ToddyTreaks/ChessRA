using System;
using System.Collections.Generic;
using Script.Boards;
using UnityEngine;
using UnityEngine.Serialization;

namespace Script
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        
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

        public void MovePiece(GameObject colliderGameObject)
        {
            Position newPosition = PreviewBoard.Instance.WhatIsPosition(colliderGameObject);
            PhysicalBoard.Instance.MovePiece(newPosition);
            playerTurn = !playerTurn;
        }

        public void SelectPiece(GameObject colliderGameObject)
        {
            Position positionPiece = PhysicalBoard.Instance.WhatIsPosition(colliderGameObject);
            List<Position> everyPossiblePosition = CalculBoard._instance.MoveAllowed(positionPiece);
            PreviewBoard.Instance.ShowPos(everyPossiblePosition);
        }

        public void Nothing()
        {
            PreviewBoard.Instance.ClearPreview();
        }
    }
}