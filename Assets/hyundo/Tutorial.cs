using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour {

	private int step = 0;
	private bool CR_Running = false;
	private bool isActive = false;

	public GameObject avatar;
	public GameObject nextButton;

	public GameObject tutorial_Text;
	public GameObject buttonA;
	public GameObject indexTrigger;
	public GameObject handTrigger;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (step == 1 && !CR_Running) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab small things, use the index trigger. The one on top of the controllers." ;
			StartCoroutine(ToSetActive(indexTrigger));
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)>0.1f) step++;
		}

		if (step == 2) {
			indexTrigger.SetActive(false);
			nextButton.SetActive(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "Try to grab the small tube on the plate" ;
			avatar.GetComponent<OvrAvatar>().ShowControllers(false);
		}

		if (step == 3 && !CR_Running) {
			nextButton.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab bigger things, use the hand trigger. The one on the side of the controllers." ;
			StartCoroutine(ToSetActive(handTrigger));
		}
	}

	IEnumerator ToSetActive(GameObject obj) {
		CR_Running = true;
     	yield return new WaitForSeconds(0.5f);
		obj.SetActive(!isActive);
		isActive = !isActive;
		CR_Running = false;
 	}

	public void SetStep() {
		step++;
	}
}
