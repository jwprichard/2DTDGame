using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class Base : MonoBehaviour, IBuilding
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public int Range { get; set; }
    public int Damage { get; set; }
    public float ActionRate { get; set; }

    public void Initialize()
    {
        MaxHealth = 100;
        Health = MaxHealth;
    }

    void Update()
    {
        CheckStats();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision.gameObject);
    }

    private void HandleHit(GameObject other)
    {
        Destroy(other);
        Health -= other.GetComponentInParent<IEnemy>().Damage;
    }

    private void CheckStats()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
            GameManager.EndGame();
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
    }
}
