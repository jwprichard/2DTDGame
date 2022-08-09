using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 Position { get; private set; }
    public void CreateTile(Vector2 position)
    {
        Position = position;
        gameObject.transform.position = position;
    }
}
