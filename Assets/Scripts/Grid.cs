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
	public  Coordinate(int w, int h)
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
	public TextAsset tx;
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
					block.Represent.GetComponent<Renderer>().material.color = Color.black;// = block.Represent.GetComponent<Renderer>().sharedMaterial;
				}
				block.Watered = false;
				
			}
		}
	}
	public void WaterFlow(int w,int h)
	{
		if (!Blocks[w][h].Watered)
		{
			Blocks[w ][h ].Represent
					.GetComponent<Renderer>().material.color = Color.blue;
			Blocks[w][h].Watered = true;

			foreach (var dir in GetValidDirs(w, h))
			{
				if (Blocks[w][h].Connections[dir] && Blocks[w+dir.IndexWidth][h+dir.IndexHeight].Connections[new Dir(dir.IndexWidth*-1,dir.IndexHeight*-1)] )
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
	public void SaveLevel(int number = 0,bool  rewrite=true)
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
				GameObject obj;

				if (i == EmptyBlockPos.IndexWidth && j == EmptyBlockPos.IndexHeight)
				{
					obj = Instantiate(pool.EmptyBlock, new Vector3(i * Size, 0, j * Size), Quaternion.identity);
					block = new EmptyBlock(i, j, obj);

				}
				else
				{
					obj = Instantiate(pool.Block, new Vector3(i * Size, 0, j * Size), Quaternion.identity);
					block = new Block(i, j, obj);
					obj.GetComponent<Renderer>().sharedMaterial.color = new Color(Random.value, Random.value, 1);
				}
				obj.AddComponent<BlockID>().IndexWidth = i;
				obj.GetComponent<BlockID>().IndexHeight = j;
				obj.transform.parent = Base.transform;
				Blocks[i].Add(block);

			}
		}
		Base.transform.position = transform.position;
		Base.transform.rotation = transform.rotation;
		SaveLevel(0,false);
	}


	public void ReCreateGrid(TextAsset txa)
	{
		string path = Application.dataPath + "/Resources/" + "level" + level.ToString() + "";
		TextAsset tx= (TextAsset)Resources.Load("level" + level.ToString());
		print(tx);
		print(path);

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

		Width = Blocks.Count;
		print(Width);
		Height = Blocks[0].Count;
		print(Height);
		Vector3 posBase = transform.position;
		Quaternion rotBase = transform.rotation;
		for (int i = 0; i < Width; i++)
		{
			for (int j = 0; j < Height; j++)
			{
				Block block;
				GameObject obj;
				Vector3 translate = new Vector3(i * Size, 0, j * Size) + posBase;
				Quaternion rotate = Quaternion.identity * rotBase	; 
				if ((Blocks[i][j] as EmptyBlock) != null)
				{
					print(PrefabsPool.Instanse.name);
					obj = Instantiate(PrefabsPool.Instanse.EmptyBlock, translate,rotate);
					block = new EmptyBlock(i, j, obj);

				}
				else
				{
					obj = Instantiate(PrefabsPool.Instanse.Block, translate, rotate);
					//block = new Block(i, j, obj);
					if (i==WaterStart.IndexWidth&&j==WaterStart.IndexHeight)
					{
						obj.GetComponent<Renderer>().material.color = Color.blue;
					}
					//obj.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, 1);
				}
				obj.AddComponent<BlockID>().IndexWidth = i;
				obj.GetComponent<BlockID>().IndexHeight = j;
				obj.GetComponent<BlockID>().Grid= this;

				obj.transform.parent = Base.transform;
				Blocks[i][j].Represent = obj;
				Blocks[i][j].DrawConnections();
				//Blocks[i].Add(block);

			}
		}

		WaterFlow(WaterStart.IndexWidth,WaterStart.IndexHeight);
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
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out hit))
			{
				print(hit.transform.name);
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
		if (Input.touchCount == 1)
		{
			// touch on screen
			if (Input.GetTouch(0).phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
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