﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour {
    
    bool initialized = false;
	// Use this for initialization
	void Start () {
        

    }
	
	// Update is called once per frame
	void Update () {
        if (!initialized &&OVRInput.GetDown(OVRInput.Button.Three)) 
        {
            initialized = true;
            Initialize();
        }
	}

    void Initialize()
    {
        GameObject rightIndexFinger = GameObject.Find("hands:b_r_index_ignore");
        SphereCollider rightIndexFingerCollider=rightIndexFinger.AddComponent<SphereCollider>();
        rightIndexFingerCollider.radius = 0.01f;
        rightIndexFinger.tag = "rHand";

    }
}
