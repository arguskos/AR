using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generator : MonoBehaviour
{


    public  int MaxIndexWidth=5;
    public  int MaxIndexHeight=5;
    List<List<GameObject>> Tiles = new List<List<GameObject>>();
    public GameObject TileObjectPrefab;
    public GameObject EmptybjectPrefab;
    public GameObject ClickBoxPrefab;
    public GameObject Victory;
    public GameObject ImageTarget;
    public int StarWidth;
    private float _tileWidth = 0.4f;
    public float Scale;
    public GameObject Platform;
    public int StartHieght;
    public static bool BlockInput=false;
    public int EndWidth;
    public int EndHeight;
    public bool AR;
    public  GameObject _parentPlatform;
    public GameObject Base;
    public bool Generate;    
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    // Use this for initialization
    void Start()
    {
        if (Generate)
        {
            _tileWidth = Scale;

            _parentPlatform = Instantiate(Platform, transform.position,
                Platform.transform.rotation);
            _parentPlatform.transform.localScale= new Vector3(Scale*5.2f, _parentPlatform.transform.localScale.y, Scale*5.2f);
            //_parentPlatform.transform.Translate(0, 2, 0);
            if (AR)
            {
                Base.transform.parent = ImageTarget.transform;
                //Base.transform.Translate(0,_parentPlatform.transform.localScale.y,0);
            }
            _parentPlatform.transform.parent = Base.transform;
            // _parentPlatform.transform.position+= new Vector3(0,_parentPlatform.transform.localScale.x/2,0);
            Vector3 shift = _parentPlatform.transform.position + new Vector3(0, Scale/1.3f, 0) +
                            new Vector3(Scale/2, 0, Scale/2) -
                            new Vector3(_tileWidth*MaxIndexWidth, 0, _tileWidth*MaxIndexWidth)/2;
            //(transform.position - platfrom.transform.localScale)/2 + new Vector3(Scale / 2, 0, Scale / 2);
            for (int i = 0; i < MaxIndexWidth; i++)
            {
                Tiles.Add(new List<GameObject>());
                for (int j = 0; j < MaxIndexHeight; j++)

                {
                  
                    var c = Instantiate(ClickBoxPrefab, shift + new Vector3(i*_tileWidth, 0, j*_tileWidth),
                        ClickBoxPrefab.transform.rotation);

                    c.transform.localScale = new Vector3(Scale, Scale, Scale);
                    c.GetComponent<ClickBox>().WidthIndex = i;
                    c.GetComponent<ClickBox>().HeightIndex = j;
                    c.GetComponent<ClickBox>().Generator = this;

                    c.transform.parent = _parentPlatform.transform;

                    if (i != 1 || j != 1)
                    {
                        Tiles[i].Add(Instantiate(TileObjectPrefab, shift + new Vector3(i*_tileWidth, 0, j*_tileWidth),
                            TileObjectPrefab.transform.rotation));

                        int rnd = Random.Range(0, 6);
                        switch (rnd)
                        {
                            case 0:
                                Tiles[i][j].GetComponent<TileObject>().SlotDown = true;
                                Tiles[i][j].GetComponent<TileObject>().SlotUp = true;
                                break;
                            case 1:
                                Tiles[i][j].GetComponent<TileObject>().SlotLeft = true;
                                Tiles[i][j].GetComponent<TileObject>().SlotUp = true;

                                break;
                            case 2:
                                Tiles[i][j].GetComponent<TileObject>().SlotUp = true;
                                Tiles[i][j].GetComponent<TileObject>().SlotDown = true;

                                break;
                            case 3:
                                Tiles[i][j].GetComponent<TileObject>().SlotUp = true;

                                Tiles[i][j].GetComponent<TileObject>().SlotRight = true;
                                break;
                            case 4:
                                Tiles[i][j].GetComponent<TileObject>().SlotDown = true;

                                Tiles[i][j].GetComponent<TileObject>().SlotRight = true;
                                break;
                            case 5:
                                Tiles[i][j].GetComponent<TileObject>().SlotDown = true;
                                Tiles[i][j].GetComponent<TileObject>().SlotLeft = true;
                                Tiles[i][j].GetComponent<TileObject>().SlotUp = true;

                                Tiles[i][j].GetComponent<TileObject>().SlotRight = true;
                                break;
                        }

                        Tiles[i][j].GetComponent<TileObject>().MakeAxis();
                    }
                    else
                    {
                        Tiles[i].Add(Instantiate(EmptybjectPrefab, shift + new Vector3(i*_tileWidth, 0, j*_tileWidth),
                            EmptybjectPrefab.transform.rotation));
                        Tiles[i][j].GetComponent<TileObject>().IsEmpty = true;

                    }
                    Tiles[i][j].GetComponent<TileObject>().WidthIndex = i;
                    Tiles[i][j].GetComponent<TileObject>().HeightIndex = j;
                    Tiles[i][j].GetComponent<TileObject>().Generator = this;

                    Tiles[i][j].transform.localScale = new Vector3(Scale, Scale, Scale);
                    Tiles[i][j].transform.parent = _parentPlatform.transform;

                }
            }



  
            _parentPlatform.transform.rotation = transform.rotation;
        }
        else
        {
            var all = _parentPlatform.transform.GetComponentsInChildren<TileObject>();
           /// var allCL = _parentPlatform.transform.GetComponentsInChildren<ClickBox>();

            Vector3 shift = _parentPlatform.transform.position + new Vector3(0, 0.14f, 0) +
                        new Vector3(Scale / 2, 0, Scale / 2) -
                        new Vector3(_tileWidth * MaxIndexWidth, 0, _tileWidth * MaxIndexWidth) / 2;
            for (int i = 0; i < MaxIndexWidth; i++)
            {
                Tiles.Add(new List<GameObject>());
                for (int j =0 ; j < MaxIndexHeight ;j++)
                {
                    Tiles[i].Add(new GameObject());


                    /// allCL[j * MaxIndexHeight + i].Generator = this;
                }
            }
            foreach (var tile in all)
            {
             //   Destroy(Tiles[tile.WidthIndex][tile.HeightIndex].gameObject);
                Tiles[tile.WidthIndex][tile.HeightIndex] = tile.gameObject;
                Tiles[tile.WidthIndex][tile.HeightIndex].GetComponent<TileObject>().MakeAxis();
                var c = Instantiate(ClickBoxPrefab, shift + new Vector3(tile.WidthIndex * _tileWidth, 0, tile.HeightIndex * _tileWidth),
    ClickBoxPrefab.transform.rotation);

                c.transform.localScale = new Vector3(Scale, Scale, Scale);
                c.GetComponent<ClickBox>().WidthIndex = tile.WidthIndex;
                c.GetComponent<ClickBox>().HeightIndex = tile.HeightIndex;
                c.GetComponent<ClickBox>().Generator = this;

                c.transform.parent = _parentPlatform.transform;
                if (Tiles[tile.WidthIndex][tile.HeightIndex].GetComponent<TileObject>().IsLoked)
                {
                    Tiles[tile.WidthIndex][tile.HeightIndex].GetComponent<Renderer>().material.color = Color.gray;
                }


            }
        }
        //Tiles[StarWidth][StartHieght].GetComponent<TileObject>().IsStart = true;
        var obj = Tiles[StarWidth][StartHieght].GetComponent<TileObject>();
        ProcessTile(ref obj);

        //Tiles[EndWidth][EndHeight].GetComponent<Renderer>().material.color = Color.red;
        //Tiles[EndWidth][EndHeight].GetComponent<TileObject>().IsFinish = true;

    }

    public void SwitchOffAll()
    {
        for (int i = 0; i < Tiles.Count; i++)
        {
            for (int j = 0; j < Tiles[i].Count; j++)
            {
                if (Tiles[i][j].GetComponent<Renderer>())
                {
                    if (!Tiles[i][j].GetComponent<TileObject>().IsEmpty&&!Tiles[i][j].GetComponent<TileObject>().IsLoked)
                        Tiles[i][j].GetComponent<Renderer>().material.color = Color.black;
                    Tiles[i][j].GetComponent<TileObject>().IsOn = false;
                 
                }


            }
        }
    }

    private void ProcessTile(ref TileObject tile)
    {
        int WidthIndex = tile.WidthIndex;
        int HeightIndex = tile.HeightIndex;
        bool SlotDown = tile.SlotDown;
        bool SlotRight = tile.SlotRight;
        bool SlotLeft = tile.SlotLeft;
        bool SlotUp = tile.SlotUp;
        if (tile.IsEmpty)
        {
            return; 
        }
        tile.IsOn = true;
        tile.GetComponent<Renderer>().material.color = Color.blue;
        if (WidthIndex == EndWidth && HeightIndex == EndHeight)
        {
            Victory.SetActive(true);
        }
        if (SlotRight && WidthIndex > 0 && !Tiles[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty && Tiles[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().SlotLeft )
        {
            var obj = Tiles[WidthIndex - 1][HeightIndex].GetComponent<TileObject>();
            if (!obj.IsOn)
            {
              
                ProcessTile(ref obj); //.TurnOnInit(ref Tiles);

            }
        }
        if (SlotLeft && WidthIndex < MaxIndexWidth -1&& !Tiles[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsEmpty && Tiles[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().SlotRight )
        {
            var obj = Tiles[WidthIndex + 1][HeightIndex].GetComponent<TileObject>();
            if (!obj.IsOn)
            {
                ProcessTile(ref obj); //.TurnOnInit(ref Tiles);
            }

        }

        if (SlotDown && HeightIndex < MaxIndexHeight -1 && !Tiles[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsEmpty && Tiles[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().SlotUp )
        {
            var obj = Tiles[WidthIndex ][HeightIndex+1].GetComponent<TileObject>();
            if (!obj.IsOn)
            {
                ProcessTile(ref obj); //.TurnOnInit(ref Tiles);
            }

        }
        if (SlotUp && HeightIndex > 0 && !Tiles[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsEmpty && Tiles[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().SlotDown )
        {
            var obj = Tiles[WidthIndex ][HeightIndex-1].GetComponent<TileObject>();
            if (!obj.IsOn)
            {
                ProcessTile(ref obj); //.TurnOnInit(ref Tiles);
            }

        }
    }
    // Update is called once per frame
    void Update()
    {

        if (!BlockInput)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.GetComponent<ClickBox>() && hit.collider.transform.parent==_parentPlatform.transform)
                    {
                        hit.collider.GetComponent<ClickBox>()
                            .Click(ref Tiles);
                    }
                    SwitchOffAll();
                    var obj = Tiles[StarWidth][StartHieght].GetComponent<TileObject>();
                    //
                    //MAKE PROPER STARTING
                    //
                    if (obj.SlotRight)
                        ProcessTile(ref obj);
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.GetComponent<ClickBox>() )
                    {
                        hit.collider.GetComponent<ClickBox>()
                            .ClickRight(ref Tiles);
                    }

                }
            }
            if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
            {
                Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit raycastHit;

                if (Physics.Raycast(raycast, out raycastHit))
                {
                    if (raycastHit.collider.GetComponent<ClickBox>())
                    {
                        raycastHit.collider.GetComponent<ClickBox>()
                            .Click(ref Tiles);
                    }
                    SwitchOffAll();
                    var obj = Tiles[StarWidth][StartHieght].GetComponent<TileObject>();

                    ProcessTile(ref obj);
                }
            }
        }
        //if (Input.GetMouseButtonDown(1))
        //{
        //    RaycastHit hit;
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        if (hit.collider.GetComponent<TubeID>())
        //        {

        //            TileGrid[hit.collider.GetComponent<TubeID>().WidthIndex][hit.collider.GetComponent<TubeID>().HeightIndex]
        //                .TurnOn();
        //        }

        //    }
        //}
    }
}
