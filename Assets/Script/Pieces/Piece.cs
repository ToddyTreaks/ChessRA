using System.Collections.Generic;
using UnityEngine;

public enum Team
{
    WHITE,
    BLACK
}

public abstract class Piece : MonoBehaviour
{
    public Team team;
    public List<Vector2> direction;
    public GameObject prefabModel;
}