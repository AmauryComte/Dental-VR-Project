using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class AppSceneManager : MonoBehaviour {

	public GameObject syringe;
	public GameObject syringe_Silhouette;
	public GameObject GLASS;
	public GameObject glass;
	public GameObject needle;
	public GameObject NEEDLE;
	public GameObject NEEDLE_BASE;
	public GameObject LUER_CAP;
	public GameObject point;

	private float meanDistanceX;
	private float meanDistanceY;
	private float meanDistanceZ;

	public GameObject Instruction_Text;
	private int step;

	// Use this for initialization
	void Start () {
		step = 0;
		Instruction_Text.GetComponent<TextMeshPro>().text = "Picked up the tube and place it in the syringe";
	}
	
	// Update is called once per frame
	void Update () {

		if(glass.GetComponent<SyringeMaker>().GetIsInPlace() && step==0) {
			Instruction_Text.GetComponent<TextMeshPro>().text = "Now take the needle and fix it to the syringe";
			glass.SetActive(false);
			GLASS.SetActive(true);
			step++;
		}

		if(needle.GetComponent<SyringeMaker>().GetIsInPlace() && step==1) {
			Instruction_Text.GetComponent<TextMeshPro>().text = "Then take the syringe and place it in the orange silhouette that will appear";
			needle.SetActive(false);
			NEEDLE.SetActive(true);
			NEEDLE_BASE.SetActive(true);
			LUER_CAP.SetActive(true);
			step++;
		}

		// Third step, take the syringe
		if (syringe.GetComponent<SyringeController>().GetIsGrabbed() && step==2) {
			Instruction_Text.GetComponent<TextMeshPro>().text = "Place it in the orange silhouette";
			syringe_Silhouette.SetActive(true);
			step++;
		}

		// Fourth step, place it in the orange silhouette
		meanDistanceX = Mathf.Abs(syringe_Silhouette.transform.position.x - syringe.transform.position.x);
		meanDistanceY = Mathf.Abs(syringe_Silhouette.transform.position.y - syringe.transform.position.y);
		meanDistanceZ = Mathf.Abs(syringe_Silhouette.transform.position.z - syringe.transform.position.z);
		if (meanDistanceX<0.01 && meanDistanceY<0.01 &&  meanDistanceZ < 0.01 && step==3){
			syringe.GetComponent<SyringeController>().SetIsInPlace(true);
			syringe_Silhouette.SetActive(false);
			Instruction_Text.GetComponent<TextMeshPro>().text = "Now drag the syringe to the red point, wich represent the position to perform the anesthesia";
			step++;
		}

		if(point.GetComponent<Point>().GetIsTouch() && step==4) {
			Instruction_Text.GetComponent<TextMeshPro>().text = "Finally you can press the Index trigger to anaesthetize the patient";
			step++;
		} 

		if (syringe.GetComponent<SyringeController>().GetAnimDone() && step==5) {
			Instruction_Text.GetComponent<TextMeshPro>().text = "Good Job !";
		}
	}
}
