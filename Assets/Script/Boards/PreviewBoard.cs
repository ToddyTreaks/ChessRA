using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewBoard : MonoBehaviour
{
    [SerializeField] private GameObject _previewBlock;
    
    public static PreviewBoard _instance;
    public static GameObject[,] _previewBoards;
    
    private List<GameObject> _previewBlocks;
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

    void Start()
    {
        _previewBlocks = new List<GameObject>();
        _previewBoards = new GameObject[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject preview = Instantiate(_previewBlock, new Vector3(i, 0, j), Quaternion.identity);
                preview.SetActive(false);
                _previewBoards[i, j] = preview;
            }
        }
    }

    public void ShowPos(List<Position> positions)
    {
        foreach (Position pos in positions)
        {
            
        }
    }
    
    public void ClearPreview()
    {
        foreach (GameObject preview in _previewBlocks)
        {
            preview.SetActive(false);
        }
    }
    /*
     * Attributs : Prefab de preview, tableau de preview, liste de positionscontenant les positions des previews activé
     * 
     * Initialise les previews blocs ( au start on fait 64 case et on les désactive, distance x : 1 & z : 1 /!\ On fait en localposition)
     * 
     * Fonction qui pour une liste de position affiche les positions
     * Fonction qui clear le tableau de preview
     * 
     */
}
