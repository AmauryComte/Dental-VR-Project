using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AppSceneManager : MonoBehaviour {

	public GameObject syringe;
	public GameObject syringe_Silhouette;
	private float meanDistanceX;
	private float meanDistanceY;
	private float meanDistanceZ;

	public Text Instruction_Text;
	private int step;

	// Use this for initialization
	void Start () {
		step = 0;
		Instruction_Text.text = "Picked up the Syringe";
	}
	
	// Update is called once per frame
	void Update () {
		// First step, take the syringe
		if (syringe.GetComponent<SyringeController>().GetIsGrabbed() && step==0) {
			Instruction_Text.text = "Now place it in the orange silhouette";
			syringe_Silhouette.SetActive(true);
			step++;
		}

		// Second step, place it in the orange silhouette
		meanDistanceX = Mathf.Abs(syringe_Silhouette.transform.position.x - syringe.transform.position.x);
		meanDistanceY = Mathf.Abs(syringe_Silhouette.transform.position.y - syringe.transform.position.y);
		meanDistanceZ = Mathf.Abs(syringe_Silhouette.transform.position.z - syringe.transform.position.z);
		if (step==1 && meanDistanceX<0.01 && meanDistanceY<0.01 &&  meanDistanceZ < 0.01){
			syringe.GetComponent<SyringeController>().SetIsInPlace(true);
			syringe_Silhouette.SetActive(false);
			Instruction_Text.text = "Now press the index trigger to perform the anestesia";
			step++;
		}
	}
}
