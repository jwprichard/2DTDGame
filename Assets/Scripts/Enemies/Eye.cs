using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.HelperFunctions;

public class Eye : MonoBehaviour, IEnemy
{
    private SimpleTimer timer;
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    public float Speed { get; set; }
    public float AttackSpeed { get; set; }
    public int Damage { get; set; }
    public float Range { get; set; }

    Transform Target;

    Transform AttackPoint;

    BuildingManager BuildingManager { get; set; }

    public void Initialize()
    {
        BuildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        Health = 100;
        Speed = 5;
        Damage = 50;
        AttackSpeed = 20;
        Range = 2.5f;
        AttackPoint = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        CheckStats();
        FindTarget();
        Move();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("BuildingProjectile"))
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
        timer = new((float)Globals.AttackConstant / AttackSpeed * 1000);
    }

    private void Move()
    {
        if (Target == null) { }
        else if (Vector2.Distance(transform.position, Target.position) < Range)
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
            Target = BuildingManager.FindClosestBuilding(transform.position);
        }
    }

    private void Attack()
    {
        if (timer == null) CreateTimer();
        else if (timer.IsRunning) { }
        else
        {
            Collider2D hit = Physics2D.OverlapCircle(AttackPoint.position, Range, 1 << 6);
            if (hit != null)
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Enemies/Eye/Projectile"));
                go.transform.parent = transform;
                go.transform.position = AttackPoint.position;
                go.GetComponent<IProjectile>().Initialize(10, new Vector2(Target.position.x, Target.position.y));
            }
            CreateTimer();
        }
    }

}
