using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeGrabControllerTutorial : MonoBehaviour {

	private bool isGrabbed = false;

	private void OnTriggerStay(Collider other) {
		Debug.Log("ontriggerstay");

		if (other.tag.Equals("rHand")) {
			Debug.Log("rhand");
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)>0.0f && !isGrabbed) {
				transform.parent = other.transform;
				isGrabbed = true;

			}

			else if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)==0.0f && isGrabbed) {
				transform.parent = null;
				isGrabbed = false;
			}
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag.Equals("rHand")) {
			isGrabbed = false;
		}
	}
}
