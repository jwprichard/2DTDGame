using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.HelperFunctions;

public class Enemy : MonoBehaviour, IEnemy
{
    private SimpleTimer timer;
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }
    public float AttackSpeed { get; set; }
    public int Damage { get; set; }

    Transform Target;

    Transform AttackPoint;

    BuildingManager BuildingManager { get; set; }

    public void Initialize()
    {
        BuildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        Health = 100;
        Speed = 5;
        Damage = 20000;
        AttackSpeed = 20;
        AttackPoint = GetComponentInChildren<Transform>();
        //CreateTimer();
    }

    private void Update()
    {
        CheckStats();
        FindTarget();
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            HandleHit(collision.gameObject);
        }
    }

    private void CheckStats()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void CreateTimer()
    {
        //SimpleTimer.Callback callback = new(Action);
        timer = new(((float)Globals.AttackConstant/AttackSpeed) * 1000);
    }

    private void Move()
    {
        if (Target == null) { }
        else if (Vector2.Distance(transform.position, Target.position) < 0.5f)
        {
            Attack();
        }
        else
        {
            //Move to and look at the target
            transform.SetPositionAndRotation(Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime), HelperFunctions.LookAt(transform.position, Target.position));
        }
    }

    private void HandleHit(GameObject other)
    {
        Destroy(other);
        Health -= other.GetComponentInParent<IBuilding>().Damage;
    }

    private void FindTarget()
    {
        if (Target == null)
        {
            Target = BuildingManager.FindBuilding();
        }
    }

    private void Attack()
    {
        if (timer == null) CreateTimer();
        else if (timer.IsRunning) { }
        else
        {
            Collider2D hit = Physics2D.OverlapCircle(AttackPoint.position, 0.1f, 1<<6);
            if (hit != null)
            {
                hit.GetComponent<IBuilding>().TakeDamage(Damage);                
            }
            CreateTimer();
        }
    }

}
