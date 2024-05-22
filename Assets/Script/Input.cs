using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Input : MonoBehaviour
{
    // layer mask for the pieces
    [SerializeField] private LayerMask _pieceLayer;
    [SerializeField] private LayerMask _previewLayer;

    private Piece _selectedPiece;

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitPreview, Mathf.Infinity, _previewLayer))
            {
                GameManager.Instance.physicalBoard.MovePiece(_selectedPiece,
                    GameManager.Instance.physicalBoard.GetCoordinatesInBoard(hitPreview.collider.gameObject));
            }
            else if (Physics.Raycast(ray, hitInfo: out RaycastHit hitPiece, Mathf.Infinity, _pieceLayer))
            {
                GameManager.Instance.physicalBoard.SelectPiece(hitPiece.collider.gameObject);
                _selectedPiece = GameManager.Instance.board.GetPiece(
                    GameManager.Instance.physicalBoard.GetCoordinatesInBoard(hitPiece.collider.gameObject));
            }
            else
            {
                GameManager.Instance.physicalBoard.SelectNone();
            }
        }
    }
}