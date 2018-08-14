using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Options : MonoBehaviour {

	public GameObject syringe;
	public GameObject joint_2;

	public void SyringeBehaviour() {
		if (syringe.GetComponent<SyringeController>().GetIsInPlace()) {
			syringe.transform.parent = joint_2.transform;
		}
	}

	public void SyringeBehaviourToNormal(){
		if (syringe.GetComponent<SyringeController>().GetIsInPlace()) {
			syringe.transform.parent = null;
		}
	}
}
