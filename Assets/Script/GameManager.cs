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
        
        public GameObject CanvasPromotion;
        
        public bool canClick = true;
        private bool playerTurn = true;
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
        public void MovePiece(GameObject colliderGameObject)
        {
            Nothing();
            Position newPosition = PreviewBoard.Instance.WhatIsPosition(colliderGameObject);
            
            PhysicalBoard.Instance.MovePiece(newPosition);
            playerTurn = !playerTurn;
        }

        public void SelectPiece(GameObject colliderGameObject)
        {
            //check if the piece team is corresponding to its turn
            if (!playerTurn && colliderGameObject.GetComponent<Piece>().team == Team.WHITE || 
                playerTurn && colliderGameObject.GetComponent<Piece>().team == Team.BLACK) return;
            PreviewBoard.Instance.ClearPreview();
            Position positionPiece = PhysicalBoard.Instance.WhatIsPosition(colliderGameObject);
            PhysicalBoard.Instance.selectedPosition = positionPiece;
            List<Position> everyPossiblePosition = CalculBoard._instance.MoveAllowed(positionPiece);
            List<Position> legalPosition = CalculBoard._instance.KingNotInCheck(positionPiece, everyPossiblePosition);
            PreviewBoard.Instance.ShowPos(legalPosition);
        }

        public void Nothing()
        {
            PreviewBoard.Instance.ClearPreview();
        }

    }
}