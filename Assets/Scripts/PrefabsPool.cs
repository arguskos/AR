using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabsPool : MonoBehaviour {

	// Use this for initialization

	public GameObject Block, EmptyBlock;
	public static PrefabsPool Instanse;
	public void Awake()
	{
		if (Instanse==null)
		{
			Instanse = this;
		}
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
