using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void AnesthesiaScene() {
		SceneManager.LoadScene("Dental_Room/scene/dental_room");
	}

	public void QuitApp() {
		Application.Quit();
	}
}
