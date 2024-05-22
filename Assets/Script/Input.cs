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
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (UnityEngine.Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(UnityEngine.Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hitPreview, Mathf.Infinity, _previewLayer))
            {
                GameManager.Instance.MovePiece(hitPreview.collider.gameObject);
            }
            else if (Physics.Raycast(ray, hitInfo: out RaycastHit hitPiece, Mathf.Infinity, _pieceLayer))
            {
                GameManager.Instance.SelectPiece(hitPreview.collider.gameObject);
            }
            else
            {
                GameManager.Instance.Nothing();
            }
        }
    }
}