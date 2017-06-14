using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProofOfConcept : MonoBehaviour {

    public GameObject Canvas;
    private float Timer;
    public float MaxTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        if (Timer>= MaxTime)
        {
            Canvas.GetComponent<Animator>().enabled = false;
        }

    }
}
