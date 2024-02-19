using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Camera mainCamera;

    void Awake()
    {
        mainCamera = GetComponent<Camera>();
        mainCamera.orthographicSize = (int)((float)Screen.height / (float)Screen.width * 10);
    }
}
