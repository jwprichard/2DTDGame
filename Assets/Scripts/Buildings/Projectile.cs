using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.HelperFunctions;

public class Projectile : MonoBehaviour, IProjectile
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("BuildingProjectile"))
        {
            if (collision.CompareTag("Enemy"))
            {
                Destroy(gameObject);
            }
        }
        else if (gameObject.CompareTag("EnemyProjectile"))
        {
            if (collision.CompareTag("Building"))
            {
                Destroy(gameObject);
            }
        }
    }

    private void Move()
    {
        float step = Speed * Time.deltaTime;
        transform.SetPositionAndRotation(Vector2.MoveTowards(transform.position, Target, step), HelperFunctions.LookAt(transform.position, Target));
        if (Vector2.Distance(Target, transform.position) < 0.001f)
        {
            Destroy(gameObject);
        }
    }
}
