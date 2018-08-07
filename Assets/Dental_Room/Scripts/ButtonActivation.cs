using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivation : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("rHand")) GetComponent<Button>().OnSelect(null);

		if (tag.Equals("BackwardButton")) transform.GetChild(0).gameObject.SetActive(true);
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag.Equals("rHand")) {
			Invoke("onClickInvoke", 0.4f);
		}
	}

	private void onClickInvoke() {
		if (tag.Equals("BackwardButton")) transform.GetChild(0).gameObject.SetActive(false);
		GetComponent<Button>().onClick.Invoke();
	}
}
