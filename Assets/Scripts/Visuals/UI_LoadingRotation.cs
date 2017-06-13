using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_LoadingRotation : MonoBehaviour {

    public float Speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float zrotation = Time.time * Speed;
        Vector3 rot = new Vector3(0, 0, zrotation);
        transform.localEulerAngles = rot;
    }
}
