using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class PieceDic
{
    public PieceType type;
    public Team team;
    public GameObject prefab;
}

public class PhysicalBoard : MonoBehaviour
{
    
    private PhysicalBoard _instance;
    // Start is called before the first frame update
    void Awake()
    {
        if(_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    /*
     * Instantiate the pieces on the board
     * Gestion de movePiece
     */
    [SerializeField] private List<PieceDic> pieces;
    [SerializeField] private Transform origin;
    [SerializeField] private Transform direction;
    [SerializeField] private GameObject previewBlock;


    private Dictionary<(PieceType, Team), GameObject> Pieces;
    private float _xDir;
    private float _zDir;
    private static float _length;
    private static float _height;
    private List<GameObject> _previewBlocks;
    private GameObject _selectedPiece;

    private void Start()
    {
        _xDir = direction.position.x - origin.position.x;
        _zDir = direction.position.z - origin.position.z;
        Pieces = new Dictionary<(PieceType, Team), GameObject>();
        foreach (var piece in pieces)
        {
            Pieces.Add((piece.type, piece.team), piece.prefab);
        }

        _previewBlocks = new List<GameObject>();
    }


    public void Setup(Piece[,] board)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (board[i, j] == null) continue;
                var oui = Instantiate(
                    parent: this.transform,
                    original: Pieces[(board[i, j].type, board[i, j].team)],
                    position: origin.position + new Vector3(i * _xDir, 0, j * _zDir),
                    rotation: direction.rotation);
                if (i == 0 && j == 0)
                {
                    _length = Mathf.Abs(oui.transform.localPosition.x);
                    _height = Mathf.Abs(oui.transform.localPosition.z);
                }

                if (i == 7 && j == 7)
                {
                    _length += Mathf.Abs(oui.transform.localPosition.x);
                    _height += Mathf.Abs(oui.transform.localPosition.z);
                }
            }
        }
    }

    public void ClearBoard()
    {
        foreach (Transform child in origin)
        {
            Destroy(child.gameObject);
        }
    }

    public void DestroyPiece(GameObject piece)
    {
        Destroy(piece);
    }

    public void ChangePiecePosition(GameObject piece, Position position)
    {
        piece.transform.position = origin.position + new Vector3(position.xIndex * _xDir, 0, position.yIndex * _zDir);
    }

    public void SelectPiece(GameObject piece)
    {
        if (!_selectedPiece.IsUnityNull()) SelectNone();
        _selectedPiece = piece;
        piece.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        Position position = GetCoordinatesInBoard(piece);
        Debug.Log("position" + position.xIndex + " " + position.yIndex);
        Piece pieceScript = GameManager.Instance.board.GetPiece(new Position(position.xIndex, position.yIndex));
        //check if good team
        //check if it's the turn of the team
        List<Position> posList = pieceScript.GetMoveSelectedPiece();
        Debug.Log(posList.Count);
        foreach (Position pos in posList)
        {
            //highlight the possible move
            GameObject highlight =
                Instantiate(
                    parent: piece.transform,
                    original: previewBlock);
            highlight.transform.localPosition = new Vector3((float)((pos.xIndex - position.xIndex) * 1.2), 0,
                (float)((pos.yIndex - position.yIndex) * 1.35));
            _previewBlocks.Add(highlight);
        }
    }

    public Position GetCoordinatesInBoard(GameObject obj)
    {
        if (obj == null) return null;

        Position position = new Position((int)((obj.transform.localPosition.x + _length / 2) * 7 / _length),
            (int)((obj.transform.localPosition.z + _height / 2) * 8 / _height));
        return position;
    }

    public void SelectNone()
    {
        if (_selectedPiece.IsUnityNull()) return;
        _selectedPiece.transform.localScale = new Vector3(1, 1, 1);
        _selectedPiece = null;
        foreach (GameObject block in _previewBlocks)
        {
            Destroy(block);
        }

        _previewBlocks.Clear();
    }

    public void MovePiece(Piece piece, Position targetPosition)
    {
        Debug.Log("MovePiece to : " + targetPosition.xIndex + " " + targetPosition.yIndex);
        piece.MovePiece(targetPosition);
        ChangePiecePosition(_selectedPiece, targetPosition);
    }
}