using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderActivation : MonoBehaviour {
    private Slider slider;
    public bool isRot = true;
    public Transform valueMin, valueMax, value;
  
    [System.Serializable]
    public class ObjRig
    {

        public Transform transform;
        public Vector3 rotMin;
        public Vector3 rotMax;
    }


    public ObjRig[] objs;

    private void Start()
    {
        slider = GetComponent<Slider>();

        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("rIndex"))
        {
            value.position = other.transform.position;
           
            slider.value = (value.transform.localPosition.x - valueMin.transform.localPosition.x) / (valueMax.transform.localPosition.x - valueMin.transform.localPosition.x);

        }
    }
    public void PatientAngle()
    {
        if (isRot)
        {
            foreach (ObjRig tmp in objs)
            {
                tmp.transform.localRotation = Quaternion.Euler(slider.value * (tmp.rotMax - tmp.rotMin) + tmp.rotMin);
            }
        }
        else
        {
            foreach (ObjRig tmp in objs)
            {
                tmp.transform.localPosition = slider.value * (tmp.rotMax - tmp.rotMin) + tmp.rotMin;
            }
        }
    }
}
