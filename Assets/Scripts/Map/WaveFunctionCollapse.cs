using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class WaveFunctionCollapse
{
    public Dictionary<Vector2, List<Module>> ModuleDictionary;
    private Dictionary<Vector2, Module> Map = new();
    List<Module> ModuleList = new ();


    private int width;
    private int height;
    int numRemaining;
    private bool Collapsed = false;
    public bool ReBuild { get; private set; } = false;

    private Vector2 coord;
    private Module m;
    public int Seed { get; private set; }

    public Dictionary<Vector2, Module> Initialize(int width, int height , int seed)
    {
        this.width = width;
        this.height = height;
        Seed = seed;
        numRemaining = width * height;
        ModuleDictionary = new();
        ReBuild = false;

        if (Seed == 0)
        {
            System.Random r = new ();
            Seed = r.Next();
        }

        UnityEngine.Random.InitState(Seed);

        ModuleList = LoadModules();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                List<Module> mList = new(ModuleList);
                ModuleDictionary.Add(new(i, j), mList);
            }
        }

        while (!Collapsed)
        {
            if (ReBuild)
            {
                return null;
            }
            Iterate();
        }

        foreach (KeyValuePair<Vector2, List<Module>> pair in ModuleDictionary)
        {
            Map.Add(pair.Key, pair.Value[0]);
        }

        return Map;
    }

    public void Init(int width, int height)
    {
        this.width = width;
        this.height = height;
        numRemaining = width * height;

        ModuleList = LoadModules();
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                List<Module> mList = new(ModuleList);
                ModuleDictionary.Add(new(i, j), mList);
            }
        }
    }

    public Tuple<Vector2, Module> Build()
    {
        Iterate();

        return new Tuple<Vector2, Module>(coord, m);
    }

    private void Iterate()
    {
        List<Vector2> coords = GetMinEntropyCoords();
        if (numRemaining < 1)
        {
            Collapsed = true;
            return;
        }
        else
        {
            Propagate(Collapse(coords));
        }

    }

    private Vector2 Collapse(List<Vector2> coords)
    {
        Vector2 chosenCoord;
        int num;

        if (coords.Count == 1)
        {
            chosenCoord = coords[0];
        }
        else
        {
            num = UnityEngine.Random.Range(0, coords.Count - 1);
            chosenCoord = coords[num];
        }

        ModuleDictionary.TryGetValue(chosenCoord, out List<Module> modules);

        //Calculate chosen tile based on wieghts
        List<int> weightList = new();
        for (int i = 0; i < modules.Count; i++)
        {
            for (int j = 0; j < modules[i].Weight; j++)
            {
                weightList.Add(i);
            }
        }

        num = UnityEngine.Random.Range(0, weightList.Count - 1);
        if (num > weightList.Count)
        {
            Debug.LogError("Error: num is greater than weightlist");
        }
        if (weightList[num] > modules.Count)
        {
            Debug.LogError("Error: Given index does not exist in Module List");
        }
        Module chosenModule = modules[weightList[num]];

        coord = chosenCoord;
        m = chosenModule;

        modules.Clear();
        modules.Add(chosenModule);
        numRemaining -= 1;
        return chosenCoord;
    }

    private void Propagate(Vector2 collapsed)
    {
        List<Vector2> stack = new();
        stack.Add(collapsed);

        while (stack.Count > 0)
        {
            Vector2 coord = stack[0];
            stack.RemoveAt(0);

            for (int i = 0; i < 4; i++)
            {
                List<string> socketList = new();
                ModuleDictionary.TryGetValue(coord, out List<Module> modules);
                foreach (Module module in modules)
                {
                    socketList.Add(module.Sockets[i].SocketA);
                }

                Vector2 otherCoord = coord + GetDirection(i);

                if (ModuleDictionary.TryGetValue(otherCoord, out List<Module> otherModules))
                {
                    if (otherModules.Count > 1)
                    {
                        List<Module> newModuleList = new();

                        foreach (Module otherModule in otherModules)
                        {
                            if (socketList.Contains(otherModule.GetConnectingSocket(i)))
                            {
                                newModuleList.Add(otherModule);
                            }

                            if (modules.Count == 1)
                            {
                                foreach (string s in modules[0].Constraint_From)
                                {
                                    if (otherModule.TileName == s)
                                    {
                                        newModuleList.Remove(otherModule);
                                    }
                                }
                            }
                        }

                        if (newModuleList.Count < otherModules.Count)
                        {
                            if (newModuleList.Count < 1)
                            {
                                Debug.LogError("Error: Unsolvable");
                                ReBuild = true;
                                return;
                            }
                            if (newModuleList.Count == 1)
                            {
                                numRemaining -= 1;
                            }
                            if (!stack.Contains(otherCoord))
                            {
                                stack.Add(otherCoord);
                            }
                            ModuleDictionary[otherCoord] = newModuleList;
                        }
                    }
                }
            }
        }
    }

    private Vector2 GetDirection(int d)
    {
        switch (d)
        {
            case 0:
                return new Vector2(1, 0); //North
            case 1:
                return new Vector2(0, 1); //East
            case 2:
                return new Vector2(-1, 0); //South
            case 3:
                return new Vector2(0, -1); //West
            default:
                Debug.LogError("Error, direction not in any 2D space!.");
                return new Vector3(0, 0, 0);
        }
    }

    private List<Vector2> GetMinEntropyCoords()
    {
        List<Vector2> minEntropyList = new();
        float minEntropy = int.MaxValue;
        foreach (KeyValuePair<Vector2, List<Module>> entry in ModuleDictionary)
        {
            if (entry.Value.Count > minEntropy || entry.Value.Count == 1) { }
            else if (entry.Value.Count == minEntropy)
            {
                minEntropyList.Add(entry.Key);
            }
            else
            {
                minEntropyList.Clear();
                minEntropyList.Add(entry.Key);
                minEntropy = entry.Value.Count;
            }
        }
        if (minEntropy < 1)
        {
            Debug.LogError("Error: Min Entropy is < 1");
        }

        return minEntropyList;
    }

    private List<Module> LoadModules()
    {
        string jsonString = File.ReadAllText("D:\\Game Design\\2DTDGame\\Assets\\Resources\\Data\\Modules.json");
        ModuleList mList = JsonConvert.DeserializeObject<ModuleList>(jsonString);
        foreach (Module m in mList.Modules)
        {
            m.Initialize();
        }
        return mList.Modules;
    }
}
