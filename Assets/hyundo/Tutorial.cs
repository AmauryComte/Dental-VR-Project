using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour {

	private int step = 0;
	private bool CR_Running = false;
	public bool isActive = false;

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
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab small things, use the index trigger" ;
			StartCoroutine(ToSetActive(indexTrigger));
			//if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger)>0.1f) step++;
		}

		if (step == 2 && !CR_Running) {
			tutorial_Text.GetComponent<TextMeshPro>().text = "To grab bigger things, use the hand trigger" ;
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

	public void SetStep(int value) {
		step = value;
	}
}
