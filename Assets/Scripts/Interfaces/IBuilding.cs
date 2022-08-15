using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    internal interface IBuilding
    {
        float Health { get; set; }
        float MaxHealth { get; set; }
        int Range { get; set; }
        int Damage { get; set; }
        float ActionRate { get; set; } 

        void Initialize();

        void TakeDamage(float damage);
    }
}
