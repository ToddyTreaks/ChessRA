using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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

    private void Awake()
    {
        _xDir = direction.position.x - origin.position.x;
        _zDir = direction.position.z - origin.position.z;
        Pieces = new Dictionary<(PieceType, Team), GameObject>();
        foreach (var piece in pieces)
        {
            Pieces.Add((piece.type, piece.team), piece.prefab);
        }
    }


    public void Setup(Piece[,] board){
        for( int i = 0; i <  8; i++){
            for(int j = 0; j <  8; j++)
            {
                if(board[i,j] == null) continue;
                Instantiate(
                    parent: this.transform,
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
