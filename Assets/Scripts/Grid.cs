using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]
public class Coordinate
{
    public Coordinate()
    {
        IndexHeight = 0;
        IndexHeight = 0;
    }
    public Coordinate(int w, int h)
    {
        IndexHeight = w;
        IndexHeight = h;
    }
    public int IndexWidth;
    public int IndexHeight;
}

[System.Serializable]

public struct Dir
{
    public Dir(int x, int y)
    {
        IndexWidth = x;
        IndexHeight = y;
    }
    public int IndexWidth;
    public int IndexHeight;
}

public class Grid : MonoBehaviour
{

    public Coordinate EmptyBlockPos;
    public Coordinate WaterStart;
    public int Width, Height;
    public int level;
    public float Size;
    public List<List<Block>> Blocks;//= new List<List<Block>>();

    public IEnumerator SlowShift(GameObject obj, GameObject obj2)
    {
        float time = 0;
        Vector3 pos1 = obj.transform.position;
        Vector3 pos2 = obj2.transform.position;

        while (time < 1)
        {
            time += 0.2f; ;
            obj.transform.position = Vector3.Lerp(pos1, pos2, time);
            obj2.transform.position = Vector3.Lerp(pos2, pos1, time);
            yield return null;

        }

    }

    public void Shift(int indexWidth, int indexHeight, Dir dir, ref List<List<Block>> Blocks)
    {

        int otherW = indexWidth + dir.IndexWidth;
        int otherH = indexHeight + dir.IndexHeight;
        GameObject save = Blocks[indexWidth][indexHeight].Represent;
        Block temp = Blocks[indexWidth][indexHeight];
        Blocks[indexWidth][indexHeight] = Blocks[otherW][otherH];
        Blocks[otherW][otherH] = temp;

        //Blocks[indexWidth][indexHeight].X = Blocks[otherW][otherH].X;
        //Blocks[indexWidth][indexHeight].Y = Blocks[otherW][otherH].Y;
        //Blocks[indexWidth][indexHeight].Represent = Blocks[otherW][otherH].Represent;


        //Blocks[otherW][otherH].X = indexWidth;
        //Blocks[otherW][otherH].Y = indexHeight;
        //Blocks[otherW][otherH].Represent = save;

        Vector3 savePos = Blocks[indexWidth][indexHeight].Represent.transform.position;
        //Blocks[indexWidth][indexHeight].Represent.transform.position = Blocks[otherW][otherH].Represent.transform.position;
        //Blocks[otherW][otherH].Represent.transform.position = savePos;

        StartCoroutine(SlowShift(Blocks[indexWidth][indexHeight].Represent, Blocks[otherW][otherH].Represent));

        Blocks[otherW][otherH].Represent.GetComponent<BlockID>().IndexWidth = otherW;
        Blocks[otherW][otherH].Represent.GetComponent<BlockID>().IndexHeight = otherH;


        Blocks[indexWidth][indexHeight].Represent.GetComponent<BlockID>().IndexWidth = indexWidth;
        Blocks[indexWidth][indexHeight].Represent.GetComponent<BlockID>().IndexHeight = indexHeight;


    }
    //public void Shift(int indexWidth,int indexHeight,Dir dir, ref  List<List<Block>> Blocks)
    //{

    //	int otherW = indexWidth + dir.X;
    //	int otherH = indexHeight + dir.Y;
    //	GameObject save = Blocks[indexWidth][indexHeight].Represent;

    //	Blocks[indexWidth][indexHeight].X = Blocks[otherW][otherH].X;
    //	Blocks[indexWidth][indexHeight].Y = Blocks[otherW][otherH].Y;
    //	Blocks[indexWidth][indexHeight].Represent = Blocks[otherW][otherH].Represent;


    //	Blocks[otherW][otherH].X = indexWidth;
    //	Blocks[otherW][otherH].Y = indexHeight;
    //	Blocks[otherW][otherH].Represent = save;

    //	Vector3 savePos = Blocks[indexWidth][indexHeight].Represent.transform.position;
    //	Blocks[indexWidth][indexHeight].Represent.transform.position = Blocks[otherW][otherH].Represent.transform.position;
    //	Blocks[otherW][otherH].Represent.transform.position=savePos;

