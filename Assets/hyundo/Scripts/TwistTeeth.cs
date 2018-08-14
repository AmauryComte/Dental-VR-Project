using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwistTeeth : MonoBehaviour {

    public Transform forcep;

    public Transform originParent;

    private Vector3 originRot;

    public float angleMax = 5;
    public float angleIncrease = 3;
    
    private bool isTwist = false;
    private bool grabbed = false;

    //haptic
    OVRHapticsClip clip;
    // Use this for initialization
    void Start()
    {
        clip = new OVRHapticsClip(1);
        clip.WriteSample(255);
    }
    private void Update()
    {
        if (RemoveTeethManager.Instance.remainTwist<=0)
        {
            RemoveTeethManager.Instance.currentSequence = Sequence.ExtractTeeth;
            forcep.parent = GameObject.Find("hands:b_r_hand").transform;
            transform.parent = forcep;

            forcep.localPosition = forcep.GetComponent<GrabTool>().initPos;
            forcep.localRotation = Quaternion.Euler(forcep.GetComponent<GrabTool>().initRot);
            forcep.GetComponent<GrabTool>().confirmSequenceChange = false;
            //Destroy(forcep.GetComponent<GrabTool>());
            GetComponent<TwistTeeth>().enabled = false;
        }

        if (grabbed)
        {
            transform.LookAt(originParent);

            float angle = Vector3.Angle(originParent.position - transform.position, originRot);
            if (angle > angleMax / 2)
            {
                if (angle > angleMax)
                {
                    OVRHaptics.RightChannel.Queue(clip);

                }
                if (!isTwist)
                {
                    isTwist = true;
                }
            }
            else
            {
                if (isTwist)
                {
                    isTwist = false;
                    RemoveTeethManager.Instance.remainTwist--;
                    RemoveTeethManager.Instance.currentSequence = Sequence.TwistTeeth;
                    //angle setting
                    angleMax += angleIncrease;

                    ////sound, success, feedback
                }
            }
        }

    }



    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ForcepGrab")
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.95)
            {
                if (RemoveTeethManager.Instance.currentSequence == Sequence.GrabTeeth)
                {
                    RemoveTeethManager.Instance.currentSequence = Sequence.TwistTeeth;
                }
                if (forcep.parent != transform)
                {
                    originParent = GameObject.Find("hands:b_r_grip").transform;
                    Transform child=transform.GetChild(0);
                    child.parent = null;
                    transform.LookAt(originParent);
                    child.parent = transform;
                    
                    forcep.parent = transform;
                    originRot = originParent.position-transform.position;
                    grabbed = true;
                }
               
                
                


                
            }
            else
            {
                if (RemoveTeethManager.Instance.currentSequence == Sequence.TwistTeeth)
                {
                    RemoveTeethManager.Instance.currentSequence = Sequence.GrabTeeth;
                }
                if (forcep.parent==transform)
                    forcep.parent = null;
            }
        }
    }
    /*
    //public Vector3 posOffset = new Vector3(0.001f, 0.001f, 0.001f);
    public float errorOffset = 0.003f;
    public Vector3 rotOffset = Vector3.zero;
    [HideInInspector]
    public int tiltNumMax=6;

    private int tiltNum = 0;

    //pos error
    private Vector3 originPos;
    public bool error = false;


    //collider
    private Transform parent;
    private bool grabbed=false;
    private Collider forcep;

    


  


    
   
	
	// Update is called once per frame
	void FixedUpdate () {
        
        if (!error)
        {
            if (Vector3.Distance(transform.position, originPos) > errorOffset / 2)
            {
                OVRHaptics.RightChannel.Queue(clip);
                if (Vector3.Distance(transform.position, originPos) > errorOffset)
                {
                    error = true;
                }
            }
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.95 && grabbed)
            {
                transform.parent = forcep.transform;
            }
            else
            {
                transform.parent = parent;
            }
        }
        else
        {
            transform.position = originPos;
            transform.parent = parent;
        }
        
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ForcepGrab")
        {
            grabbed = true;
            forcep = other;
        }
        else
        {
            grabbed = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ForcepGrab")
        {
            error = false;
        }
    }
    public void SetOriginPos()
    {
        originPos = transform.position;
        error = false;
    }
    */
}
