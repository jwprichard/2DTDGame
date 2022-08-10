using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class Enemy : MonoBehaviour, IEnemy
{
    int IEnemy.Health
    {
        get { return _health; }
        set { _health = value; }
    }
    [SerializeField]
    int _health = 10;
    int IEnemy.MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    [SerializeField]
    int _maxHealth = 10;
    int IEnemy.Speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    [SerializeField]
    int _speed = 5;
    Vector2 Target = new (0,0);

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float step = _speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, Target, step);
    }

}