    //	Blocks[otherW][otherH].Represent.GetComponent<BlockID>().IndexWidth = otherW;
    //	Blocks[otherW][otherH].Represent.GetComponent<BlockID>().IndexHeight= otherH;


    //	Blocks[indexWidth][indexHeight].Represent.GetComponent<BlockID>().IndexWidth = indexWidth;
    //	Blocks[indexWidth][indexHeight].Represent.GetComponent<BlockID>().IndexHeight = indexHeight;


    //}
    public void SwithOffAll()
    {
        foreach (var item in Blocks)
        {
            foreach (Block block in item)
            {
                if (block.Watered)
                {
                    block.Represent.GetComponent<PuzzlePieceVisuals>().SetCorrect(false);
                    //block.Represent.GetComponent<Renderer>().material.color = Color.white;// = block.Represent.GetComponent<Renderer>().sharedMaterial;
                }
                block.Watered = false;

            }
        }
    }
    public void WaterFlow(int w, int h)
    {
        if (!Blocks[w][h].Watered && !(Blocks[w][h] is EmptyBlock))
        {
            //Blocks[w ][h ].Represent
            //		.GetComponent<Renderer>().material.color = Color.blue;
            Blocks[w][h].Represent.GetComponent<PuzzlePieceVisuals>().SetCorrect(true);

            Blocks[w][h].Watered = true;

            foreach (var dir in GetValidDirs(w, h))
            {
                if (Blocks[w][h].Connections[dir] && Blocks[w + dir.IndexWidth][h + dir.IndexHeight].Connections[new Dir(dir.IndexWidth * -1, dir.IndexHeight * -1)])
                {

                    WaterFlow(w + dir.IndexWidth, h + dir.IndexHeight);

                }
            }
        }

    }
    private List<Dir> GetValidDirs(int widthIndex, int heightIndex)
    {
        List<Dir> dirs = new List<Dir>();
        if (widthIndex > 0)
        {
            dirs.Add(new Dir(-1, 0));
        }
        if (widthIndex < Width - 1)
        {
            dirs.Add(new Dir(1, 0));
        }
        if (heightIndex > 0)
        {
            dirs.Add(new Dir(0, -1));
        }
        if (heightIndex < Height - 1)
        {
            dirs.Add(new Dir(0, 1));
        }
        return dirs;
    }
    public void SaveLevel(int number = 0, bool rewrite = true)
    {

        int num = number;
        string path = Application.dataPath + "/Resources/" + "level" + (num).ToString() + ".txt";
        if (!rewrite)
        {
            while (File.Exists(path))
            {
                num++;
                path = Application.dataPath + "/Resources/" + "level" + num.ToString() + ".txt";
            }
        }
        level = num;
        FileStream fs = new FileStream(path, FileMode.Create);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(fs, Blocks);
        fs.Close();
    }

