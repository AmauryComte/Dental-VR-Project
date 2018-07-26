using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeController : MonoBehaviour {

	private Animator anim;
	public GameObject syringe_Silhouette;
	public Transform anchor;
	public float speed = 1f;
	private bool isGrabbed = false;
	private bool isInPlace = false;
	private Vector3 inversePosition;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}

	void Update() {
		if (isInPlace && isGrabbed) {
			inversePosition = transform.InverseTransformPoint(anchor.position);
			transform.Translate(0,0,inversePosition.z * Time.deltaTime,Space.Self);
		}
	}
	
	// Called once per frame when trigger
	private void OnTriggerStay(Collider other) {

		if (other.tag.Equals("rHand")) {
			// On the first frame we initialize the thyringe transform parent to the rhand transform
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)>0.1f && !isGrabbed) {
				isGrabbed = true;
				if (!isInPlace) transform.parent = other.transform;
			}
			
			// if we release the syringe grabb become false
			else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)==0.0f) {
				isGrabbed = false;
				transform.parent = null;
				anim.speed = 0;
			}

			// Then the syringe is grabbed and if button one not pressed, we activate push mode
			else if (isGrabbed && !OVRInput.Get(OVRInput.Button.One)) {
				anim.SetBool("pushing", true);
				anim.speed = speed * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
			}

			// but if button one is pressed it will pull
			else if (isGrabbed && OVRInput.Get(OVRInput.Button.One)) {
				anim.SetBool("pushing", false);
				anim.speed = speed * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
			}
		}
	}

	public bool GetIsGrabbed() {
		return isGrabbed;
	}
    
	public void SetIsInPlace(bool b) {
		isInPlace = b;
		transform.parent = null;
		transform.position = syringe_Silhouette.transform.position;
		transform.rotation = syringe_Silhouette.transform.rotation;
	}
}
