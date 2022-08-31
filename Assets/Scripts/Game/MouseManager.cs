using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
            if (EventSystem.current.IsPointerOverGameObject())
            {
                Debug.Log("Clicked on the UI");
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 dir = new(0, 0, 11);
                //Debug.DrawRay(ray.origin, dir, Color.red, 10f);
                RaycastHit2D hitInfo = (Physics2D.Raycast(ray.origin, dir));
                //if (Input.mousePosition.x > 440 && Input.mousePosition.x < 710 && Input.mousePosition.y < 90) { }
                if (hitInfo.collider != null)
                {
                    Debug.Log(hitInfo.collider.gameObject.name);
                    buildingM.RegisterClick(hitInfo.collider.transform);
                }
            }

        }
    }
}