    private void PlaceBlocs(int indexI,int indexJ,PrefabsPool prefabs, Block block,  out GameObject obj,Vector3 tranform)
    {
        obj = new GameObject("Delete me please");
        DestroyImmediate(obj);

        switch (block.Direction)
        {
            case Directions.LeftRIght:
                DestroyImmediate(obj);
                obj = Instantiate(prefabs.Straight, tranform, Quaternion.identity);
                break;
            case Directions.UpDown:
                DestroyImmediate(obj);

                obj = Instantiate(prefabs.Straight, tranform, Quaternion.identity);
                obj.transform.Rotate(0, 90, 0);
                break;
            case Directions.RightDown:
                DestroyImmediate(obj);

                obj = Instantiate(prefabs.Curved, tranform, Quaternion.identity);

                break;
            case Directions.LeftUp:
                DestroyImmediate(obj);
                obj = Instantiate(prefabs.Curved, tranform, Quaternion.identity);
                obj.transform.Rotate(0, -180, 0);

                break;
            case Directions.LeftDown:
                DestroyImmediate(obj);

                obj = Instantiate(prefabs.Curved, tranform, Quaternion.identity);
                obj.transform.Rotate(0, -270, 0);

                break;
            case Directions.RightUp:
                DestroyImmediate(obj);
                obj = Instantiate(prefabs.Curved, tranform, Quaternion.identity);
                obj.transform.Rotate(0, -90, 0);

                break;

        }
    }
    // Use this for initialization
    public void CreateGrid()
    {

        if (Blocks == null)
        {
            Blocks = new List<List<Block>>();
        }
        GameObject Base = new GameObject("GridBase");
        PrefabsPool pool = FindObjectOfType<PrefabsPool>();
        for (int i = 0; i < Width; i++)
        {
            Blocks.Add(new List<Block>());
            for (int j = 0; j < Height; j++)
            {
                Block block;
                GameObject obj=new GameObject("Delete me please");

                Vector3 translate = new Vector3(i * Size, 0, j * Size);

                if (i == EmptyBlockPos.IndexWidth && j == EmptyBlockPos.IndexHeight)
                {
                    DestroyImmediate(obj);
                    obj = Instantiate(pool.EmptyBlock, new Vector3(i * Size, 0, j * Size), Quaternion.identity);
                    block = new EmptyBlock(i, j, obj);

                }
                else
                {
                    block = new Block(i, j, null);
                    PlaceBlocs(i, j, pool, block, out obj,translate);




                    //obj.GetComponent<Renderer>().sharedMaterial.color = new Color(Random.value, Random.value, 1);
                }
                obj.AddComponent<BlockID>().IndexWidth = i;
                obj.GetComponent<BlockID>().IndexHeight = j;
                obj.transform.parent = Base.transform;
                Blocks[i].Add(block);

            }
        }
        Base.transform.position = transform.position;
        Base.transform.rotation = transform.rotation;
        SaveLevel(0, false);
    }


