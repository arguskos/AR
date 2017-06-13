using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBG_SphereRot : MonoBehaviour {

    public Vector3 SphereRotation;
    public float Speed = 1.0f;
    public int[] Directions = new int[3];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float rot = Time.time * Speed;
        SphereRotation = new Vector3(rot * Directions[0], rot * Directions[1], rot * Directions[2]);
        transform.localEulerAngles = SphereRotation;

    }
}
