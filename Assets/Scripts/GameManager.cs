using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Emit;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Use this for initialization
    private  int _score;
    public  int Score
    {
        get { return _score; }
        set
        {
            _score = value;
            OnScoreUpdate();
        }
    }

    public static string TimeToEnd;

    public Action OnScoreUpdate;
    public Action OnTimeupdate;
    void Start ()
    {
   
    }

    public void FIreMe()
    {
        print("FIRED");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
