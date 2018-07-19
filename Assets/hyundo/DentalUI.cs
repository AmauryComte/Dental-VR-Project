using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DentalUI : MonoBehaviour {

    public Vector3 offset=Vector3.zero;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            transform.position = GameObject.FindGameObjectWithTag("lhand").transform.position;
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) < 0.7f && transform.parent != null)
        {
            transform.parent = null;
        }
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "lhand"&&OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)>0.7f)
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.7f&&transform.parent!=other.transform)
            {
                transform.parent = other.transform;
            }
        }
    }
}
