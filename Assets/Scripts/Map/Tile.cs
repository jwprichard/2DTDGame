using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.HelperFunctions;

public class Tile : MonoBehaviour
{
    public Vector2 Position { get; private set; }
    public Sprite[] sprites;
    public float Rotation;
    public Module Module { get; private set; }

    public void Initialize(Vector2 position, Module module)
    {
        Module = module;
        gameObject.name = Module.TileName.ToLower();
        Position = position;
        gameObject.transform.position = position;
        CreateSR();
        Rotation = module.Rotation;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, module.Rotation);
        gameObject.AddComponent<BoxCollider2D>();
    }

    public void CreateSR()
    {
        SpriteRenderer spriteR = gameObject.AddComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites\\Terrain\\" + Module.TileName);
        int rnd = HelperFunctions.RandomNumber(0, sprites.Length);
        spriteR.sprite = sprites[rnd];
        spriteR.sortingLayerName = "Background";
    }

    //public void Rotate()
    //{
    //    if (Module.Rotation != "C" || Module.Rotation != "0")
    //    {
    //        if (Module.Rotation == "1")
    //        {
    //            gameObject.transform.rotation = Quaternion.Euler(0, 0, 90);
    //        }
    //        else if (Module.Rotation == "2")
    //        {
    //            gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
    //        }
    //        else if (Module.Rotation == "3")
    //        {
    //            gameObject.transform.rotation = Quaternion.Euler(0, 0, 270);
    //        }
    //    }
    //}
}
