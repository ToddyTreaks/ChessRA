using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/*
 * Attributs : Prefab de preview, tableau de preview, liste de positionscontenant les positions des previews activé
 *
 * Initialise les previews blocs ( au start on fait 64 case et on les désactive, distance x : 1 & z : 1 /!\ On fait en localposition)
 *
 * Fonction qui pour une liste de position affiche les positions
 * Fonction qui clear le tableau de preview
 *
 */
public class PreviewBoard : MonoBehaviour
{
    [SerializeField] private GameObject previewBlock;
    [SerializeField] private Transform parent;
    
    public static PreviewBoard Instance;
    public  GameObject[,] PreviewBoards;
    
    private  List<GameObject> _previewBlocks;
    // Start is called before the first frame update
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

    void Start()
    {
        _previewBlocks = new List<GameObject>();
        PreviewBoards = new GameObject[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject preview = Instantiate(
                    original: previewBlock, 
                    position: new Vector3(i, 0, -j),
                    rotation: Quaternion.identity,
                    parent: parent);
                preview.SetActive(false);
                PreviewBoards[i, j] = preview;
            }
        }
        parent.transform.localScale = new Vector3((float)0.022, (float)0.022, (float)0.022);
    }

    public void ShowPos(List<Position> positions)
    {
        foreach (Position pos in positions)
        {
            PreviewBoards[pos.xIndex, pos.yIndex].SetActive(true);
            _previewBlocks.Add(PreviewBoards[pos.xIndex, pos.yIndex]);
        }
    }
    
    public void ClearPreview()
    {
        foreach (GameObject preview in _previewBlocks)
        {
            preview.SetActive(false);
        }
        _previewBlocks.Clear();
    }
    public Position WhatIsPosition(GameObject piece)
    {
        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                if (PreviewBoards[i, j] == piece)
                    return new Position(i, j);
            }
        }
        return new Position(-1, -1);
    }
}
