using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AppSceneManager : MonoBehaviour {

	public GameObject syringe;
	public GameObject syringe_Silhouette;
	public GameObject GLASS;
	public GameObject glass;

	private float meanDistanceX;
	private float meanDistanceY;
	private float meanDistanceZ;

	public Text Instruction_Text;
	private int step;

	// Use this for initialization
	void Start () {
		step = 0;
		Instruction_Text.text = "Picked up the tube and place it in the syringe";
		Debug.Log(GLASS.transform.position.x);
		Debug.Log(glass.transform.position.x);
		Debug.Log(GLASS.transform.position.y);
		Debug.Log(glass.transform.position.y);
		Debug.Log(GLASS.transform.position.z);
		Debug.Log(glass.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		if(glass.GetComponent<SyringeMaker>().GetIsInPlace() && step==0) {
			Instruction_Text.text = "Now take the needle and fix it to the syringe";
			glass.SetActive(false);
			GLASS.SetActive(true);
			step++;
		}

		// First step, take the syringe
		if (syringe.GetComponent<SyringeController>().GetIsGrabbed() && step==2) {
			Instruction_Text.text = "Now place it in the orange silhouette";
			syringe_Silhouette.SetActive(true);
			step++;
		}

		// Second step, place it in the orange silhouette
		meanDistanceX = Mathf.Abs(syringe_Silhouette.transform.position.x - syringe.transform.position.x);
		meanDistanceY = Mathf.Abs(syringe_Silhouette.transform.position.y - syringe.transform.position.y);
		meanDistanceZ = Mathf.Abs(syringe_Silhouette.transform.position.z - syringe.transform.position.z);
		if (meanDistanceX<0.01 && meanDistanceY<0.01 &&  meanDistanceZ < 0.01 && step==3){
			syringe.GetComponent<SyringeController>().SetIsInPlace(true);
			syringe_Silhouette.SetActive(false);
			Instruction_Text.text = "Now press the index trigger to perform the anestesia";
			step++;
		}
	}
}
