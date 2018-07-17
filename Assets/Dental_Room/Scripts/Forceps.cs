﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forceps : MonoBehaviour {
    [SerializeField]
    protected OVRInput.Controller m_controller;
    private Animator anim;

	private bool isGrabbed = false;
	private bool isInPlace = false;
	public GameObject syringe_Silhouette;
    public bool traceHand = true;

    public GameObject[] points;
    int pointNum = 0;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	}

	// Called once per frame when trigger
    private void OnTriggerStay(Collider other)
    {
		if (other.tag.Equals ("rHand")) {
            Debug.Log(isGrabbed);
            Debug.Log(isInPlace);
            Debug.Log(OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger, m_controller));
			// On the first frame we initialize the thyringe transform parent to the rhand transform
			if (OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger)>0.0f && !isGrabbed && !isInPlace) {
				isGrabbed = true;
                Debug.Log(isGrabbed);
				transform.parent = other.transform;
			}

			// Then the syringe is grabbed and at Primaryindextrigger we set the anim
			else if (isGrabbed &&  OVRInput.Get (OVRInput.Axis1D.SecondaryIndexTrigger, m_controller)==1.0f) {
					anim.SetBool ("open", false);
			}

			// Then we close the anim when release the index trigger
			else if (!anim.GetBool("open") && OVRInput.Get (OVRInput.Axis1D.SecondaryIndexTrigger, m_controller)==0f) {
					anim.SetBool ("open", true);
			}

			// When we release the syringe grabb become false
			else if (OVRInput.Get (OVRInput.Axis1D.SecondaryHandTrigger, m_controller)==1.0f && isGrabbed && !isInPlace) {
				if (!anim.GetBool("open")) anim.SetBool ("open", true);
				isGrabbed = false;
				transform.parent = null;
			}

			// if in place (near the silhouette) we fix the syringe at the silhouette place
			else if (isInPlace){
				transform.parent = null;
				//transorm.position = syringe_Silhouette.transform.position;
				transform.rotation = syringe_Silhouette.transform.rotation;
			}

		}

        /*if (points.Length>0) {
            if (other.gameObject.Equals(points[pointNum])){
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, m_controller))
                {
                    points[pointNum].SetActive(false);
                    if (points.Length - 1 > pointNum)
                    {
                        points[pointNum + 1].SetActive(true);
                        pointNum++;
                    }
                    

                }
            }
        }
        if (other.tag.Equals("rHand"))
        {
            if (OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, m_controller)&& (OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, m_controller) || OVRInput.Get(OVRInput.Touch.PrimaryThumbstick, m_controller) || OVRInput.Get(OVRInput.Touch.Two, m_controller) || OVRInput.Get(OVRInput.Touch.One, m_controller)) && traceHand)
            {
                if (other.transform.childCount == 0)
                {
                    transform.position = other.transform.position;
                    transform.rotation = other.transform.rotation;
                    transform.parent = other.transform;
                }
            }
            else
            {
                transform.parent = null;
            }
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, m_controller))
            {
                anim.SetBool("open", false);
            }
            else
            {
                anim.SetBool("open", true);
            }
        }*/
    }

	public bool GetIsGrabbed() {
		return isGrabbed;
	}
    
	public void SetIsInPlace(bool b) {
		isInPlace = b;
	}

}
