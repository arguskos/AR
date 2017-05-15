using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldsCicle : MonoBehaviour {

	// Use this for initialization
    public GameObject Base;
	void Start () {
		
	}

    public void NextField()
    {
        StartCoroutine(NextFieldCor());
    }

    private IEnumerator NextFieldCor()
    {
        float time = 0f;
        Vector3 start = Base.transform.eulerAngles;
        while (time < 1.0f)
        {
            time += 0.1f;
            Base.transform.eulerAngles = Vector3.Lerp(start,start+new Vector3(0,70,0), time);
            yield return null;
        }
    }
	// Update is called once per frame
	void Update () {
		
	}
}
