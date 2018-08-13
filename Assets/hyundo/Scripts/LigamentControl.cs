using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigamentControl : MonoBehaviour {
    public Material ligamentMat;

    private float vibrateTime = 0f;
    OVRHapticsClip clip;
	// Use this for initialization
	void Start () {
        clip = new OVRHapticsClip(1);
        clip.WriteSample(255);
        

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (vibrateTime > 0f)
        {
            OVRHaptics.RightChannel.Queue(clip);
            vibrateTime -= Time.deltaTime;
        }
	}
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ligament")
        {
            RemoveTeethManager.Instance.remainLigament--;
            RemoveTeethManager.Instance.currentSequence = Sequence.Desmotomy;
            //진동
            vibrateTime = 0.5f;

            if (RemoveTeethManager.Instance.remainLigament == 0)
            {
                RemoveTeethManager.Instance.NextSequence();
                StartCoroutine("LigamentBleed");
            }
            other.enabled = false;

        }
    }

    IEnumerator LigamentBleed()
    {
        
        float g = 1;
        while (g > 0)
        {
            
            g -= 0.01f;

            ligamentMat.color = new Color(1, g, g);
            yield return new WaitForSeconds(0.05f);
        }

    }
    private void OnDisable()
    {
        ligamentMat.color = new Color(1, 1, 1);
    }
}
