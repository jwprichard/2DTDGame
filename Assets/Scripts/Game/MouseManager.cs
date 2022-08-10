using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    public BuildingManager buildingM;

    void Update()
    {
        CheckClick();
    }

    private void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 dir = new(0, 0, 11);
            //Debug.DrawRay(ray.origin, dir, Color.red, 10f);
            RaycastHit2D hitInfo = (Physics2D.Raycast(ray.origin, dir));
            if (hitInfo.collider != null)
            {
                buildingM.RegisterClick(hitInfo.collider.transform);
            }

        }
    }
}
