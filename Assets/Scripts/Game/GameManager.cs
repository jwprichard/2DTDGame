using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public MapManager mapManager;
    public BuildingManager buildingManager;
    public EnemyManager enemyManager;
    public int[] size;
    public static bool GameOver = false;

    // Called on the first frame of the game
    void Start()
    {
        //Initialize();
    }

    private void Initialize()
    {
        mapManager.InitializeMap(size[0], size[1]);
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
        if (GUI.Button(new(10, 120, 100, 100), "Spawn Enemy"))
        {
            enemyManager.CreateEnemy();
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
