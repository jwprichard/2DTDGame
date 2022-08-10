using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;

public enum CurrentOperation
{
    Build,
    Other,
}

public enum Buildings
{
    Base,
    Turret,
}

public class BuildingManager : MonoBehaviour
{
    public CurrentOperation co;
    public Buildings currentBuilding;
    private bool baseBuilt = false;
    public void RegisterClick(Transform t)
    {
        if (co == CurrentOperation.Build)
        {
            BuildBuilding(t);
        }
    }

    private void BuildBuilding(Transform t)
    {
        if (currentBuilding == Buildings.Base && baseBuilt) Debug.LogException(new("Error: Base Building already built.")); // Cant Build Base if one already exists
        else if (t.childCount > 0) Debug.LogException(new("Error: Building already exists in current location.")); // Cant build on top of other building
        else // Build Building
        {
            Vector3 pos = new(t.position.x, t.position.y, 0.1f);
            GameObject go = Instantiate(Resources.Load<GameObject>("Buildings/" + currentBuilding));
            go.transform.position = pos;
            go.transform.parent = t;
            if (currentBuilding == Buildings.Base)
            {
                baseBuilt = true;
                currentBuilding = Buildings.Turret;
            }
        }
    }
}
