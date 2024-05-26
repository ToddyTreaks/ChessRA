using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Input : MonoBehaviour
{
    // layer mask for the pieces
    [SerializeField] private LayerMask _pieceLayer;
    [SerializeField] private LayerMask _previewLayer;
    [SerializeField] private Camera _camera;

    private Piece _selectedPiece;

    private void Update()
    {
        if (!GameManager.Instance.canClick) return;
        if (UnityEngine.Input.touchCount == 0) return;
        Vector3 touchPosition = UnityEngine.Input.GetTouch(0).position;
        Ray ray = _camera.ScreenPointToRay(touchPosition);
        if (Physics.Raycast(ray, out RaycastHit hitPreview, Mathf.Infinity, _previewLayer))
        {
            GameManager.Instance.MovePiece(hitPreview.collider.gameObject);
        }
        else if (Physics.Raycast(ray, hitInfo: out RaycastHit hitPiece, Mathf.Infinity, _pieceLayer))
        {
            GameManager.Instance.SelectPiece(hitPiece.collider.gameObject);
        }
        else
        {
            GameManager.Instance.Nothing();
        }
    }
}