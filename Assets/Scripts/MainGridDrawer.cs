using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class MainGridDrawer : MonoBehaviour
{


	public float Size;
	public int[] Levels;
	public int SlideLength;
	private int _currentLevel;
	private List<GameObject> _grids = new List<GameObject>();
	private List<Dir> _dirs = new List<Dir>();
	public GameObject ImageTarget;
	//TODO dele unuse text asset;
	public bool AR;
	private Vector3 _centerPos;
	// Use this for initialization
	void Start()
	{
		_dirs.Add(new Dir(1, 0));
		_dirs.Add(new Dir(-1, 0));
		_dirs.Add(new Dir(0, 1));
		_dirs.Add(new Dir(0, -1));

		UI.Instane.OnLevelAction += NextLevel;
		for (int i = 0; i < Levels.Length; i++)
		{
			var gr = new GameObject("grid");
			Grid newGR = gr.AddComponent<Grid>();//.ReCreateGrid();
			PlaceRool(ref gr, i);
			newGR.Start();
			newGR.Size = Size;
			newGR.level = Levels[i];
			newGR.ReCreateGrid();
			if (AR)
			{
				gr.transform.parent = ImageTarget.transform;
			}
			_grids.Add(gr);
		}
		_centerPos = _grids[0].transform.position;
		Server.Instance.EnteredRoom();
	}

	public void NextLevel()
	{
		if (_currentLevel < _grids.Count-1)
		{
			Vector3 pos = _grids[_currentLevel].transform.position;
			//_grids[_currentLevel].transform.position = new Vector3(
			//	pos.x + _dirs[_currentLevel].IndexWidth * SlideLength,
			//	pos.y, pos.z + _dirs[_currentLevel].IndexHeight * SlideLength
			//);

			StartCoroutine(SlowTranslate(_grids[_currentLevel], new Vector3(
				pos.x + _dirs[_currentLevel].IndexWidth * SlideLength,
				pos.y, pos.z + _dirs[_currentLevel].IndexHeight * SlideLength
			)));

			_currentLevel++;
		}
		//_currentLevel %= _grids.Count;
	}

	private void PlaceRool(ref GameObject gr, int index)
	{
		gr.transform.position = transform.position + new Vector3(0, -0.4f * index, 0);
		//StartCoroutine(SlowTranslate(gr, transform.position + new Vector3(0, -2 * index, 0)));
	}

	private IEnumerator SlowTranslate(GameObject tr, Vector3 finPos)
	{
		float time = 0;
		Vector3 pos = tr.transform.position;
		while (time<1)
		{
			time += Time.deltaTime;
			tr.transform.position = Vector3.Lerp(pos, finPos, time);
			yield return null;
		}
	}
	// Update is called once per frame
	void Update()
	{

	}
}
