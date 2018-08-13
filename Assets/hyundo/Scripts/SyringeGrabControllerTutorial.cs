using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeGrabControllerTutorial : MonoBehaviour {

	private Animator anim;
	public float speed = 1f;
	private bool isGrabbed = false;
	private bool push = false;
	private bool pull = false;
	private float push = 0f;
	private float pull = 0f;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Called once per frame when trigger
	private void OnTriggerStay(Collider other) {

		if (other.tag.Equals("rHand")) {
			// On the first frame we initialize the thyringe transform parent to the rhand transform
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)>0.1f && !isGrabbed) {
				isGrabbed = true;
				transform.parent = other.transform;
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
				push = true;
				push += speed * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * Time.deltaTime;
			}

			// but if button one is pressed it will pull
			else if (isGrabbed && OVRInput.Get(OVRInput.Button.One)) {
				anim.SetBool("pushing", false);
				anim.speed = speed * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
				pull = true;
				pull += speed * OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) * Time.deltaTime;;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals("rHand")) {
			isGrabbed = false;
		}
	}

	public bool GetIsGrabbed(){
		return isGrabbed;
	}

	public bool GetPush(){
		return push;
		if (push>2f) return true;
		else return false;
	}

	public bool GetPull() {
		return pull;
		if (pull>2f) return true;
		else return false;
	}

	public bool GetAnimDone(){
		return push && pull;
		return true;
	}
}
