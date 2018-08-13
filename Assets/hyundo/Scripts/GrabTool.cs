using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabTool : MonoBehaviour {
    public Vector3 initPos = Vector3.zero;
    public Vector3 initRot = Vector3.zero;
    public float grabForce = 0.5f;
    public bool isGrabbable = false;

    [HideInInspector]
    public bool grabbed
    {
        get { return _grabbed; }
        set
        {
            if (value == _grabbed)
            {
                return;
            }
            else
            {
                _grabbed = value;
                if (_grabbed)
                {
                    transform.parent = otherCollider.transform;
                    transform.localPosition = initPos;
                    transform.localRotation = Quaternion.Euler(initRot);
                    RemoveTeethManager.Instance.NextSequence();
                }
                else
                {
                    transform.parent = null;
                    if (RemoveTeethManager.Instance.currentSequence == Sequence.PutDownKnife)
                    {
                        RemoveTeethManager.Instance.NextSequence();
                    }
                    else
                    {
                        RemoveTeethManager.Instance.PreviousSequence();
                    }
                }
            }
        }
    }
    private Collider otherCollider;
    private bool _grabbed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "rHandGrap"&&isGrabbable)
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > grabForce)
            {
                otherCollider = other;
                grabbed = true;

            }
            else
            {
                grabbed = false;
            }
        }
    }

}
