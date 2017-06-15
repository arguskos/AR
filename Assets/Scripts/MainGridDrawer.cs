using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGridDrawer : MonoBehaviour
{


    private float _size;
    public int[] Levels;
    public int SlideLength;
    private int _currentLevel;
    private List<GameObject> _grids = new List<GameObject>();
    private List<Dir> _dirs = new List<Dir>();
    public GameObject ImageTarget;
    public bool AR;
    public int PlaceRoolID = 1;
    private Vector3 _centerPos;
    public int SelectedGrid;
    public int PrevSelected = -1;

    public GridProperties Properties;
    public static MainGridDrawer Instance;


    public OnGridSellection OnGridSelection;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    // Use this for initialization
    void Start()
    {
        _size = Properties.Scale;
        _dirs.Add(new Dir(1, 0));
        _dirs.Add(new Dir(-1, 0));
        _dirs.Add(new Dir(0, 1));
        _dirs.Add(new Dir(0, -1));

        //UI.Instane.OnLevelAction += NextLevel;
        for (int i = 0; i < Levels.Length; i++)
        {
            #if UNITY_EDITOR
                        AR = false;
            #endif

         
            var gr = new GameObject("grid");
            gr.transform.localScale = new Vector3(_size * 5, _size * 5, +_size * 5);
            gr.AddComponent<BoxCollider>().size = new Vector3(1, 0.3f, 1);
            gr.AddComponent<GridBase>().GridID = i;

            Grid newGR = gr.AddComponent<Grid>();//.ReCreateGrid();

            newGR.Start();
            newGR.Size = _size;
            newGR.level = Levels[i];
            newGR.ReCreateGrid();
            if (AR)
            {
                gr.transform.parent = ImageTarget.transform;
            }
            // gr.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            if (PlaceRoolID == 1)
            {
                PlaceRool(ref gr, i);

            }
            else if (PlaceRoolID == 2)
            {
                PlaceRool2(ref gr, i);

            }
            else
            {
                PlaceRool3(ref gr, i);
            }
            _grids.Add(gr);


        }
        _centerPos = _grids[0].transform.position;
    }

    public void NextLevel()
    {
        if (_currentLevel < _grids.Count - 1)
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

    public void HideGrids()
    {
        foreach (var grid in _grids)
        {
            grid.SetActive(false);
        }
    }

    public void ShowGrids()
    {
        foreach (var grid in _grids)
        {
            grid.SetActive(true);
        }
    }
    private void PlaceRool(ref GameObject gr, int index)
    {
        gr.transform.position = new Vector3(-0.9f, -index, -0.9f);

        //StartCoroutine(SlowTranslate(gr, transform.position + new Vector3(0, -2 * index, 0)));
    }
    private void PlaceRool2(ref GameObject gr, int index)
    {
        var rotBase = new GameObject("rot");
        //foreach (Transform obj in  transform)
        //{
        //    gr.transform.position = new Vector3(4- 2.5f, 0, -2.5f)+objzzzz;

        //}
        rotBase.transform.parent = ImageTarget.transform;
        gr.transform.parent = rotBase.transform;

        gr.transform.position = new Vector3(Properties.DistanceFromCenter, 0, Properties.DistanceFromCenter);
        rotBase.transform.Rotate(0, 72 * index, 0);
        gr.transform.Rotate(new Vector3(00, -72, 0));


        //StartCoroutine(SlowTranslate(gr, transform.position + new Vector3(0, -2 * index, 0)));
    }

    private void PlaceRool3(ref GameObject gr, int index)
    {
        //foreach (Transform obj in  transform)
        //{
        //    gr.transform.position = new Vector3(4- 2.5f, 0, -2.5f)+objzzzz;

        //}
        gr.transform.position = ImageTarget.transform.GetChild(index).transform.position;
        gr.transform.rotation = ImageTarget.transform.GetChild(index).transform.rotation;



        //StartCoroutine(SlowTranslate(gr, transform.position + new Vector3(0, -2 * index, 0)));
    }
    private IEnumerator SlowTranslate(GameObject tr, Vector3 finPos)
    {
        float time = 0;
        Vector3 pos = tr.transform.position;
        while (time < 1)
        {
            time += Time.deltaTime;
            tr.transform.position = Vector3.Lerp(pos, finPos, time);
            yield return null;
        }
    }

    public void HideUnActive()
    {
        for (int i = 0; i < _grids.Count; i++)
        {
            if (i != SelectedGrid)
            {
                _grids[i].SetActive(false);
            }
            else
            {
                _grids[i].SetActive(true);

            }
        }
    }
    public IEnumerator SelectGrid()
    {

        //print(_grids.Count+" "+SelectedGrid);
        _grids[SelectedGrid].GetComponent<BoxCollider>().enabled = false;
        _grids[SelectedGrid].GetComponent<GridBase>().InitPos =
            _grids[SelectedGrid].GetComponent<GridBase>().transform.position;
        HideUnActive();
        float time = 0;
        Vector3 pos = _grids[SelectedGrid].gameObject.transform.position;
        Vector3 end = new Vector3(ImageTarget.transform.position.x, pos.y, ImageTarget.transform.position.z);
        while (time <= 1)
        {
            time += 0.1f;
            _grids[SelectedGrid].gameObject.transform.position = Vector3.Lerp(pos, end, time);
            if (PrevSelected != -1)
            {
                _grids[PrevSelected].gameObject.transform.position = Vector3.Lerp(end, _grids[PrevSelected].GetComponent<GridBase>().InitPos, time);
            }
            yield return null;
        }
        if (PrevSelected != -1)
        {
            _grids[PrevSelected].gameObject.GetComponent<BoxCollider>().enabled = true;

        }
        PrevSelected = SelectedGrid;
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            Ray ray = CamerasManager.Instance.CurrentCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {

                GridBase grid = hit.transform.GetComponent<GridBase>();

                if (grid != null)
                {
                    //action 
                    SelectedGrid = grid.GridID;
                    print("MainGridS");
                    StartCoroutine(OnGridSelection.SequenceCourutine(grid));
                }
            }

        }

        if (Input.touchCount == 1)
        {
            // touch on screen

            if (CamerasManager.Instance.CurrentCamera == CamerasManager.Instance.ARCamera && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = CamerasManager.Instance.CurrentCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit = new RaycastHit();
                GridBase grid = hit.transform.GetComponent<GridBase>();
                if (grid != null)
                {
                    //action 
                    SelectedGrid = grid.GridID;

                    StartCoroutine(OnGridSelection.SequenceCourutine(grid));
                }
            }
        }
    }
}
