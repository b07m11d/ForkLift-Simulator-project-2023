﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControlVRMode : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("off VR");
            XRSettings.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log("on VR");

            XRSettings.enabled = true;
        }
    }
}
