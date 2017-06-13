using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using  UnityEngine.UI;

public class OnTracked : MonoBehaviour , ITrackableEventHandler
{
    public   GameObject ImageTarget;
    public Text text;
    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.TRACKED)
        {
            foreach (Transform obj in ImageTarget.transform)
            {
                
                obj.parent = null;
            }
            text.text = "tracked";

            GameObject.CreatePrimitive(PrimitiveType.Cube);
                
        }
        text.text = "something";
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
