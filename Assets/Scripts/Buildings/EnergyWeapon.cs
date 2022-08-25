using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.HelperFunctions;

public class EnergyWeapon : MonoBehaviour, IBuilding
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
        AssignObjects();
    }

    public void Update()
    {
        Fire();
        LookAt();
        CheckStats();
    }

    private void AssignObjects()
    {
        FirePoint = GetComponentInChildren<Transform>();
    }

    public void CreateTimer()
    {
        timer = new(ActionRate * 1000);
    }

    private void CheckStats()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void LookAt()
    {
        if (Target == null) { }
        else
        {
            transform.rotation = HelperFunctions.LookAt(transform.position, Target.position);
        }
    }

    private void Fire()
    {
        if (timer == null) CreateTimer();
        else if (timer.IsRunning) { }
        else
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, Range, 1 << 7);
            if (hits.Length > 0)
            {
                float distance = Vector2.Distance(new(int.MaxValue, int.MaxValue), transform.position);
                foreach (Collider2D hit in hits)
                {
                    if (Vector2.Distance(hit.transform.position, transform.position) < distance)
                    {
                        Target = hit.transform;
                    }
                }
                GameObject go = Instantiate(Resources.Load<GameObject>("Buildings/EnergyWeapon/Projectile"));
                go.transform.parent = transform;
                go.transform.position = FirePoint.position;
                go.GetComponent<IProjectile>().Initialize(10, new Vector2(Target.position.x, Target.position.y));
            }
            CreateTimer();
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }

}
