using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public Dictionary<Vector2, Module> Map;
    public void InitializeMap(int x, int y)
    {
        WaveFunctionCollapse wfc = new ();
        Map = wfc.Initialize(x, y);

        foreach(KeyValuePair<Vector2, Module> entry in Map)
        {
            GameObject tile = new();
            tile.AddComponent<Tile>().Initialize(entry.Key, entry.Value.TileName);
            tile.transform.parent = transform;
            //mapArray[i,j] = tile;
        }

        //mapArray = new GameObject[x,y];

        //for (int i = 0; i < mapArray.GetLength(0); i++)
        //{
        //    for(int j = 0; j < mapArray.GetLength(1); j++)
        //    {
        //        GameObject tile = new();
        //        tile.AddComponent<Tile>().Initialize(new(i,j), "Ground");
        //        tile.transform.parent = transform;
        //        //mapArray[i,j] = tile;
        //    }
        //}
    }
}
