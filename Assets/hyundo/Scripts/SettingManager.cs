using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingManager : MonoBehaviour {
    
    bool initialized = false;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        GameObject obj = GameObject.Find("hands:b_r_index_ignore");
        if (!initialized && obj!=null) 
        {
            initialized = true;
            Initialize();
        }
	}

    void Initialize()
    {
        GameObject rightIndexFinger = GameObject.Find("hands:b_r_index_ignore");
        Rigidbody rightIndexFingerRB = rightIndexFinger.AddComponent<Rigidbody>();
        rightIndexFingerRB.isKinematic = true;
        SphereCollider rightIndexFingerCollider=rightIndexFinger.AddComponent<SphereCollider>();
        rightIndexFingerCollider.radius = 0.01f;
        rightIndexFingerCollider.isTrigger = true;
        rightIndexFinger.tag = "rHand";

        GameObject rightGrip = GameObject.Find("hands:b_r_grip");
        Rigidbody rightGripRB = rightGrip.AddComponent<Rigidbody>();
        rightGripRB.isKinematic = true;

        SphereCollider rightGripCollider = rightGrip.AddComponent<SphereCollider>();
        rightGripCollider.radius = 0.05f;
        rightGripCollider.isTrigger = true;
        rightGrip.tag = "rHandGrap";

        GameObject rightGripFix = GameObject.Find("hands:b_r_hand");
        Rigidbody rightGripFixRB = rightGripFix.AddComponent<Rigidbody>();
        rightGripFixRB.isKinematic = true;
        SphereCollider rightGripFixCollider = rightGripFix.AddComponent<SphereCollider>();
        rightGripFixCollider.radius = 0.05f;
        rightGripFixCollider.center = new Vector3(-0.08f, 0.01f, -0.02f);
        rightGripFixCollider.isTrigger = true;
        rightGripFix.tag = "rHandGrapFix";
    }
}
