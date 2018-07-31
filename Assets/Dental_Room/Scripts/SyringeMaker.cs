using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeMaker : MonoBehaviour {

	private float distanceX;
	private float distanceY;
	private float distanceZ;
	private float eulerAngleX;
	private float eulerAngleY;
	private float eulerAngleZ;
	public Transform objectToPlace;

	private bool isInPlace = false;
	private bool isGrabbed = false;

	void Update() {
		if (close(objectToPlace)){
			isInPlace = true;
		}
	}
	private void OnTriggerStay(Collider other) {

		if (other.tag.Equals("rHand")) {
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)>0.0f && !isGrabbed) {
				transform.parent = other.transform;
				isGrabbed = true;
			}

			else if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)==0.0f && isGrabbed) {
				transform.parent = null;
				isGrabbed = false;
				Debug.Log(isGrabbed);
			}
		}
	}

	bool close(Transform objTransform) {
		distanceX = Mathf.Abs(transform.position.x - objTransform.position.x);
		distanceY = Mathf.Abs(transform.position.y - objTransform.position.y);
		distanceZ = Mathf.Abs(transform.position.z - objTransform.position.z);
		eulerAngleX = Mathf.Abs(transform.rotation.eulerAngles.x - objTransform.rotation.eulerAngles.x);
		eulerAngleY = Mathf.Abs(transform.rotation.eulerAngles.y - objTransform.rotation.eulerAngles.y);
		if (distanceX < 0.005 && distanceY < 0.005 && distanceZ < 0.005) {
			return true;
		}
		else return false;
	}

	public bool GetIsInPlace() {
		return isInPlace;
	}
}
