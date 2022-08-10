using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

enum Enemies
{
    Enemy
}

public class EnemyManager : MonoBehaviour
{
    public void CreateEnemy()
    {
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("Enemies/Enemy"));
        gameObject.transform.position = new(6, 8, 0);
        gameObject.transform.parent = transform;
    }
}
