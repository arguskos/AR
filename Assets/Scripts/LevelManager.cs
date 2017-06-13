using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class LevelManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void ScaleCircle()
    {
        SceneManager.LoadScene("Scale3");
    }
    public void ScaleCirclePlaced()
    {
        SceneManager.LoadScene("Scaled2Fixed");
    }
    public void PlaceOnDots()
    {
        SceneManager.LoadScene("PlaceOnDots");
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            
        }
	}
}
