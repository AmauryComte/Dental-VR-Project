﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour {

	private int step = 0;
	private bool CR_Running = false;
	private bool CR_Running_2 = false;
	private bool CR_Running_3 = false;
	private bool isActive = false;
	private bool isActive_2 = false;
	private bool isActive_3 = false;

	public GameObject avatar;
	public GameObject nextButton;
	public GameObject startButton;

	public GameObject tutorial_Text;
	public GameObject clipboard;
	public GameObject buttonA;
	public GameObject buttonX;
	public GameObject rightIndexTrigger;
	public GameObject rightHandTrigger;
	public GameObject leftHandTrigger;
	public GameObject syringe;
	public GameObject tube;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (step == 0) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "Welcome to the tutorial !";
			buttonX.SetActive(false);
			startButton.SetActive(true);
		}

		if (step == 1 && !CR_Running) {
			leftHandTrigger.SetActive(false);
			tutorial_Text.GetComponent<TextMeshPro>().text = "The first command you need to learn is how to bring the clipboard to you. You can do that by pressing X on your left controller.";
			StartCoroutine(ToSetActive(buttonX));
			if (OVRInput.Get(OVRInput.Button.Three)) nextButton.SetActive(true);
		}

		if (step == 2 && !CR_Running) {
			rightIndexTrigger.SetActive(false);
			buttonX.SetActive(false);
			tutorial_Text.GetComponent<TextMeshPro>().text = "And you can grab it by pressing the hand trigger of your left controller.";
			StartCoroutine(ToSetActive(leftHandTrigger));
			if (clipboard.GetComponent<DentalUI>().GetIsGrabbed()) nextButton.SetActive(true);
		}

 		if (step == 3 && !CR_Running) {
			leftHandTrigger.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab small things, use the index trigger. The one on top of the controllers." ;
			StartCoroutine(ToSetActive(rightIndexTrigger));
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger)>0.1f) nextButton.SetActive(true);
		}

		if (step == 4) {
			rightIndexTrigger.SetActive(false);
			rightHandTrigger.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(false);
			tube.SetActive(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "Try to grab the small tube on the plate" ;
			if (tube.GetComponent<TubeGrabControllerTutorial>().GetIsGrabbed()) nextButton.SetActive(true);
		}

		if (step == 5 && !CR_Running) {
			tube.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab bigger things, use the hand trigger. The one on the side of the controllers." ;
			StartCoroutine(ToSetActive(rightHandTrigger));
			if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)>0.1f) nextButton.SetActive(true);
		}

		if (step == 6) {
			rightHandTrigger.SetActive(false);
			avatar.GetComponent<OvrAvatar>().ShowControllers(false);
			syringe.SetActive(true);
			tutorial_Text.GetComponent<TextMeshPro>().text = "Grab the syringe with the hand trigger.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetIsGrabbed()) nextButton.SetActive(true);
		}

		if (step == 7 && !CR_Running && !CR_Running_2) {
			avatar.GetComponent<OvrAvatar>().ShowControllers(true);
			StartCoroutine(ToSetActive(rightHandTrigger, 1));
			StartCoroutine(ToSetActive(rightIndexTrigger, 2));
			tutorial_Text.GetComponent<TextMeshPro>().text = "Now you have the syringe, you need to know how to use it. First, while grabbing it, press the index trigger to push the piston.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetPush()) nextButton.SetActive(true);
		}

		if (step == 8 && !CR_Running && !CR_Running_2 && !CR_Running_3) {
			avatar.GetComponent<OvrAvatar>().ShowControllers(true);
			StartCoroutine(ToSetActive(rightHandTrigger, 1));
			StartCoroutine(ToSetActive(rightIndexTrigger, 2));
			StartCoroutine(ToSetActive(buttonA, 3));
			tutorial_Text.GetComponent<TextMeshPro>().text = "Then to pull the piston back, you need to still grabbing the syringe, press the index trigger and press A at the same time.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetPull()) nextButton.SetActive(true);
		}

		if (step == 9) {
			avatar.GetComponent<OvrAvatar>().ShowControllers(false);
			rightHandTrigger.SetActive(false);
			rightIndexTrigger.SetActive(false);
			buttonA.SetActive(false);
			tutorial_Text.GetComponent<TextMeshPro>().text = "Now try this without the controllers.";
			if (syringe.GetComponent<SyringeGrabControllerTutorial>().GetAnimDone()) nextButton.SetActive(true);
		}

		if (step == 10) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "Well done, now you can start practicing. Press the home button to go back to home menu.";
		}
	}

	IEnumerator ToSetActive(GameObject obj) {
		CR_Running = true;
     	yield return new WaitForSeconds(0.4f);
		obj.SetActive(!isActive);
		isActive = !isActive;
		CR_Running = false;
 	}

	IEnumerator ToSetActive(GameObject obj, int value) {
		if (value==1) {
			CR_Running = true;
			yield return new WaitForSeconds(0.4f);
			obj.SetActive(!isActive);
			isActive = !isActive;
			CR_Running = false;
		}
		else if (value==2) {
			CR_Running_2 = true;
			yield return new WaitForSeconds(0.4f);
			obj.SetActive(!isActive_2);
			isActive_2 = !isActive_2;
			CR_Running_2 = false;
		}
		else {
			CR_Running_3 = true;
			yield return new WaitForSeconds(0.4f);
			obj.SetActive(!isActive_3);
			isActive_3 = !isActive_3;
			CR_Running_3 = false;
		}
 	}

	public void NextStep() {
		step++;
	}

	public void PreviousStep() {
		step--;
	}

	public void SetStep(int value) {
		step = value;
	}

	public void ReturnHome(){
		buttonA.SetActive(false);
		buttonX.SetActive(false);
		rightIndexTrigger.SetActive(false);
		rightHandTrigger.SetActive(false);
		leftHandTrigger.SetActive(false);
		syringe.SetActive(false);
		tube.SetActive(false);
		nextButton.SetActive(false);
		CR_Running = false;
		CR_Running_2 = false;
		CR_Running_3 = false;
		avatar.GetComponent<OvrAvatar>().ShowControllers(false);
		SetStep(0);
	}
}
