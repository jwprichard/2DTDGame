using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    private GameObject[,] mapArray;
    public void InitializeMap(int x, int y)
    {
        mapArray = new GameObject[x,y];

        for (int i = 0; i < mapArray.GetLength(0); i++)
        {
            for(int j = 0; j < mapArray.GetLength(1); j++)
            {
                GameObject go = new();
                go.AddComponent<Tile>().CreateTile(new(i,j));
                mapArray[i,j] = go;
            }
        }
    }
}
