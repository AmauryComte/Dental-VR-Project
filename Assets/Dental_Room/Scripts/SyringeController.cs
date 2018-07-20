using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeController : MonoBehaviour {

	private Animator anim;
	private bool isGrabbed = false;
	private bool isInPlace = false;
	public GameObject syringe_Silhouette;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Called once per frame when trigger
	private void OnTriggerStay(Collider other) {
		Debug.Log("on trigger stay");
		
		if (other.tag.Equals("rHand")) {
			// On the first frame we initialize the thyringe transform parent to the rhand transform
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)>0.3f && !isGrabbed && !isInPlace) {
				isGrabbed = true;
				transform.parent = other.transform;
				Debug.Log(isGrabbed);
			}

			// Then the syringe is grabbed and at Primaryindextrigger we set the anim
			else if (isGrabbed &&  OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)==1f) {
				Debug.Log("On ouvre");
				anim.SetBool("open", false);
				Debug.Log("On ouvre");
			}

			// Then we close the anim when release the index trigger
			else if (!anim.GetBool("open") && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger)==0f) {
				anim.SetBool ("open", true);
				Debug.Log("On ferme");
			}

			// When we release the syringe grabb become false
			else if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)==0f && isGrabbed && !isInPlace) {
				//if (!anim.GetBool("open")) anim.SetBool ("open", true);
				isGrabbed = false;
				Debug.Log(isGrabbed);
				transform.parent = null;
			}

			// if in place (near the silhouette) we fix the syringe at the silhouette place
			else if (isInPlace){
				transform.parent = null;
				//transorm.position = syringe_Silhouette.transform.position;
				transform.rotation = syringe_Silhouette.transform.rotation;
			}

		}
	}

	public bool GetIsGrabbed() {
		return isGrabbed;
	}
    
	public void SetIsInPlace(bool b) {
		isInPlace = b;
	}
}
