using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum Team
{
    WHITE,
    BLACK
}

public abstract class Piece : MonoBehaviour
{
    public Team team;
    protected List<Vector2> direction {get; set;}
    public GameObject prefabModel;
}