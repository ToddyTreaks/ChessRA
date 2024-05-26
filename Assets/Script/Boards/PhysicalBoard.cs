using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Script.Boards
{
    /*
     * Instantiate the pieces on the board
     * Gestion de movePiece
     */
    public class PhysicalBoard : MonoBehaviour
    {
        /*
         * 0 : Pawn
         * 1 : Rock
         * 2 : Knight
         * 3 : Bishop
         * 4 : Queen
         * 5 : King
         */
        [SerializeField] private List<GameObject> piecesPrefab;
        [SerializeField] private Material blackMaterial;
        [SerializeField] private Transform parent;

        public Position selectedPosition;
        public GameObject[,] Array;
        public static PhysicalBoard Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }
        }

        private void Start()
        {
            Array = new GameObject[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    GameObject toCopy = CreatePiece(i, j);
                    if(toCopy.IsUnityNull()) continue;
                    GameObject piece = Instantiate( 
                       original: toCopy,
                       position: new Vector3(i, 0, -j),
                       rotation: Quaternion.identity,
                       parent: parent);
                    piece.GetComponent<Piece>().team = i > 4 ? Team.BLACK : Team.WHITE;
                    piece.GetComponent<MeshRenderer>().sharedMaterial = i >4? blackMaterial : piecesPrefab[0].GetComponent<MeshRenderer>().sharedMaterial;
                    Array[i, j] = piece;
                }
            }
            parent.transform.localScale = new Vector3((float)0.015, (float)0.015, (float)0.015);
        }

        private GameObject CreatePiece(int line, int col)
        {
            switch (line)
            {
                case 0:
                case 7:
                    switch (col)
                    {
                        case 0: case 7: return piecesPrefab[1];
                        case 1: case 6: return piecesPrefab[2];
                        case 2: case 5: return piecesPrefab[3];
                        case 3:         return piecesPrefab[4];
                        case 4:         return piecesPrefab[5];
                    }

                    break;
                case 1:
                case 6: return piecesPrefab[0];
                default: return null;
            }

            return null;
        }

        public void MovePiece(Position position)
        {
            if (selectedPosition.IsUnityNull()) return;
            Piece piece = Array[selectedPosition.xIndex, selectedPosition.yIndex].GetComponent<Piece>();

            if (piece is King)
            {
                King king = (King)piece;
                if (king.mayCastle)
                {
                    switch (position.yIndex)
                    {
                        case 2:
                            FastMove(new Position(position.xIndex, 2));
                            selectedPosition = new Position(position.xIndex, 0);
                            FastMove(new Position(position.xIndex, 3));
                            break;
                        case 6:
                            FastMove(new Position(position.xIndex, 6));
                            selectedPosition = new Position(position.xIndex, 7);
                            FastMove(new Position(position.xIndex, 5));
                            break;
                        default:
                            FastMove(position);
                            break;
                    }
                    king.mayCastle = false;
                    return;
                }
            }

            if(piece is Pawn)
            {
                Pawn pawn = (Pawn)piece;
                if (pawn.firstMove)
                    pawn.firstMove = false;
            }
            FastMove(position);
            
            if (piece is Pawn && (position.xIndex == 0 || position.xIndex == 7))
            {
                selectedPosition = position;
                Debug.Log("Promote");
                Pawn pawn = (Pawn)piece;
                pawn.Promote();
            }
        }

        private void FastMove(Position position)
        {
            GameObject todie = Instance.Array[position.xIndex, position.yIndex];
            if (!todie.IsUnityNull())
            {
                todie.SetActive(false);
            }
            parent.transform.localScale = new Vector3((float)1, (float)1, (float)1);
            Array[selectedPosition.xIndex, selectedPosition.yIndex].transform.position =
                new Vector3(position.xIndex, 0, -position.yIndex);
            Array[position.xIndex, position.yIndex] = Array[selectedPosition.xIndex, selectedPosition.yIndex];
            Array[selectedPosition.xIndex, selectedPosition.yIndex] = null;
            parent.transform.localScale = new Vector3((float)0.015, (float)0.015, (float)0.015);
        }

        public Position WhatIsPosition(GameObject piece)
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    if (Array[i, j] == piece)
                        return new Position(i, j);
                }
            }

            return new Position(-1, -1);
        }
        public void PromoteTo(GameObject prefab)
        {
            GameObject canvasPromotion = GameManager.Instance.CanvasPromotion;
            if (!canvasPromotion.IsUnityNull())
            {
                canvasPromotion.SetActive(false);
            }
            parent.transform.localScale = new Vector3((float)1, (float)1, (float)1);
            GameManager.Instance.canClick = true;
            Debug.Log(selectedPosition.xIndex + " " + selectedPosition.yIndex);
            var i = selectedPosition.xIndex;
            var j = selectedPosition.yIndex;
            Array[i, j].SetActive(false);
            GameObject piece = Instantiate( 
                original: prefab,
                position: new Vector3(i, 0, -j),
                rotation: Quaternion.identity,
                parent: parent);
            piece.GetComponent<Piece>().team = i < 4 ? Team.BLACK : Team.WHITE;
            piece.GetComponent<MeshRenderer>().sharedMaterial = i < 4? blackMaterial : piecesPrefab[0].GetComponent<MeshRenderer>().sharedMaterial;
            Array[i, j] = piece;
            parent.transform.localScale = new Vector3((float)0.015, (float)0.015, (float)0.015);
        }
    }
}