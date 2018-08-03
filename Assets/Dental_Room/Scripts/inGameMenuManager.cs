using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inGameMenuManager : MonoBehaviour {

	public void Pause() {
		Time.timeScale = 0f;
	}

	public void Resume () {
		Time.timeScale = 1f;
	}

	//SceneManager.GetActiveScene().name
	public void Restart () {
		SceneManager.LoadScene("Dental_Room/scene/dental_room");
	}

	public void BackToMenu (){
		SceneManager.LoadScene("hyundo/opening");
	}
}
