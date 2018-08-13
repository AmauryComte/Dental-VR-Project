using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forceps : MonoBehaviour {
    [SerializeField]
    private Animator anim;

	private bool isGrabbed = false;
	private bool isInPlace = false;
	public GameObject syringe_Silhouette;
    public bool traceHand = true;

    public GameObject[] points;
    int pointNum = 0;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
	}
	

	// Called once per frame when trigger
    private void OnTriggerStay(Collider other)
    {

		if (other.tag.Equals("rHand")) {
			// On the first frame we initialize the thyringe transform parent to the rhand transform
			if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger)>0.1f && !isGrabbed && !isInPlace) {
				isGrabbed = true;
				transform.parent = other.transform;
			}

			// Then the syringe is grabbed and at Primaryindextrigger we set the anim
			else if (isGrabbed &&  OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger)>0.1f) {
					anim.SetBool ("open", false);
			}

			// Then we close the anim when release the index trigger
			else if (!anim.GetBool("open") && OVRInput.Get (OVRInput.Axis1D.PrimaryIndexTrigger)==0f) {
					anim.SetBool ("open", true);
			}

			// When we release the syringe grabb become false
			else if (OVRInput.Get (OVRInput.Axis1D.PrimaryHandTrigger)==0.0f && isGrabbed && !isInPlace) {
				if (!anim.GetBool("open")) anim.SetBool ("open", true);
				isGrabbed = false;
				transform.parent = null;
			}

			// if in place (near the silhouette) we fix the syringe at the silhouette place
			else if (isInPlace){
				transform.parent = null;
				//transorm.position = syringe_Silhouette.transform.position;
				transform.rotation = syringe_Silhouette.transform.rotation;
			}

		}

        /*if (points.Length>0) {
            if (other.gameObject.Equals(points[pointNum])){
                if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger, m_controller))
                {
                    points[pointNum].SetActive(false);
                    if (points.Length - 1 > pointNum)
                    {
                        points[pointNum + 1].SetActive(true);
                        pointNum++;
                    }
                    

                }
            }
        }
        }*/
    }

	public bool GetIsGrabbed() {
		return isGrabbed;
	}
    
	public void SetIsInPlace(bool b) {
		isInPlace = b;
	}

}
