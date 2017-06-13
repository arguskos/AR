using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonScoreViz : MonoBehaviour {

    public GameObject[] _Bones = new GameObject[6];
    public float[] _statValues = new float[6];
    public float _delay = 0.25f;
    private Vector3[] _boneStartPositions = new Vector3[6];
    private bool[] _isActivated = new bool[6];
    private float timeActive = 0.0f;

    //public SoundManager _sound;


	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < 6; i++)
        {
            _statValues[i] = 0.0f;
            _boneStartPositions[i] = _Bones[i].transform.position;
            _isActivated[i] = false;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        timeActive += Time.deltaTime;

        for (int i = 0; i < _statValues.Length; i++)
        {
            var amount = _statValues[i];
            var newpos = this.gameObject.transform.position + ((_boneStartPositions[i] - this.gameObject.transform.position) * amount);
            iTween.MoveTo(_Bones[i], newpos, _delay);

            //Debug
            if (timeActive >= 0.5f+(((float)i)/6.0f) && _isActivated[i] == false)
            {
                _statValues[i] = Random.Range(0.1f, 1.0f);
                _isActivated[i] = true;
            }


            if (Input.GetKeyDown(KeyCode.R))
            {
                //_sound.PlaySound("Selection");
                _isActivated[i] = false;
                _statValues[i] = 0.0f;
            }
        }
    }

    //Set stat function
    public void SetStat(int pos, float stat)
    {
        _statValues[pos] = stat;
    }

    //Alternate function with array
    public void SetStat(float[] statsarray)
    {
        _statValues = statsarray;
    }

}
