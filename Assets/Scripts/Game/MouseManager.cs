using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManager : MonoBehaviour
{
    void Update()
    {
        CheckClick();
    }

    private void CheckClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = Input.mousePosition;

        if (Input.GetMouseButtonDown(0))
        {
            Vector3 dir = new(0, 0, 11);
            Debug.DrawRay(ray.origin, dir, Color.red, 10f);
            if (Physics.Raycast(ray.origin, dir, out RaycastHit hit))
            {
                Debug.Log(hit.transform.name);
            }

        }
    }
}
