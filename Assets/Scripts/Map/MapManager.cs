using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Dictionary<Vector2, Module> Map;
    WaveFunctionCollapse wfc;
    public void InitializeMap(int x, int y, int seed)
    {
        wfc = new ();
        Map = wfc.Initialize(x, y, seed);

        int rebuilt = 0;

        while (wfc.ReBuild)
        {
            rebuilt++;
            if (rebuilt == 6) break;
            Map = wfc.Initialize(x, y, seed++);
        }

        Debug.Log(rebuilt);

        foreach(KeyValuePair<Vector2, Module> entry in Map)
        {
            GameObject tile = new();
            tile.AddComponent<Tile>().Initialize(entry.Key, entry.Value);
            tile.transform.parent = transform;
        }
    }

    public void BuildSequentially(int x, int y)
    {
        if (wfc == null)
        {
            wfc= new WaveFunctionCollapse();
            wfc.Init(x, y);
        }

        foreach (KeyValuePair<Vector2, List<Module>> pair in wfc.ModuleDictionary)
        {
            Transform wfcTile = transform.Find(pair.Key.ToString());
            if (wfcTile != null)
            {
                if (wfcTile.TryGetComponent(out WFCTile anotherTile))
                {
                    anotherTile.UpdateMList(pair.Value);
                }
            }
            else
            {
                GameObject wTile = new();
                wTile.AddComponent<WFCTile>().Initialize(pair.Key, pair.Value);
                wTile.transform.parent = transform;
            }
        }

        Tuple<Vector2, Module> t = wfc.Build();

        Transform cTile = transform.Find(t.Item1.ToString());
        Destroy(cTile.gameObject);

        GameObject tile = new();
        tile.AddComponent<Tile>().Initialize(t.Item1, t.Item2);
        tile.transform.parent = transform;
        tile.name = t.Item1.ToString();
    }
}
