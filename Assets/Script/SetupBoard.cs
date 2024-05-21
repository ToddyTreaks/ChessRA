using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PieceDic
{
    public PieceType type;
    public Team team;
    public GameObject prefab;
}

public class SetupBoard : MonoBehaviour
{
    [SerializeField] private List<PieceDic> pieces;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform direction;

    private Dictionary<(PieceType, Team), GameObject> Pieces;
    private float _xDir;
    private float _zDir;

    private void Start()
    {
        _xDir = origin.position.x - direction.position.x;
        _zDir = origin.position.z - direction.position.z;
        Pieces = new Dictionary<(PieceType, Team), GameObject>();
        foreach (var piece in pieces)
        {
            Pieces.Add((piece.type, piece.team), piece.prefab);
        }
    }


    public void Setup(Piece[,] board){
        for( int i = 0; i < board.Length; i++){
            for(int j = 0; j < board.Length; j++){
                Instantiate(
                    original: Pieces[(board[i,j].type, board[i,j].team)], 
                    position: origin.position + new Vector3(i* _xDir, 0, j*_zDir), 
                    rotation: direction.rotation);
            }
        }
    }
    
    public void ClearBoard(){
        foreach(Transform child in origin){
            Destroy(child.gameObject);
        }
    }
    
    public void DestroyPiece(GameObject piece){
        Destroy(piece);
    }
    
    public void ChangePiecePosition(GameObject piece, Position position){
        piece.transform.position = origin.position + new Vector3(position.xIndex * _xDir, 0, position.yIndex * _zDir);
    }
}
