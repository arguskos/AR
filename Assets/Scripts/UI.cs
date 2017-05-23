using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI : MonoBehaviour
{
	public  delegate  void OnNewLevel();

	public static UI Instane;

	public void Awake()
	{
		if (Instane == null)
		{
			Instane = this;
		}
	}

	public OnNewLevel OnLevelAction;
	public void OnButtonPress()
	{
		OnLevelAction();
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
