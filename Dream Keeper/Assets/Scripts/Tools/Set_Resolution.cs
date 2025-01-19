using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Set_Resolution : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Screen.fullScreen = false;
        Screen.SetResolution(1900, 1080, false);
    }
}
