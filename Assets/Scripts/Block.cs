using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class Block
{
	public Block(int x, int y, GameObject obj)
	{
		X = x;
		Y = y;
		Represent = obj;
		Connections.Add(new Dir(1, 0), Random.Range(0,2)==0 ? true : false);
		Connections.Add(new Dir(-1, 0), Random.Range(0, 2) == 0 ? true : false);
		Connections.Add(new Dir(0, -1), Random.Range(0,2) == 0 ? true : false);
		Connections.Add(new Dir(0, 1), Random.Range(0, 2) == 0 ? true : false);
		

	}


	public Dictionary<Dir, bool> Connections = new Dictionary<Dir, bool>();
	public int X, Y;
	public bool Watered;
	public virtual void DrawConnections()
	{
		foreach (var item in Connections)
		{
			if (item.Value)
			{
				var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
				cube.transform.localScale = Represent.transform.localScale * 0.1f;
				cube.transform.position = Represent.transform.position +
					new Vector3(Represent.transform.localScale.x/2 * item.Key.IndexWidth	, Represent.transform.localScale.y / 2,
					Represent.transform.localScale.x/2 * item.Key.IndexHeight);
				cube.GetComponent<Renderer>().material.color = Color.white;
				cube.transform.parent = Represent.transform;
			}
		}
		//if (ConnectonLeft)
		//{

		//	var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//	cube.transform.localScale = Represent.transform.localScale * 0.1f;
		//	cube.transform.position = Represent.transform.position +
		//		new Vector3(Represent.transform.localScale.x / 2, Represent.transform.localScale.y / 2,
		//		0);
		//	cube.GetComponent<Renderer>().material.color = Color.white;
		//	cube.transform.parent = Represent.transform;

		//}

		//if (ConnectionRight)
		//{

		//	var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//	cube.transform.localScale = Represent.transform.localScale * 0.1f;
		//	cube.transform.position = Represent.transform.position +
		//		new Vector3(-Represent.transform.localScale.x / 2, Represent.transform.localScale.y / 2,
		//		0);
		//	cube.GetComponent<Renderer>().material.color = Color.white;
		//	cube.transform.parent = Represent.transform;

		//}

		//if (ConnectionDonw)
		//{

		//	var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//	cube.transform.localScale = Represent.transform.localScale * 0.1f;
		//	cube.transform.position = Represent.transform.position +
		//		new Vector3(0, Represent.transform.localScale.y / 2,
		//		Represent.transform.localScale.x / 2);
		//	cube.GetComponent<Renderer>().material.color = Color.white;
		//	cube.transform.parent = Represent.transform;

		//}
		//if (ConnectionUp)
		//{

		//	var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		//	cube.transform.localScale = Represent.transform.localScale * 0.1f;
		//	cube.transform.position = Represent.transform.position +
		//		new Vector3(0, Represent.transform.localScale.y / 2,
		//		-Represent.transform.localScale.x / 2);
		//	cube.GetComponent<Renderer>().material.color = Color.white;
		//	cube.transform.parent = Represent.transform;
		//}



	}
	[System.NonSerialized]
	public GameObject Represent;
}


[System.Serializable]
public class EmptyBlock : Block
{
	public EmptyBlock(int x, int y, GameObject obj) : base(x, y, obj)
	{
	}
	public override void DrawConnections()
	{
	}



}