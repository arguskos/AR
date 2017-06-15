using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIProofOfConcept : MonoBehaviour {

    public GameObject Canvas;
    public GameObject ARCanvas;

    //private float Timer;
    //public float MaxTime;

    public GameObject RightBar;
    public GameObject TeamCreation;

    public float Speed = 10;
    private Animator _anim;

    private float _timer;


    public bool Debug;

    private bool _introAnimEnded = false;
    // Use this for initialization
    void Start ()
    {
        if (!Debug)
        {
            Speed = 1;
        }
        _anim = Canvas.GetComponent<Animator>();
        _anim.speed = Speed;

    }

    public void OnSettingPress()
    {
        CamerasManager.Instance.SetCamera(CamerasManager.Instance.UiCamera);

    }

    public void OnCameraPress()
    {
        CamerasManager.Instance.SetCamera(CamerasManager.Instance.ARCamera);
    }

    public void OnStatPress()
    {
        CamerasManager.Instance.SetCamera(CamerasManager.Instance.UiCamera);

    }
    // Update is called once per frame
    void Update ()
	{
	    if (_anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !_anim.IsInTransition(0) && !_introAnimEnded)
	    {
	        _introAnimEnded = true;
            //animation ended

            if (!Debug)
	        {

                TeamCreation.SetActive(true);

	        }
	        else
	        {
	            RightBar.SetActive(true);

            }
        }


        //Timer += Time.deltaTime;
        //if (Timer>= MaxTime)
        //{
        //    Canvas.GetComponent<Animator>().enabled = false;
        //}

    }
}
