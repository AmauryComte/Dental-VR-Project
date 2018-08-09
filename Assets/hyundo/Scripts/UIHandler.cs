using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIHandler : MonoBehaviour {

    public void Anesthesia() {
        SceneManager.LoadScene("Dental_Room/scene/dental_room");
    }

    public void RemoveTeeth() {
        SceneManager.LoadScene("hyundo/Scenes/RemoveTeeth");
    }

    public void Quit () {
        Application.Quit();
    }

}
