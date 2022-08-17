using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public class WaveFunctionCollapse
{
    public Dictionary<Vector2, List<Module>> ModuleDictionary = new ();
    private Dictionary<Vector2, Module> Map = new();
    List<Module> ModuleList = new ();


    private int width;
    private int height;
    int numRemaining;
    private bool Collapsed = false;

    public Dictionary<Vector2, Module> Initialize(int width, int height)
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

        while (!Collapsed)
        {
            Iterate();
        }

        foreach (KeyValuePair<Vector2, List<Module>> pair in ModuleDictionary)
        {
            Map.Add(pair.Key, pair.Value[0]);
        }

        return Map;
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
        System.Random rnd = new();
        int num;

        if (coords.Count == 1)
        {
            chosenCoord = coords[0];
        }
        else
        {
            num = rnd.Next(coords.Count);
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

        num = rnd.Next(weightList.Count);
        if (num > weightList.Count)
        {
            Debug.LogError("Error: num is greater than weightlist");
        }
        if (weightList[num] > modules.Count)
        {
            Debug.LogError("Error: Given index does not exist in Module List");
        }
        Module chosenModule = ModuleList[weightList[num]];
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
                List<int> ValidNeighbours = new();
                ModuleDictionary.TryGetValue(coord, out List<Module> modules);
                foreach (Module module in modules)
                {
                    for (int k = 0; k < module.ValidNeighbours.Length; k++)
                    {
                        ValidNeighbours.Add(module.ValidNeighbours[k]);
                    }
                }

                Vector2 otherCoord = coord + GetDirection(i);

                if (ModuleDictionary.TryGetValue(otherCoord, out List<Module> otherModules)){
                    if (otherModules.Count > 1)
                    {
                        List<Module> newModuleList = new List<Module>();

                        foreach (Module otherModule in otherModules)
                        {
                            if (ValidNeighbours.Contains(otherModule.ID))
                            {
                                newModuleList.Add(otherModule);
                            }
                        }

                        if (newModuleList.Count < otherModules.Count)
                        {
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
                return new Vector2(-1, 0); //South
            case 2:
                return new Vector2(0, 1); //Easy
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
        return mList.Modules;
    }
}
