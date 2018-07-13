using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : MonoBehaviour {
    GameObject forcep;
    Forceps sc;
    float t;
    public int pullNum=10;
    bool pullTrig = false;
    [SerializeField]
    protected OVRInput.Controller m_controller;
    Quaternion rot;
    // Use this for initialization
    void Start () {
        forcep = GameObject.Find("Grasping_forceps");
        sc = forcep.GetComponent<Forceps>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, m_controller) && !GetComponent<Collider>().enabled)
        {
            transform.parent = null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (forcep.GetComponent<Collider>() == other)
        {


            rot = GameObject.FindGameObjectWithTag("rHand").transform.rotation;
            Debug.Log(rot.eulerAngles.x + " / " + rot.eulerAngles.y + " / " + rot.eulerAngles.z);
            t = Time.time;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (forcep.GetComponent<Collider>() == other)
        {


            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, m_controller))
            {

                sc.traceHand = false;
                forcep.transform.parent = this.transform;
                Quaternion rHandrot = GameObject.FindGameObjectWithTag("rHand").transform.rotation;
                this.transform.rotation = Quaternion.Euler(rot.eulerAngles-rHandrot.eulerAngles  );

                float angle=Vector3.Angle(rot.eulerAngles, rHandrot.eulerAngles);
                //Debug.Log(angle);

                if (pullTrig)
                {
                    if (angle > 30f)
                    {
                        pullTrig = false;
                        pullNum--;
                        
                    }
                }
                else
                {
                    if (angle < 10f)
                    {
                        pullTrig = true;
                        pullNum--;
                        
                    }
                }
                //if (t+10f < Time.time)
                if(pullNum<0)
                {
                    forcep.transform.parent = null;
                    sc.traceHand = true;
                    GetComponent<Collider>().enabled = false;
                    transform.parent = forcep.transform;
                }
            }
            else
            {
                sc.traceHand = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (forcep.GetComponent<Collider>() == other)
        {
            sc.traceHand = true;
        }
    }
}
