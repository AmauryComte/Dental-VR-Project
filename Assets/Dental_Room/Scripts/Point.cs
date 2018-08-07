using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour {
	private bool isTouch = false;

	void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("syringe")) isTouch = true;
	}

	public bool GetIsTouch(){
		return isTouch;
	}
}
