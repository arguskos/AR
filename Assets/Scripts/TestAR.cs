using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAR : MonoBehaviour
{
	public GameObject spawn;

	public GameObject Inage;	
	// Use this for initialization
	void Start ()
	{
		var o =Instantiate(spawn);
		o.transform.parent = Inage.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
