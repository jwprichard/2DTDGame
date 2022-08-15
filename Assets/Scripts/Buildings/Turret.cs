using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using System.Timers;

public class Turret : MonoBehaviour, IBuilding
{
    private SimpleTimer timer;
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public int Range { get; set; }
    public int Damage { get; set; }
    public float ActionRate { get; set; }
    private Transform FirePoint;
    private Transform Target;

    public void Initialize()
    {
        MaxHealth = 100;
        Health = MaxHealth;
        Range = 10;
        ActionRate = 1;
        Damage = 50;
        CreateGameObjects();
        CreateTimer();
    }

    public void Update()
    {
        if (!timer.IsRunning)
        {
            CreateTimer();
            Fire();
        }
        SetTarget();
        CheckStats();
    }

    private void CreateGameObjects()
    {
        FirePoint = GetComponentInChildren<Transform>();
    }

    public void CreateTimer()
    {
        //SimpleTimer.Callback callback = new(Action);
        timer = new (ActionRate * 1000);
    }

    private void CheckStats()
    {
        if(Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Fire()
    {
        if (Target == null) { }
        else
        {
            GameObject go = Instantiate(Resources.Load<GameObject>("Buildings/Turret/Bullet"));
            go.transform.parent = transform;
            go.transform.position = FirePoint.position;
            go.GetComponent<IProjectile>().Initialize(10, new Vector2(Target.position.x, Target.position.y));
        }
    }

    private void SetTarget()
    {
        if (Target == null)
        {
            EnemyManager enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
            Tuple<bool, Transform> t = enemyManager.FindClosestEnemy(new Vector2(transform.position.x, transform.position.x));
            if (t.Item1 == true)
            {
                Target = t.Item2;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

}
