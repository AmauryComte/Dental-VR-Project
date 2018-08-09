using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderActivation : MonoBehaviour {
    private Slider slider;
    public Transform valueMin, valueMax, value;

    public Transform ob1;
    public float ob1Min = 33f, ob1Max = -12f;

    public Transform ob2;
    public float ob2Min = 0f, ob2Max = 37f;
    public float ob2Y=0f;


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
        ob1.localRotation = Quaternion.Euler(slider.value * (ob1Max - ob1Min) + ob1Min, 0, 0);
        ob2.localRotation = Quaternion.Euler(slider.value * (ob2Max - ob2Min) + ob2Min, ob2Y, 0);
    }
}
