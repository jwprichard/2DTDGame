using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Dictionary<Vector2, Module> Map;
    WaveFunctionCollapse wfc;
    public void InitializeMap(int x, int y)
    {
        wfc = new ();
        Map = wfc.Initialize(x, y);

        foreach(KeyValuePair<Vector2, Module> entry in Map)
        {
            GameObject tile = new();
            tile.AddComponent<Tile>().Initialize(entry.Key, entry.Value);
            tile.transform.parent = transform;
        }
    }

    public void CreateTile(int x, int y)
    {
        if (wfc == null)
        {
            wfc= new WaveFunctionCollapse();
            wfc.Init(x, y);
        }

        Tuple<Vector2, Module> t = wfc.Build();

        GameObject tile = new();
        tile.AddComponent<Tile>().Initialize(t.Item1, t.Item2);
        tile.transform.parent = transform;
    }
}
