using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Utils;

public class GameManager : MonoBehaviour
{

    public MapManager mapManager;
    public BuildingManager buildingManager;
    public EnemyManager enemyManager;
    public int Seed;
    public int[] size;
    public static bool GameOver = false;

    // Called on the first frame of the game
    void Start()
    {
        //Initialize();
        mapManager.BuildMap(size[0], size[1]);
    }

    private void Initialize()
    {
        mapManager.InitializeMap(size[0], size[1], Seed);
    }

    public static void EndGame()
    {
        GameOver = true;
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
        if (GUI.Button(new(10, 120, 100, 100), "Build 1"))
        {
            mapManager.BuildSequentially(size[0], size[1]);
        }
        if (GUI.Button(new(10, 230, 100, 100), "Spawn Enemy"))
        {
            //enemyManager.CreateEnemy(new(2, 2));
            enemyManager.SpawnEnemies = true;
        }
        if (GameOver)
        {
            if (GUI.Button(new(500, 500, 300, 100), "Game Over, Restart?"))
            {
                //Restart Game
                SceneManager.LoadScene("Game");
                GameOver = false;
            }
        }
    }
}
