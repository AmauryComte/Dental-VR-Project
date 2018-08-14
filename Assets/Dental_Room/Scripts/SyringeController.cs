using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SyringeController : MonoBehaviour {

	private Animator anim;
	public GameObject syringe_Silhouette;
	public Transform anchor;
	public float speed = 1f;
	private bool isGrabbed = false;
	private bool isInPlace = false;
	private bool finalPlace = false;
	private float animDone = 0f;

	private Vector3 inversePosition;
	private float zInitLocalPosition;
	private int noizeValue;
	private byte noizeByte;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update() {
		if (isInPlace && isGrabbed && !finalPlace) {
			inversePosition = transform.InverseTransformPoint(anchor.position);
			if ((!(transform.localPosition.z < zInitLocalPosition - 0.1) && !(transform.localPosition.z > zInitLocalPosition + 0.1)) || (inversePosition.z>0 && (transform.localPosition.z < zInitLocalPosition - 0.1)) || (inversePosition.z<0 && (transform.localPosition.z > zInitLocalPosition + 0.1))) {
				transform.Translate(0,0,inversePosition.z * Time.deltaTime,Space.Self);
			}
			if (transform.localPosition.z > zInitLocalPosition) {
				noizeValue = (int) ((transform.localPosition.z - zInitLocalPosition) * 2000);
				noizeByte = Convert.ToByte(noizeValue + 25);
				byte[] noize = { noizeByte };
				OVRHaptics.Channels[1].Preempt(new OVRHapticsClip(noize, 1));
			}
		}
	}
	
	// Called once per frame when trigger
	private void OnTriggerStay(Collider other) {

		if (other.tag.Equals("rHand")) {
			// On the first frame we initialize the thyringe transform parent to the rhand transform
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)>0.1f && !isGrabbed) {
				isGrabbed = true;
				if (!isInPlace && !finalPlace) transform.parent = other.transform;
			}
			
			// if we release the syringe grabb become false
			else if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)==0.0f) {
				isGrabbed = false;
				transform.parent = null;
				anim.speed = 0;
			}

			// Then the syringe is grabbed and if button one not pressed, we activate push mode
			else if (isGrabbed && !OVRInput.Get(OVRInput.Button.One)) {
				anim.SetBool("pushing", true);
				anim.speed = speed * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
				animDone += speed * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * Time.deltaTime;
			}

			// but if button one is pressed it will pull
			else if (isGrabbed && OVRInput.Get(OVRInput.Button.One)) {
				anim.SetBool("pushing", false);
				anim.speed = speed * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals("rHand")) {
			isGrabbed = false;
		}
	}

	public bool GetIsGrabbed() {
		return isGrabbed;
	}

	public bool GetAnimDone(){
		if (animDone>3.5f/speed) return true;
		else return false;
	}

	public void SetFinalPlace(bool b) {
		finalPlace = b;
	}
    
	public void SetIsInPlace(bool b) {
		isInPlace = b;
		transform.parent = null;
		transform.position = syringe_Silhouette.transform.position;
		transform.rotation = syringe_Silhouette.transform.rotation;
		zInitLocalPosition = transform.localPosition.z;
	}
}
