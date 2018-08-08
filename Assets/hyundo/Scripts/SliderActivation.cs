using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderActivation : MonoBehaviour {
    private Slider slider;
    public Transform valueMin, valueMax, value;

    public Transform patientSpine;
    private float patientSpineMin = 33f, patientSpineMax = -12f;

    public Transform chairBody;
    private float chairMin = 0f, chairMax = 37f;


    private void Start()
    {
        slider = GetComponent<Slider>();

        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("rHand"))
        {
            value.position = other.transform.position;
            Debug.Log(value.localPosition);
            slider.value = (value.transform.localPosition.x - valueMin.transform.localPosition.x) / (valueMax.transform.localPosition.x - valueMin.transform.localPosition.x);

        }
    }
    public void PatientAngle()
    {
        patientSpine.localRotation = Quaternion.Euler(slider.value * (patientSpineMax - patientSpineMin) + patientSpineMin, 0, 0);
        chairBody.localRotation = Quaternion.Euler(slider.value * (chairMax - chairMin) + chairMin, 0, 0);
    }
}
