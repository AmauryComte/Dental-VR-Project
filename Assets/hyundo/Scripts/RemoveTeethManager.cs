using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum Sequence
{
    Ready=0,TeethCheck,GrabKnife,Desmotomy,PutDownKnife,GrabForcep,GrabTeeth,TwistTeeth,ExtractTeeth,Clear
}
public class RemoveTeethManager : MonoBehaviour {
    

    public static RemoveTeethManager Instance
    {
        get { return instance; }
    }
    private static RemoveTeethManager instance;
    

    public TextMeshPro description;
    public GameObject teeth;
    public GameObject teethDescription;
    public GameObject knife;
    public GameObject ligament;
    public GameObject ligamentDescription;
    public GameObject forcep;
    public GameObject grabTeethDescription;
    public GameObject TwistTeethDescription;
    
    public GameObject tray;
    [HideInInspector]
    public int remainLigament = 3;
    [HideInInspector]
    public int remainTwist = 6;

    public Sequence currentSequence
    {
        get { return _sequence; }
        set
        {
            _sequence = value;
            switch (_sequence)
            {
                case Sequence.Ready:
                    description.text = "In this lesson, you can learn how to pull out a <color=\"yellow\">maxillary anterior tooth</color>.\n" +
                                     "Click the <color=\"red\">option button</color>.";
                    
                    teeth.GetComponent<ColorBlink>().isBlink = true;
                    break;
                case Sequence.TeethCheck:
                    teethDescription.SetActive(true);

                    break;
                case Sequence.GrabKnife:
                    teethDescription.SetActive(false);
                    description.text = "Grab the <color=\"yellow\">Lancet</color>.";
                    teeth.GetComponent<ColorBlink>().isBlink = false;
                    knife.GetComponent<ColorBlink>().isBlink = true;
                    knife.GetComponent<GrabTool>().isGrabbable = true;
                    ligament.GetComponent<ColorBlink>().isBlink = false;
                    break;

                case Sequence.Desmotomy:
                    description.text = "cut <color=\"yellow\">periodontal ligament</color>.\nperiodontal ligaments link the gum and teeth. to remove tooth, you must cut the ligaments. \n\n" +
                                       "remain : "+remainLigament;
                    knife.GetComponent<ColorBlink>().isBlink = false;
                    ligament.GetComponent<ColorBlink>().isBlink = true;
                    ligamentDescription.SetActive(true);
                    break;
                case Sequence.PutDownKnife:
                    description.text = "Put down the Lancet.";
                    ligament.GetComponent<ColorBlink>().isBlink = false;
                    ligamentDescription.SetActive(false);
                    break;

                case Sequence.GrabForcep:
                    description.text = "Grab the <color=\"yellow\">Forcep</color>.";
                    knife.GetComponent<GrabTool>().isGrabbable = false;
                    forcep.GetComponent<ColorBlink>().isBlink = true;
                    forcep.GetComponent<GrabTool>().isGrabbable = true;
                    break;

                case Sequence.GrabTeeth:
                    description.text = "Grab the Tooth.";
                    forcep.GetComponent<ColorBlink>().isBlink = false;
                    teeth.GetComponent<ColorBlink>().isBlink = true;
                    grabTeethDescription.SetActive(true);
                    break;

                case Sequence.TwistTeeth:
                    description.text = "Twist the Tooth up and down<color=\"red\">slowly</color>.\n" +
                                       "If you Twist the Tooth to hard, your hand will vibrate.\n" +
                                       "remain : "+remainTwist;
                    grabTeethDescription.SetActive(false);
                    TwistTeethDescription.SetActive(true);
                    teeth.GetComponent<ColorBlink>().isBlink = false;
                    break;

                case Sequence.ExtractTeeth:
                    description.text = "Extract the Tooth! and put it on the tray";
                    tray.GetComponent<ColorBlink>().isBlink = true;
                    TwistTeethDescription.SetActive(false);

                    break;

                case Sequence.Clear:
                    description.text = "Clear!!";
                    tray.GetComponent<ColorBlink>().isBlink = false;
                    break;



            }
        }
    }




    private Sequence _sequence;
    
    public void TeethCheck()
    {
        
        if (currentSequence==Sequence.Ready||currentSequence == Sequence.TeethCheck)
        {
            currentSequence++;
        }
    }
    public void NextSequence()
    {
        currentSequence++;
    }
    public void PreviousSequence()
    {
        currentSequence--;
    }
    public void InitSequence()
    {
        currentSequence = 0;
    }












    private void Awake()
    {
        if (instance)
        {
            DestroyImmediate(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        
    }
    // Use this for initialization
    void Start () {
        currentSequence = 0;// Sequence.GrabForcep;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
