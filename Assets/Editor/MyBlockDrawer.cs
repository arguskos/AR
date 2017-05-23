using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BlockID))]
[CanEditMultipleObjects]
public class MyBlockDrawer : Editor
{
	SerializedProperty Info;
	Grid _grid;
	PrefabsPool _pool;
	
	void OnEnable()
	{
		Info = serializedObject.FindProperty("BlockI");

		_pool = GameObject.FindObjectOfType<PrefabsPool>();
		_grid = GameObject.FindObjectOfType<Grid>(); 
	}

	
	public override void OnInspectorGUI()
	{
		//DrawDefaultInspector();

		DrawDefaultInspector();


		if (GUILayout.Button("Cicle"))
		{
			var t = (target as BlockID);

			if ((_grid.Blocks[t.IndexWidth][t.IndexHeight] as EmptyBlock) != null)
			{
				var obj=Instantiate(_pool.Block,t.transform.position, t.transform.rotation);
				//DestroyImmediate(_grid.Blocks[t.IndexWidth][t.IndexHeight].Represent);
				obj.transform.parent = t.transform.parent;
				obj.name = "Block";
				t.name = "unused";
				obj.AddComponent<BlockID>().IndexWidth = t.IndexWidth;
				obj.AddComponent<BlockID>().IndexHeight = t.IndexHeight;

				_grid.Blocks[t.IndexWidth][t.IndexHeight] = new Block(t.IndexWidth, t.IndexHeight, obj); 
			}
			else if ((_grid.Blocks[t.IndexWidth][t.IndexHeight] as Block) != null)
			{
				var obj = Instantiate(_pool.EmptyBlock, t.transform.position, t.transform.rotation);
				//DestroyImmediate(_grid.Blocks[t.IndexWidth][t.IndexHeight].Represent);
				obj.transform.parent = t.transform.parent;
				obj.name = "Empty";
				t.name = "unused";
				t.GetComponent<Renderer>().enabled = false;
				obj.AddComponent<BlockID>().IndexWidth = t.IndexWidth;
				obj.AddComponent<BlockID>().IndexHeight = t.IndexHeight;

				_grid.Blocks[t.IndexWidth][t.IndexHeight] = new EmptyBlock(t.IndexWidth, t.IndexHeight, obj);
			}

			_grid.SaveLevel(_grid.level);
			//{
			//	t.MomentInfo2.Clear();
			//	foreach (var temp in Parameters.Names)
			//	{
			//		t.MomentInfo2.Add(new MomentInfo(temp));
			//	}
		}


		this.Repaint();

	}
}
