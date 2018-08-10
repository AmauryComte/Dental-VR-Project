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
	public GameObject buttonX;
	public GameObject indexTrigger;
	public GameObject handTrigger;
	public GameObject syringe;
	public GameObject tube;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (step == 1 && !CR_Running) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "The first command you need to learn is how to bring the clipboard to you. You can do that by pressing X on your left controller.";
			StartCoroutine(ToSetActive(buttonX));
			if (OVRInput.Get(OVRInput.Button.Three)) {
				nextButton.SetActive(true);
			}
		}

 		if (step == 2 && !CR_Running) {
			buttonX.SetActive(false);
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab small things, use the index trigger. The one on top of the controllers." ;
			StartCoroutine(ToSetActive(indexTrigger));
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)>0.1f) nextButton.SetActive(true);
		}

		if (step == 3) {
			indexTrigger.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(false);
			tube.SetActive(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "Try to grab the small tube on the plate" ;
			if (tube.GetComponent<TubeGrabControllerTutorial>().GetIsGrabbed()) nextButton.SetActive(true);
		}

		if (step == 4 && !CR_Running) {
			tube.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab bigger things, use the hand trigger. The one on the side of the controllers." ;
			StartCoroutine(ToSetActive(handTrigger));
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)>0.1f) nextButton.SetActive(true);
		}

		if (step == 5) {
			handTrigger.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(false);
			syringe.SetActive(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "Grab the syringe with the hand trigger.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetIsGrabbed()) nextButton.SetActive(true);
		}

		if (step == 6) {
			avatar.GetComponent<OvrAvatar>().ShowControllers(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "Now you have the syringe, you need to know how to use it. While grabbing it, press the index trigger to push the piston.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetPush()) step++;
		}

		if (step == 7) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "And to pull the piston back, you need to still grabbing the syringe, press the index trigger and press A at the same time.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetPull()) step++;
		}

		if (step == 8) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "Now try this without the controllers.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetAnimDone()) nextButton.SetActive(true);
		}

		if (step == 9) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "Well done, now you can start practicing. Press the left arrow on top of this cliboard to come back to the menu.";
		}
	}

	IEnumerator ToSetActive(GameObject obj) {
		CR_Running = true;
     	yield return new WaitForSeconds(0.4f);
		obj.SetActive(!isActive);
		isActive = !isActive;
		CR_Running = false;
 	}

	public void SetStep() {
		step++;
	}
}
