using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

enum Enemies
{
    Enemy,
    Eye,
}

public class EnemyManager : MonoBehaviour
{
    private SimpleTimer timer;
    public bool SpawnEnemies { get; set; } = false;
    public void CreateEnemy(Vector2 pos)
    {
        Enemies[] e = (Enemies[])Enum.GetValues(typeof(Enemies));
        int r = UnityEngine.Random.Range(0, e.Length);
        GameObject go = Instantiate(Resources.Load<GameObject>("Enemies/" + e[r].ToString() + "/" + e[r].ToString()));
        go.GetComponent<IEnemy>().Initialize();
        go.transform.position = new(pos.x, pos.y);
        go.transform.parent = transform;
    }

    public void Update()
    {
        Spawn();
    }

    private void Spawn()
    {
        if (!SpawnEnemies) { }
        else if (timer == null) CreateTimer();
        else if (timer.IsRunning) { }
        else
        {
            CreateEnemy(new(UnityEngine.Random.Range(0, 20), UnityEngine.Random.Range(0, 20)));
            CreateTimer();
        }
    }
    public void CreateTimer()
    {
        //SimpleTimer.Callback callback = new(Action);
        timer = new(1000);
    }

}