    public void ReCreateGrid()
    {
        string path = Application.dataPath + "/Resources/" + "level" + level.ToString() + "";
        TextAsset tx = (TextAsset)Resources.Load("level" + level.ToString());

        //if (File.Exists(path+".txt"))
        //{
        print(path);



        byte[] bytes = tx.bytes;
        MemoryStream reader = new MemoryStream(bytes);
        BinaryFormatter bf = new BinaryFormatter();
        Blocks = bf.Deserialize(reader) as List<List<Block>>;
        reader.Close();



        //}


        GameObject Base = gameObject;// new GameObject("GridBase");
        var plane = GameObject.CreatePrimitive(PrimitiveType.Cube);
        plane.transform.localScale = new Vector3(5 * Size, 0.01f, 5 * Size);
        plane.GetComponent<Renderer>().material.color = Color.gray;
        plane.transform.position = new Vector3(transform.position.x, transform.position.y - 0.01f, transform.position.z);
        plane.transform.parent = Base.transform;
        Destroy(plane.GetComponent<BoxCollider>());

        Width = Blocks.Count;
        print(Width);
        Height = Blocks[0].Count;
        print(Height);
        Quaternion rotBase = transform.rotation;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Block block;
                GameObject obj;
                Vector3 translate = new Vector3(i * Size-Size*Width/2+Size/2, 0, j * Size-Size*Height/2+Size/2);
                Quaternion rotate = Quaternion.identity * rotBase;
                if ((Blocks[i][j] as EmptyBlock) != null)
                {
                    obj = Instantiate(PrefabsPool.Instanse.EmptyBlock, translate, rotate);
                    obj.transform.localScale = new Vector3(Size,Size,Size);
                    block = new EmptyBlock(i, j, obj);

                }
                else
                {





                    //obj = Instantiate(PrefabsPool.Instanse.Straight, translate, rotate);

                    //switch ((Blocks[i][j].Direction))
                    //{
                    //    case Directions.LeftRIght:
                    //        DestroyImmediate(obj);
                    //        obj = Instantiate(PrefabsPool.Instanse.Straight, new Vector3(i * Size, 0, j * Size), Quaternion.identity);
                    //        break;
                    //    case Directions.UpDown:
                    //        DestroyImmediate(obj);

                    //        obj = Instantiate(PrefabsPool.Instanse.Straight, new Vector3(i * Size, 0, j * Size), Quaternion.identity);
                    //        obj.transform.Rotate(0, 90, 0);
                    //        break;
                    //    case Directions.RightDown:
                    //        DestroyImmediate(obj);

                    //        obj = Instantiate(PrefabsPool.Instanse.Curved, new Vector3(i * Size, 0, j * Size), Quaternion.identity);

                    //        break;
                    //    case Directions.LeftUp:
                    //        DestroyImmediate(obj);

                    //        obj = Instantiate(PrefabsPool.Instanse.Curved, new Vector3(i * Size, 0, j * Size), Quaternion.identity);
                    //        obj.transform.Rotate(0, -180, 0);


                    //        break;

                    //}




                    PlaceBlocs(i, j, PrefabsPool.Instanse, Blocks[i][j], out obj,translate);




                    //if (Blocks[i][j].Direction == Directions.UpDown)
                    //{
                    //    obj.transform.Rotate(0, 90, 0);
                    //}
                    //obj.GetComponent<Renderer>().material.color = Color.white;

                    //block = new Block(i, j, obj);
                    if (i == WaterStart.IndexWidth && j == WaterStart.IndexHeight)
                    {
                        //	obj.GetComponent<Renderer>().material.color = Color.blue;
                        obj.GetComponent<PuzzlePieceVisuals>().SetCorrect(true);

                    }
                    //obj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, 1);
                }
                obj.AddComponent<BlockID>().IndexWidth = i;
                obj.GetComponent<BlockID>().IndexHeight = j;
                obj.GetComponent<BlockID>().Grid = this;

                obj.transform.parent = Base.transform;
                Blocks[i][j].Represent = obj;
                //Blocks[i][j].DrawConnections();
                //Blocks[i].Add(block);

            }
        }

        WaterFlow(WaterStart.IndexWidth, WaterStart.IndexHeight);
    }
    public void Start()
    {

        WaterStart = new Coordinate(0, 0);

        //CreateGrid();	
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
                if (hit.transform.GetComponent<BlockID>() != null)
                {
                    BlockID b = hit.transform.GetComponent<BlockID>(); 
                    if (b.Grid == this)
                    {
                        foreach (var dir in GetValidDirs(b.IndexWidth, b.IndexHeight))
                        {
                            if ((Blocks[b.IndexWidth + dir.IndexWidth][
                                    b.IndexHeight + dir.IndexHeight] as EmptyBlock) != null)
                            {

                                Shift(b.IndexWidth, b.IndexHeight, dir, ref Blocks);
                                SwithOffAll();
                                WaterFlow(WaterStart.IndexWidth, WaterStart.IndexHeight);

                                break;
                            }
                            //Blocks[b.IndexWidth][b.IndexHeight].Shift(Blocks[b.IndexWidth + dir.X][b.IndexHeight + dir.Y], ref Blocks);
                        }
                    }
                }
            }



        }
        if (Input.touchCount == 1)
        {
            // touch on screen
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = CamerasManager.Instance.CurrentCamera.ScreenPointToRay(Input.GetTouch(0).position);
                RaycastHit hit = new RaycastHit();
                BlockID b = hit.transform.GetComponent<BlockID>();

                if (b.Grid == this)
                {
                    foreach (var dir in GetValidDirs(b.IndexWidth, b.IndexHeight))
                    {
                        if ((Blocks[b.IndexWidth + dir.IndexWidth][b.IndexHeight + dir.IndexHeight] as EmptyBlock) != null)
                        {

                            Shift(b.IndexWidth, b.IndexHeight, dir, ref Blocks);
                            SwithOffAll();
                            WaterFlow(WaterStart.IndexWidth, WaterStart.IndexHeight);

                            break;
                        }
                        //Blocks[b.IndexWidth][b.IndexHeight].Shift(Blocks[b.IndexWidth + dir.X][b.IndexHeight + dir.Y], ref Blocks);
                    }
                }
            }
        }
    }
}