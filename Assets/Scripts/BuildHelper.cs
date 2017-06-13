using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class BuildHelper : MonoBehaviour
{
    public Camera Main;

    public GameObject AR;

    public MainGridDrawer Drawer;
	// Use this for initialization
	void Awake ()
	{
	    Drawer = FindObjectOfType<MainGridDrawer>();
	    Main = Camera.main;
	    AR = GameObject.Find("ARCamera");
	    if (Application.isEditor)

	    {
	        AR.SetActive(false);
            Main.gameObject.SetActive(true);

            Drawer.AR = false;
	    }

    }

    public void Start()
    {
        if (Application.isEditor)

        {
            AR.SetActive(false);
            Main.gameObject.SetActive(true);
            Drawer.AR = false;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
