using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public Vector2 Position { get; private set; }
    public Sprite sprite;

    public void Initialize(Vector2 position, string name)
    {
        gameObject.name = name.ToLower();
        Position = position;
        gameObject.transform.position = position;
        CreateSR(name);
        gameObject.AddComponent<BoxCollider2D>();//.isTrigger = true;
    }

    public void CreateSR(string name)
    {
        SpriteRenderer spriteR = gameObject.AddComponent<SpriteRenderer>();
        sprite = Resources.Load<Sprite>("Sprites/" + name);
        spriteR.sprite = sprite;
        spriteR.sortingLayerName = "Background";
    }
}
