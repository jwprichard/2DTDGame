using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public MapManager mapManager;

    // Called on the first frame of the game
    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        mapManager.InitializeMap(10, 10);
    }
}
