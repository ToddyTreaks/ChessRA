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

        public static Position selectedPosition;
        public static GameObject[,] Array;
        public static PhysicalBoard Instance;

        void Awake()
        {
            if(Instance == null)
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
                    GameObject piece = Instantiate( toCopy, new Vector3(i, 0, j), Quaternion.identity);
                    piece.GetComponent<MeshRenderer>().material = (i + j) % 2 == 0 ? blackMaterial : piecesPrefab[0].GetComponent<MeshRenderer>().material;
                    Array[i, j] = piece;
                }
            }
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
                        case 4:         return piecesPrefab[4];
                        case 3:         return piecesPrefab[5];
                    }
                    break;
                case 1: case 6: return piecesPrefab[0];
                default: return null;
            }
            return null;
        }

        public static void MovePiece(Position position)
        {
            if (selectedPosition.IsUnityNull()) return;
            Destroy(Array[position.xIndex,position.yIndex]);
            Array[selectedPosition.xIndex, selectedPosition.yIndex].transform.position =
                new Vector3(position.xIndex, 0, position.yIndex);
            Array[position.xIndex, position.yIndex] = Array[selectedPosition.xIndex, selectedPosition.yIndex];
            Array[selectedPosition.xIndex, selectedPosition.yIndex] = null;
        }

        public static Position WhatIsPosition(GameObject piece)
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
        
    }
}