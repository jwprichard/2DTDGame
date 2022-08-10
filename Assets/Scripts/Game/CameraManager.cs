using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    public float dragSpeed = 0.25f;
    [SerializeField]
    public float scrollSpeed = 0.25f;

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        if (Input.GetMouseButton(2))
        {
            if (x != 0)
            {
                transform.position += -(transform.right * (x * dragSpeed));
            }

            if (y != 0)
            {
                transform.position += -(transform.up * (y * dragSpeed));
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            transform.GetComponent<Camera>().orthographicSize -= scrollSpeed;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            transform.GetComponent<Camera>().orthographicSize += scrollSpeed;
        }
    }
}
