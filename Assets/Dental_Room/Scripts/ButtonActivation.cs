using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonActivation : MonoBehaviour {

	public bool can_exit_trigger = true;

	private void OnTriggerEnter(Collider other) {
		if (other.tag.Equals("rIndex")) GetComponent<Button>().OnSelect(null);

		if (tag.Equals("BackwardButton")) transform.GetChild(0).gameObject.SetActive(true);
	}

	private void OnTriggerExit(Collider other) {
		if (other.tag.Equals("rIndex")) {
			if (can_exit_trigger) {
				can_exit_trigger = false;
				Invoke("onClickInvoke", 0.4f);
			}
		}
	}

	private void onClickInvoke() {
		if (tag.Equals("BackwardButton")) transform.GetChild(0).gameObject.SetActive(false);
		GetComponent<Button>().onClick.Invoke();
		can_exit_trigger = true;
	}
}
