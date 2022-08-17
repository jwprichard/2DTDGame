using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class ModuleList
{
    public List<Module> Modules { get; set;}
}

[Serializable]
public class Module
{
    public string TileName;
    //string TileName;
    public int Weight;
    public int ID;
    public int[] ValidNeighbours;
    //public string[] sockets = new string[4];

    //public Module(string name)
    //{
    //    TileName = name;
    //}
}
