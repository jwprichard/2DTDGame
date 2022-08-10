using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public MapManager mapManager;
    [SerializeField]
    public int[] size;

    // Called on the first frame of the game
    void Start()
    {
        //Initialize();
    }

    private void Initialize()
    {
        mapManager.InitializeMap(size[0], size[1]);
    }

    private void OnGUI()
    {
        if (GUI.Button(new(10, 10, 100, 100), "Create Map"))
        {
            foreach (Transform child in mapManager.transform)
            {
                Destroy(child.gameObject);
            }
            Initialize();
        }
    }
}