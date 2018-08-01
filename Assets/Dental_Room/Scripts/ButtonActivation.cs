using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivation : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("rHand")) {
			GetComponent<Button>().OnSelect(null);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag.Equals("rHand")) {
			GetComponent<Button>().onClick.Invoke();
		}
	}
}
