using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang.Environments;
using UnityEngine;


public class TubesGrid : MonoBehaviour
{

    public enum Sides { Top, Right, Down, Left };

    public static TubesGrid Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    public class TubeCell
    {
        public TubeCell(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public List<TubeCell> OutCells = new List<TubeCell>();
        public List<int> Connectors = new List<int>();
        public GameObject TubeObject;
        private int _x;
        private int _y;
        private bool _isWatered = false;
        public virtual void Rotate()
        {
            TubeObject.transform.Rotate(new Vector3(0, 90, 0));
        }

        public void TestNeighbors()
        {
            if (!_isWatered)
            {
                _isWatered = true;
                foreach (var cells in GetConnecteBothWays())
                {
                    cells.TubeObject.GetComponent<Renderer>().material.color = Color.black;
                    cells.TestNeighbors();
                }
            }
        }

        public List<TubeCell> GetConnecteBothWays()
        {
            List<TubeCell> retcells = new List<TubeCell>();

            foreach (var cells in GetConnectedNeighbors())
            {
                foreach (var otherCells in cells.GetConnectedNeighbors())
                {
                    if (otherCells == this)
                    {
                        retcells.Add(cells);

                    }
                }
            }
            return retcells;
        }
        public List<TubeCell> GetConnectedNeighbors()
        {
            List<TubeCell> cells = new List<TubeCell>();

            for (int i = 0; i < Connectors.Count; i++)
            {
                if (OutCells[Connectors[i]] != null && OutCells[Connectors[i]].OutCells != null)
                {
                    foreach (var connectorOther in OutCells[Connectors[i]].Connectors)
                    {
                        //if (OutCells[Connectors[i]].OutCells.Count == 4 && OutCells[Connectors[i]].OutCells[connectorOther] !=null && OutCells[Connectors[i]].OutCells[connectorOther]==this)
                        {
                            cells.Add(OutCells[Connectors[i]]);//.TubeObject.gameObject.GetComponent<Renderer>().material.color = Color.black;

                        }
                    }

                }

            }
            return cells;

        }
        public virtual void PostInit()
        {

        }
        public void WaterFlow()
        {
            for (int i = 0; i < Connectors.Count; i++)
            {
                if (OutCells[(int)Connectors[i]] != null)
                {
                    OutCells[(int)Connectors[i]].WaterFlow();
                    print("x:" + _x + " ,y:" + _y);
                }
            }
        }
    }

    public class StraightTube : TubeCell
    {
        public StraightTube(int x, int y) : base(x, y)
        {
            Connectors.Add((int)Sides.Top);
            Connectors.Add((int)Sides.Down);
        }

        public override void Rotate()
        {
            base.Rotate();
            for (int i = 0; i < Connectors.Count; i++)
            {
                Connectors[i] += 1;
                Connectors[i] %= 4;
            }
        }
    }
    public class RotatedTube : TubeCell
    {
        public RotatedTube(int x, int y) : base(x, y)
        {
            Connectors.Add((int)Sides.Top);
            Connectors.Add((int)Sides.Left);
        }

        public override void PostInit()
        {
            base.PostInit();
            Debug.Log("HEY");
            TubeObject.transform.Rotate(180, 0, 0);

        }

        public override void Rotate()
        {
            base.Rotate();

            for (int i = 0; i < Connectors.Count; i++)
            {
                Connectors[i] -= 1;
                if (Connectors[i] == -1)
                {
                    Connectors[i] = 3;
                }
            }
        }
    }

    public class CrossTube : TubeCell
    {
        public CrossTube(int x, int y) : base(x, y)
        {
            Connectors.Add((int)Sides.Top);
            Connectors.Add((int)Sides.Left);
            Connectors.Add((int)Sides.Down);
            Connectors.Add((int)Sides.Right);

        }

        public override void PostInit()
        {
            base.PostInit();

        }

        public override void Rotate()
        {
            base.Rotate();

            for (int i = 0; i < Connectors.Count; i++)
            {
                Connectors[i] -= 1;
                if (Connectors[i] == -1)
                {
                    Connectors[i] = 3;
                }
            }
        }
    }

    public class TTube : TubeCell
    {
        public TTube(int x, int y) : base(x, y)
        {
            Connectors.Add((int)Sides.Top);
            Connectors.Add((int)Sides.Left);
            Connectors.Add((int)Sides.Right);

        }

        public override void PostInit()
        {
            base.PostInit();


        }

        public override void Rotate()
        {
            base.Rotate();

            for (int i = 0; i < Connectors.Count; i++)
            {
                Connectors[i] -= 1;
                if (Connectors[i] == -1)
                {
                    Connectors[i] = 3;
                }
            }
        }
    }


    private  IEnumerator SlowShift(Tile t,Tile next,int width , int height)
    {
        Vector3 pos = t.TubeObject.transform.position;
        int i = t.TubeObject.GetComponent<TubeID>().WidthIndex;
        int y = t.TubeObject.GetComponent<TubeID>().HeightIndex;

        for (int time =0;time<8; time++)
        {
            t.TubeObject.transform.Translate(0.1f*width,0.1f*-height,0);
            yield return null;
        }
        //t.TubeObject.transform.Translate(-0.8f * width, 0.8f * height, 0);
        //Destroy(t.TubeObject.gameObject);
        //t.TubeObject = Instantiate(TubePrefabs[0], pos, TubePrefabs[0].transform.rotation);
       // t.TubeObject.transform.position = pos;
        //t.TubeObject.GetComponent<Renderer>().enabled = false;
        Destroy(t.TubeObject.GetComponent<TubeID>());
        TubeID.CreateComponent(t.TubeObject, i+width, y+height);
        t.TurnOff();

        //next.TubeObject.GetComponent<Renderer>().enabled = true;

    }
    private static bool ShiftInternal(int currentWidth, int currentHeight, int shiftWidth, int shiftHeight)
    {
        if (TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight] is Tile.Empty)
        {
            //neighborTile.TubeObject.GetComponent<Renderer>().enabled = true;
            //neighborTile.IsEmpty = false;
            //TubesGrid.TileGrid[currentWidth ][currentHeight ].TubeObject.transform.Translate(0.8f*shiftWidth,0.8f*shiftHeight,0);
            Tile t = TubesGrid.TileGrid[currentWidth][currentHeight];



            var obj = TubesGrid.TileGrid[currentWidth][currentHeight].TubeObject;
            TubesGrid.TileGrid[currentWidth][currentHeight] = new Tile.Empty(currentWidth, currentHeight);
            // TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight];
           // TubesGrid.TileGrid[currentWidth][currentHeight].TubeObject.GetComponent<Renderer>().material.color = Color.red;
            //TubesGrid.TileGrid[currentWidth ][currentHeight ].TubeObject.GetComponent<TubeID>().SetIndex(currentWidth , currentHeight );
            //TubesGrid.TileGrid[currentWidth ][currentHeight ].TubeObject.transform.Translate(shiftWidth * 0.8f, shiftHeight * 0.8f, 0);

            //TubesGrid.TileGrid[currentWidth][currentHeight].TubeObject =
            //    TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject;
            ////TubesGrid.TileGrid[currentWidth][currentHeight].WidthIndex = currentWidth;
            ////TubesGrid.TileGrid[currentWidth][currentHeight].HeightIndex = currentHeight;
            //Destroy(TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject.gameObject);
            var obj1 = TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject;
            TubesGrid.TileGrid[currentWidth][currentHeight].TubeObject = obj;
            TubesGrid.Instance.StartCoroutine(TubesGrid.Instance.SlowShift(TileGrid[currentWidth][currentHeight], TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight], shiftWidth, shiftHeight));

            TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight] = new Tile(currentWidth + shiftWidth, currentHeight + shiftHeight);
            TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject = obj1;
            
        
            //TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject.GetComponent<Renderer>().material.color = Color.black;
             TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject.GetComponent<TubeID>().SetIndex(currentWidth , currentHeight );
            //TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject.transform.Translate(-shiftWidth*0.8f,-shiftHeight*0.8f,0);
            //TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject = t.TubeObject;
            //TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].TubeObject.transform.Translate(0.8f * shiftWidth, 0.8f * shiftHeight, 0);
            //Destroy(TubesGrid.TileGrid[currentWidth][currentHeight].TubeObject);
            //Destroy(TubesGrid.TileGrid[currentWidth+shiftWidth][currentHeight+shiftHeight].TubeObject);

