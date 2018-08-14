using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutDownTeeth : MonoBehaviour {
    public GameObject teeth;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == teeth)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) < 0.7)
            {
                teeth.transform.parent = null;
                teeth.GetComponent<Collider>().isTrigger = false;
                teeth.GetComponent<Rigidbody>().isKinematic = false;
                teeth.GetComponent<Rigidbody>().useGravity = true;
                RemoveTeethManager.Instance.NextSequence();

                Destroy(GetComponent<PutDownTeeth>());
            }
        }
    }
}
