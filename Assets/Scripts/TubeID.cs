using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeID : MonoBehaviour
{

    public int WidthIndex,HeightIndex;
	// Use this for initialization
	void Start () {
		
	}

    public static TubeID CreateComponent(GameObject where, int x,int y)
    {
        TubeID myC = where.AddComponent<TubeID>();
        myC.WidthIndex = x;
        myC.HeightIndex = y;

        return myC;
    }

    public void SetIndex(int width , int height)
    {
        WidthIndex = width;
        HeightIndex = height;   
    }
    // Update is called once per frame
        void Update () {
		
	}
}
