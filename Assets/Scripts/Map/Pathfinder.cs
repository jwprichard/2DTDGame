using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;


namespace Assets.Scripts.Map
{
    public class Node
    {
        private int x;
        private int y;

        public int gCost;
        public int hCost;
        public int fCost;

        public Node Parent;

        public Node(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Pathfinder
    {


        public Pathfinder(int width, int height)
        {

        }
    }
}
