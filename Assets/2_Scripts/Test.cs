using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{   
    void Start()
    {
        if (!GameManager.Instance || !GameManager.Instance.isTestMode) return;

        Debug.Log(Application.dataPath);
    }
}
