using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlink : MonoBehaviour {
    public Material[] origins;
    public Material[] change;
    
    public bool isBlink=false;

    
    private bool isOrigin = true;
    private MeshRenderer[] mrs;
	// Use this for initialization
	void Start () {
        mrs = GetComponentsInChildren<MeshRenderer>();
        
        StartCoroutine("Blink");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    IEnumerator Blink()
    {
        while (true)
        {
            if (isBlink)
            {
                if (isOrigin)
                {
                    foreach (MeshRenderer mr in mrs)
                        mr.materials = change;
                    
                }
                else
                {
                    foreach (MeshRenderer mr in mrs)
                        mr.materials = origins;
                }
                isOrigin = (!isOrigin);
            }
            else
            {
                foreach (MeshRenderer mr in mrs)
                    mr.materials = origins;
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
}
