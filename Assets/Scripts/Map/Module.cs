using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Interfaces;

[Serializable]
public class ModuleList
{
    public List<Module> Modules { get; set; }
}

public class Socket
{
    string P1 { get; set; }
    string P2 { get; set; }
    public string SocketA // The original position of points
    {
        get { return P1 + P2; }
    }
    public string SocketB // An inverted point position for testing socket connections
    {
        get { return P2 + P1; }
    }

    public Socket(string p1, string p2)
    {
        P1 = p1;
        P2 = p2;
    }
}

[Serializable]
public class Module
{
    public string TileName;
    public string SpriteName;
    public float Rotation;
    public int Weight;
    public string[] Constraint_To;
    public string[] Constraint_From;
    public Socket[] Sockets = new Socket[4];
    private Texture2D texture;

    public void Initialize()
    {

        Weights weight = (Weights)Enum.Parse(typeof(Weights), SpriteName);
        Weight = (int)weight;
        TileName = SpriteName + "_" + Rotation;
        CreateSockets();
    }

    public void CreateSockets()
    {
        texture = Resources.LoadAll<Texture2D>("Sprites\\Terrain\\" + SpriteName)[0];
        string p1 = ColorUtility.ToHtmlStringRGBA(texture.GetPixel(0, texture.height));
        string p2 = ColorUtility.ToHtmlStringRGBA(texture.GetPixel(texture.width, texture.height));
        string p3 = ColorUtility.ToHtmlStringRGBA(texture.GetPixel(texture.width, 0));
        string p4 = ColorUtility.ToHtmlStringRGBA(texture.GetPixel(0, 0));

        Socket[] tempSockets = new Socket[4];
        tempSockets[0] = new Socket(p1, p2);
        tempSockets[1] = new Socket(p2, p3);
        tempSockets[2] = new Socket(p3, p4);
        tempSockets[3] = new Socket(p4, p1);

        for (int i = 0; i < Sockets.Length; i++)
        {
            Sockets[i] = tempSockets[Rotate(i)];
        }
    }

    public int Rotate(int current)
    {
        int r = Rotation switch
        {
            0 => current,
            90 => current - 1,
            180 => current - 2,
            270 => current - 3,
            _ => current
        };
        if (r < 0) return r += 4;
        else return r;
    }

    public string GetConnectingSocket(int i)
    {
        return i switch
        {
            0 => Sockets[2].SocketB, //nX
            1 => Sockets[3].SocketB, //pX
            2 => Sockets[0].SocketB, //nY
            3 => Sockets[1].SocketB, //pY
            _ => Sockets[i].SocketB
        };
    }
}
