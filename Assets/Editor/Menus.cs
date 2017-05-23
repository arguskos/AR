using UnityEngine;
using UnityEditor;
public static class Menus
{
	//[MenuItem("My Commands/First Command _p")]
	//static void FirstCommand()
	//{
	//	Debug.Log("You used the shortcut P");
		
	//}
	[MenuItem("My Commands/Special Command %g")]
	static void SpecialCommand()
	{
		Debug.Log("You used the shortcut Cmd+G (Mac)  Ctrl+G (Win)");
		var script = GameObject.Find("Grid").GetComponent<Grid>();
		script.CreateGrid();
	}
}