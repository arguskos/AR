using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{

    public GameObject ImageTarget;
	// Use this for initialization
	void Start ()
	{
	    ImageTarget = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
