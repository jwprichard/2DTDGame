using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class Bullet : MonoBehaviour, IProjectile
{
    public float Speed { get; set; }

    public Vector2 Target { get; set; }
    public void Initialize(int speed, Vector2 target)
    {
        Speed = speed;
        Target = target;
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Target, step);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("here");
    }
}
