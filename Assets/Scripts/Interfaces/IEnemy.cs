using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interfaces
{
    internal interface IEnemy
    {
        public int Health { get; set; } 
        public int MaxHealth { get; set; }
        public int Speed { get; set; }
    }
}
