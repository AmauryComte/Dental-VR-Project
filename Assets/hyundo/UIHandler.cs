using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIHandler : MonoBehaviour {
    public GameObject clipboard;

    private Transform[] texts;
    private int settingMode=0;
	// Use this for initialization
	void Start () {
        texts = clipboard.GetComponentsInChildren<Transform>() ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if (settingMode == 0)
        {
            if (other.tag == "TEXT1")
            {
                texts[1].GetComponent<TextMeshPro>().text = "ANESTHESIA";
                texts[2].GetComponent<TextMeshPro>().text = "SCAILING";
                texts[3].GetComponent<TextMeshPro>().text = "REMOVE TOOTH";
                settingMode++;
            }
        }else if (settingMode == 1)
        {
            if (other.tag == "TEXT1")
            {
                SceneManager.LoadScene("Dental_Room/scene/dental_room");
                
            }
        }
    }
}
