using System;
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
        GameObject go = Instantiate(Resources.Load<GameObject>("Enemies/Enemy"));
        go.GetComponent<IEnemy>().Initialize();
        go.transform.position = new(6, 8, 0);
        go.transform.parent = transform;
    }

    public Tuple<bool, Transform> FindClosestEnemy(Vector2 pos)
    {
        Transform Enemy = null;
        float distance = int.MaxValue;
        foreach (Transform child in transform)
        {
            Vector2 childPos = new(child.position.x, child.position.y);
            float newDistance = Vector2.Distance(childPos, pos);
            if (newDistance < distance)
            {
                distance = newDistance;
                Enemy = child;
            }
        }

        if (Enemy != null) return new Tuple<bool, Transform>(true, Enemy);
        else return new Tuple<bool, Transform>(false, Enemy);
    }
}
