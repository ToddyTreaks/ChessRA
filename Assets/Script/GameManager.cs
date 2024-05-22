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
            Debug.Log("move");
            Nothing();
            Position newPosition = PreviewBoard.Instance.WhatIsPosition(colliderGameObject);
            
            PhysicalBoard.Instance.MovePiece(newPosition);
            playerTurn = !playerTurn;
        }

        public void SelectPiece(GameObject colliderGameObject)
        {
            Debug.Log("select");
            Position positionPiece = PhysicalBoard.Instance.WhatIsPosition(colliderGameObject);
            PhysicalBoard.Instance.selectedPosition = positionPiece;
            Debug.Log("selected position x : " + positionPiece.xIndex + " y " + positionPiece.yIndex );
            List<Position> everyPossiblePosition = CalculBoard._instance.MoveAllowed(positionPiece);
            List<Position> legalPosition = CalculBoard._instance.KingNotInCheck(positionPiece, everyPossiblePosition);
            Debug.Log(" number of possible posistions " + everyPossiblePosition.Count );
            PreviewBoard.Instance.ShowPos(everyPossiblePosition);
        }

        public void Nothing()
        {
            Debug.Log("nothing");
            PreviewBoard.Instance.ClearPreview();
        }
    }
}