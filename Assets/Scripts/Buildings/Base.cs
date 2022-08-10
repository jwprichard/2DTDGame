using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class Base : MonoBehaviour, IBuilding
{
    int IBuilding.Health { get; set; }
}