            //TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].WidthIndex = currentWidth +
            //                                                                                        shiftWidth;
            //TubesGrid.TileGrid[currentWidth + shiftWidth][currentHeight + shiftHeight].HeightIndex = currentHeight +
            //                                                                                         shiftHeight;
            return true;

        }
        return false;
    }

    public class Tile
    {
        public Tile(int widthIndex, int heightIndex)
        {

        }


        public bool IsEmpty = false;
        // public List<TubeCell> OutCells = new List<TubeCell>();
        //  public List<int> Connectors = new List<int>();
        public GameObject TubeObject;

        // private bool _isWatered = false;
        public bool SlotDown = false;
        public bool SlotUp = false;
        public bool SlotRight = false;
        public bool SlotLeft = false;
        private bool _IsOn = false;

        private int GetWidthIndex()
        {
            return TubeObject.GetComponent<TubeID>().WidthIndex;
        }
        private int GetHeightIndex()
        {
            return TubeObject.GetComponent<TubeID>().HeightIndex;
        }
        public virtual void TurnOn()
        {
            TubeObject.GetComponent<Renderer>().material.color = Color.blue;
            int width = GetWidthIndex();

            int height = GetHeightIndex();




            _IsOn = true;
            if (SlotUp && GetHeightIndex() > 0)
            {
                TubesGrid.TileGrid[width][height - 1].TurnOn();

            }
            if (SlotDown && GetHeightIndex() < HorizCells - 1)
            {
                TubesGrid.TileGrid[width][height + 1].TurnOn();

            }
            if (SlotRight && GetWidthIndex() > 0)
            {
                TubesGrid.TileGrid[width - 1][height].TurnOn();

            }
            if (SlotLeft && GetWidthIndex() < VertCells - 1)
            {
                TubesGrid.TileGrid[width + 1][height].TurnOn();

            }
        }
        public virtual void TurnOff()
        {
            int width = GetWidthIndex();
            int height = GetHeightIndex();
            TubeObject.GetComponent<Renderer>().material.color = Color.black;
            if (SlotUp && GetHeightIndex()>0)
            {
                TubesGrid.TileGrid[width][height-1].TurnOff();

            }
            if (SlotDown && GetHeightIndex() < HorizCells-1)
            {
                TubesGrid.TileGrid[width][height + 1].TurnOff();

            }
            if (SlotRight && GetWidthIndex() > 0)
            {
                TubesGrid.TileGrid[width-1][height ].TurnOff();

            }
            if (SlotLeft && GetWidthIndex() < VertCells-1)
            {
                TubesGrid.TileGrid[width+1][height ].TurnOff();

            }

            _IsOn = false;
        }

        public virtual void Shift()
        {
            int WidthIndex = TubeObject.GetComponent<TubeID>().WidthIndex;
            int HeightIndex = TubeObject.GetComponent<TubeID>().HeightIndex;

            print("Tile:" + WidthIndex + " " + HeightIndex);
            bool shifted = false;
            GameObject temp = TubeObject;
            Tile neighborTile = null;
            var currentTile = TubesGrid.TileGrid[WidthIndex][HeightIndex];
            if (WidthIndex > 0)
            {
                print("left:");
                if (ShiftInternal(WidthIndex, HeightIndex, -1, 0))
                {
                    return;
                }
            }
            if (WidthIndex < TubesGrid.VertCells - 1)
            {
                print("right:");
                if (ShiftInternal(WidthIndex, HeightIndex, 1, 0))
                {
                    return;
                }

            }


            if (HeightIndex < TubesGrid.HorizCells - 1)
            {
                print("down");
                if (ShiftInternal(WidthIndex, HeightIndex, 0, 1))
                {
                    return;
                }

            }
            if (HeightIndex >0)
            {
                print("up:");
                if (ShiftInternal(WidthIndex, HeightIndex, 0, -1))
                {
                    return;
                }

            }
            string outer = "";
            for (int i = 0; i < TubesGrid.TileGrid.Count; i++)
            {
                for (int j = 0; j < TileGrid[i].Count; j++)
                {
                    if (TileGrid[i][j] is Empty)
                    {
                        outer += "0";
                    }
                    else
                    {
                        outer += "1";
                    }

                }
                outer += "\n";
            }
            print(outer);




        }

        public class Empty : Tile
        {
            public Empty(int widthIndex, int heightIndex) : base(widthIndex, heightIndex)
            {
            }

            // public List<TubeCell> OutCells = new List<TubeCell>();
            //  public List<int> Connectors = new List<int>();
            // private bool _isWatered = false;
            public override void Shift()
            {
                //   print("empty" + WidthIndex + " " + HeightIndex);
            }


        }
    }

    // Use this for initialization
    private static int HorizCells = 5;
    private static int VertCells = 5;
    public GameObject ImageTarget;
    public static float WidthSpace = 0.8f;
    public static List<List<Tile>> TileGrid = new List<List<Tile>>();

    public static List<List<TubeCell>> TubeCellsGrid = new List<List<TubeCell>>();
    public GameObject AxisPrefab;

    public GameObject[] TubePrefabs;


    private void MakeAxis(Tile t)
    {
        if (t.SlotDown)
        {
            var ax = Instantiate(AxisPrefab,t.TubeObject.transform.position + new Vector3(0,0.2f,0.5f),Quaternion.identity);
            ax.transform.parent= t.TubeObject.transform;
        }
        if (t.SlotUp)
        {
            var ax = Instantiate(AxisPrefab, t.TubeObject.transform.position + new Vector3(0, 0.2f, -0.5f), Quaternion.identity);
            ax.transform.parent = t.TubeObject.transform;
        }
        if (t.SlotRight)
        {
            var ax = Instantiate(AxisPrefab, t.TubeObject.transform.position + new Vector3(-0.5f, 0.2f, 0.2f), Quaternion.identity);
            ax.transform.parent = t.TubeObject.transform;
        }
        if (t.SlotLeft)
        {
            var ax = Instantiate(AxisPrefab, t.TubeObject.transform.position + new Vector3(0.5f, 0.2f, 0.2f), Quaternion.identity);
            ax.transform.parent = t.TubeObject.transform;
        }
    }

    void Start()
    {
        for (int i = 0; i < HorizCells; i++)
        {

            ///
            /// 
            /// OLD TUBE SYSTEM
            /// 
            //TubeCellsGrid.Add(new List<TubeCell>());
            //for (int j = 0; j < VertCells; j++)
            //{
            //    int rnd = UnityEngine.Random.Range(0, 1);
            //    switch (rnd)
            //    {
            //        case 0:
            //            TubeCellsGrid[i].Add(new StraightTube(i, j));

            //            break;
            //        case 1:
            //            TubeCellsGrid[i].Add(new RotatedTube(i, j));
            //            break;
            //        case 2:
            //            TubeCellsGrid[i].Add(new CrossTube(i, j));
            //            break;
            //        case 3:
            //            TubeCellsGrid[i].Add(new TTube(i, j));
            //            break;

            //    }
            //    TubeCellsGrid[i][j].TubeObject = Instantiate(TubePrefabs[rnd],
            //        transform.position + new Vector3(WidthSpace*(i*1.0f), 0, WidthSpace*(j*1.0f)),
            //        TubePrefabs[rnd].transform.rotation);
            //    //TubeCellsGrid[i][j].TubeObject.transform.parent = ImageTarget.transform;
            //    TubeID.CreateComponent(TubeCellsGrid[i][j].TubeObject, i, j);
            //    TubeCellsGrid[i][j].PostInit();
            //}



            TileGrid.Add(new List<Tile>());
            for (int j = 0; j < VertCells; j++)
            {
                int rnd = UnityEngine.Random.Range(0, 1);
                //if (i == 1 && j == 1)
                //{
                //  TileGrid[i][j].TubeObject = Instantiate(TubePrefabs[1],
                // transform.position + new Vector3(WidthSpace * (i * 1.0f), 0, WidthSpace * (j * 1.0f)),
                //TubePrefabs[rnd].transform.rotation);


                //}
                // else
                //{
                switch (rnd)
                {
                    case 0:
                        if (i == 1 && j == 1)
                        {
                            TileGrid[i].Insert(j, new Tile.Empty(i, j));

                        }
                        else
                        {
                            TileGrid[i].Insert(j, new Tile(i, j));


                        }
                        break;

                }

                TileGrid[i][j].TubeObject = Instantiate(TubePrefabs[rnd],
 transform.position + new Vector3(WidthSpace * (i * 1.0f), 0, WidthSpace * (j * 1.0f)),
 TubePrefabs[rnd].transform.rotation);
                //}
                if (i == 1 && j == 1)
                {
                    TileGrid[i][j].TubeObject.GetComponent<Renderer>().enabled = false;

                }
              

                //TileGrid[i][j].TubeObject.transform.parent = ImageTarget.transform;
                //if (i == 2 && j == 2)
                //{
                //    //       TileGrid[i].Insert(j, new Tile.Empty(i, j));

                //    //       TileGrid[i][j].TubeObject = Instantiate(TubePrefabs[rnd],
                //    //transform.position + new Vector3(WidthSpace * (i * 1.0f), 0, WidthSpace * (j * 1.0f)),
                //    //TubePrefabs[rnd].transform.rotation);
                //    TileGrid[i][j].TubeObject.GetComponent<Renderer>().enabled = false;
                //    TileGrid[i][j].IsEmpty = true;
                //}
                TubeID.CreateComponent(TileGrid[i][j].TubeObject, i, j);
                TileGrid[i][j].TurnOff();

                if (i == 1 && j == 4)
                {

                    TileGrid[i][j].SlotUp = true;
                    TileGrid[i][j].SlotRight = true;

                    MakeAxis(TileGrid[i][j]);

                    TileGrid[i][j].TurnOn();

                }
            }



        }

        ///
        /// 
        /// CONNECT NEIGHBORS OLD 
        /// 
        /// 
        //for (int i = 0; i < HorizCells; i++)
        //{
        //    for (int j = 0; j < VertCells; j++)
        //    {
        //        if (j < VertCells - 1 && TubeCellsGrid[i][j + 1] != null)
        //            TubeCellsGrid[i][j].OutCells.Add(TubeCellsGrid[i][j + 1]);
        //        else
        //        {
        //            TubeCellsGrid[i][j].OutCells.Add(null);

        //        }
        //        if (i < HorizCells - 1 && TubeCellsGrid[i + 1] != null)
        //        {
        //            if (TubeCellsGrid[i + 1][j] != null)
        //                TubeCellsGrid[i][j].OutCells.Add(TubeCellsGrid[i + 1][j]);
        //            else
        //            {
        //                TubeCellsGrid[i][j].OutCells.Add(null);

        //            }
        //        }
        //        else
        //        {
        //            TubeCellsGrid[i][j].OutCells.Add(null);

        //        }
        //        if (j > 0 && TubeCellsGrid[i][j - 1] != null)
        //            TubeCellsGrid[i][j].OutCells.Add(TubeCellsGrid[i][j - 1]);
        //        else
        //        {
        //            TubeCellsGrid[i][j].OutCells.Add(null);

        //        }
        //        if (i > 0 && TubeCellsGrid[i - 1] != null)
        //        {
        //            if (TubeCellsGrid[i - 1][j] != null)
        //                TubeCellsGrid[i][j].OutCells.Add(TubeCellsGrid[i - 1][j]);
        //            else
        //            {
        //                TubeCellsGrid[i][j].OutCells.Add(null);

        //            }
        //        }
        //        else
        //        {
        //            TubeCellsGrid[i][j].OutCells.Add(null);

        //        }
        //    }
        //}



    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < HorizCells; i++)
            {
                for (int j = 0; j < VertCells; j++)
                {

                    TubeCellsGrid[i][j].Rotate();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<TubeID>())
                {
                    TileGrid[hit.collider.GetComponent<TubeID>().WidthIndex][hit.collider.GetComponent<TubeID>().HeightIndex]
                        .Shift();
                }

            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.GetComponent<TubeID>())
                {
                   
                    TileGrid[hit.collider.GetComponent<TubeID>().WidthIndex][hit.collider.GetComponent<TubeID>().HeightIndex]
                        .TurnOn();
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
        //            TubeCellsGrid[hit.collider.GetComponent<TubeID>().WidthIndex][hit.collider.GetComponent<TubeID>().WidthIndex]
        //                .Rotate();
        //        }

        //    }
        //}
    }
}

