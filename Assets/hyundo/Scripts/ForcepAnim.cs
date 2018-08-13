using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcepAnim : MonoBehaviour {
    public float minAngle=30f;
    public float maxAngle = 0f;

    public GameObject baseObj;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(GetComponent<GrabTool>().grabbed)
        baseObj.transform.localRotation = Quaternion.Euler(0, OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) * (maxAngle - minAngle) + minAngle, 0);
	}
}
