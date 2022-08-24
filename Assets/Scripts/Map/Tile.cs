using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.HelperFunctions;

public class WFCTile : MonoBehaviour
{
    public int TimesUpdated = 0;
    public List<Module> mList = new();
    public Vector2 Position { get; private set; }
    private SpriteRenderer spriteR;

    public void Initialize(Vector2 pos, List<Module> moduleList)
    {
        gameObject.name = pos.ToString();
        Position = pos;
        gameObject.transform.position = Position;
        mList = moduleList;
        CreateSR();
        UpdateColor();
    }

    public void CreateSR()
    {
        spriteR = gameObject.AddComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load<Sprite>("Sprites\\White");
        spriteR.sprite = sprite;
        spriteR.sortingLayerName = "Background";
    }

    public void UpdateMList(List<Module> mList)
    {
        TimesUpdated++;
        this.mList = mList;
        UpdateColor();
    }

    private void UpdateColor()
    {
        if (mList.Count < 3)
        {
            spriteR.color = Color.blue;
        }
        else if (mList.Count < 5)
        {
            spriteR.color = Color.green;
        }
        else if (mList.Count < 9)
        {
            spriteR.color = Color.yellow;
        }
        else if (mList.Count < 17)
        {
            spriteR.color = Color.red;
        }
        else
        {
            spriteR.color = Color.black;
        }
    }

}

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
        if (Module.SpriteName == "Pit_S" || Module.SpriteName == "Mountain_S")
        {
            Rotation += 90;
        }
        gameObject.transform.rotation = Quaternion.Euler(0, 0, Rotation);
        gameObject.AddComponent<BoxCollider2D>();
    }

    public void CreateSR()
    {
        SpriteRenderer spriteR = gameObject.AddComponent<SpriteRenderer>();
        sprites = Resources.LoadAll<Sprite>("Sprites\\Terrain\\" + Module.SpriteName);
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
