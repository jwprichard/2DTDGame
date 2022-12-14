using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public enum Globals
    {
        AttackConstant = 10,
    }
    [SerializeField]
    public enum Weights
    {
        Ground_M = 200,
        Mountain_M = 30,
        Mountain_C_1 = 2,
        Mountain_C_2 = 2,
        Mountain_S = 5,
        Pit_M = 10,
        Pit_C_1 = 2,
        Pit_C_2 = 2,
        Pit_S = 5,
    }
}
