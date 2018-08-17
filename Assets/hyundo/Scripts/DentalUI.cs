using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DentalUI : MonoBehaviour {

    public Vector3 offset=Vector3.zero;
    private bool isGrabbed = false;
	// Use this for initialization
	void Start () {
        offset = new Vector3(0.08f, 0f, 0.07f);
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            transform.position = GameObject.FindGameObjectWithTag("lhand").transform.position-offset;
            transform.parent = GameObject.FindGameObjectWithTag("lhand").transform;
            transform.localRotation = Quaternion.Euler(130, 10, -140);
            transform.parent = null;
        }
        if(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) < 0.7f && transform.parent != null)
        {
            transform.parent = null;
            isGrabbed = false;
        }
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "lhand" && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)>0.7f)
        {
            if (transform.parent!=other.transform)
            {
                transform.parent = other.transform;
                isGrabbed = true;
            }
        }
    }

    public bool GetIsGrabbed(){
        return isGrabbed;
    }
}
