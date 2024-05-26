using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Boards;
using UnityEngine;

public class PromoteTo : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    
    public void Click()
    {
        PhysicalBoard.Instance.PromoteTo(prefab);
    }
}
