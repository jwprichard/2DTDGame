using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 Position { get; private set; }
    public Sprite sprite;

    public void CreateTile(Vector2 position, string name)
    {
        gameObject.name = name.ToLower();
        Position = position;
        gameObject.transform.position = position;
        CreateSR();
        gameObject.AddComponent<BoxCollider2D>();//.isTrigger = true;
    }

    public void CreateSR()
    {
        SpriteRenderer spriteR = gameObject.AddComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>("Sprites/BasicGround");
        spriteR.sprite = sprite;
        spriteR.sortingLayerName = "Background";
    }
}
