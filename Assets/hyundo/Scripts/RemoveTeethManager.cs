using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum Sequence
{
    TeethCheck=0,GrabKnife,Desmotomy,PutDownKnife,GrabForcep,GrabTeeth,TwistTeeth,ExtractTeeth
}
public class RemoveTeethManager : MonoBehaviour {
    

    public static RemoveTeethManager Instance
    {
        get { return instance; }
    }
    private static RemoveTeethManager instance;


    public TextMeshPro description;
    public GameObject teeth;
    public GameObject knife;
    public GameObject ligament;
    public GameObject forcep;

    [HideInInspector]
    public int remainLigament = 3;


    public Sequence currentSequence
    {
        get { return _sequence; }
        set
        {
            _sequence = value;
            switch (_sequence)
            {
                case Sequence.TeethCheck:
                    description.text = "In this lesson, you can learn how to pull out a <color=\"yellow\">maxillary anterior tooth</color>.\n" +
                                     "Click the <color=\"red\">option button</color> to set the comfortable patient posture, and check the tooth you will pull out.";

                    teeth.GetComponent<ColorBlink>().isBlink = true;
                    break;

                case Sequence.GrabKnife:
                    description.text = "Grab the <color=\"yellow\">Lancet</color>.";
                    teeth.GetComponent<ColorBlink>().isBlink = false;
                    knife.GetComponent<ColorBlink>().isBlink = true;
                    knife.GetComponent<GrabTool>().isGrabbable = true;
                    break;

                case Sequence.Desmotomy:
                    description.text = "cut <color=\"yellow\">periodontal ligament</color>.\n" +
                                       "remain : "+remainLigament;
                    knife.GetComponent<ColorBlink>().isBlink = false;
                    ligament.GetComponent<ColorBlink>().isBlink = true;
                    break;
                case Sequence.PutDownKnife:
                    description.text = "Put down the Lancet.";
                    ligament.GetComponent<ColorBlink>().isBlink = false;
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
                    break;


            }
        }
    }




    private Sequence _sequence;
    
    public void TeethCheck()
    {
        if (currentSequence == Sequence.TeethCheck)
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
        currentSequence =  Sequence.GrabForcep;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
