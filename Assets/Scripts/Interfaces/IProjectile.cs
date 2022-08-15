using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    interface IProjectile
    {
        float Speed { get; set; }
        Vector2 Target { get; set; }

        void Initialize(int speed, Vector2 target);
    }
}
