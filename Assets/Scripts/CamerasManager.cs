using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerasManager : MonoBehaviour
{


    public Camera PuzzleCamera;

    public Camera ARCamera;
    public GameObject CameraEndPos;
    public static CamerasManager Instance;
    public void Awake()
    {
        if (Instance== null)
        {
            Instance = this;
        }
    }
	// Use this for initialization
	void Start () {
		
	}

    private IEnumerator InnerSwitching()
    {
        ARCamera.gameObject.SetActive(false);
        var pos  = ARCamera.gameObject.transform.position;
        var rot = ARCamera.gameObject.transform.rotation;

        PuzzleCamera.gameObject.SetActive(true);
        float time = 0;
        while (time<=1)
        {
            time += 0.01f;
            PuzzleCamera.transform.position = Vector3.Lerp(pos, CameraEndPos.transform.position, time);
            PuzzleCamera.transform.rotation = Quaternion.Lerp(rot, CameraEndPos.transform.rotation, time);
            yield return null;

        }
    }

    public void Switching()
    {
        StartCoroutine(InnerSwitching());
    }
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.W))
	    {
	      Switching();
	    }
	}
}
