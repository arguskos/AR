using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class TileObject : MonoBehaviour {

	// Use this for initialization
    public int WidthIndex = 0;
    public int HeightIndex = 0;
    public bool IsEmpty = false;
    public bool IsFinish= false;
    public bool IsStart = false;
    public bool IsOn = false;
    public bool SlotUp = false;
    public bool SlotRight = false;
    public bool SlotLeft = false;
    public bool SlotDown = false;
    public bool IsLoked = false;
    private float shift = 0.1f;
    public Generator Generator; 
    public GameObject AxisPrefab;
    void Start ()
    {
    }
    //public void TurnOff(ref List<List<GameObject>> grid)
    //{
    //    if (IsEmpty)
    //     {

    //        return;
    //    }

    //    bool on = false;
    //    if (SlotRight && WidthIndex > 0 && !grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().SlotLeft && grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsOn)
    //    {
    //        return;
            
    //    }
    //    if (SlotLeft && WidthIndex < Generator.MaxIndexWidth - 1 && !grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().SlotRight && grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsOn)
    //    {
    //        grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        on = true;
    //        IsOn = true;
    //        GetComponent<Renderer>().material.color = Color.blue;

    //    }

    //    if (SlotDown && HeightIndex < Generator.MaxIndexHeight - 1 && !grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().SlotUp && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsOn)
    //    {
    //        grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        on = true;
    //        IsOn = true;
    //        GetComponent<Renderer>().material.color = Color.blue;

    //    }
    //    if (SlotUp && HeightIndex > 0 && !grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().SlotDown && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsOn)
    //    {
    //        grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        on = true;
    //        IsOn = true;
    //        GetComponent<Renderer>().material.color = Color.blue;


    //    }
    //    if (!on)
    //    {
    //        IsOn = false;

    //        GetComponent<Renderer>().material.color = Color.black;

    //    }


    //}

    //public void TurnOn(ref List<List<GameObject>> grid)
    //{
    //    //if (IsOn )
    //    //    return;
    //    //IsOn = true;

    //    // GetComponent<Renderer>().material.color=Color.blue;
    //    if (IsEmpty )
    //    {
            
    //        return;
    //    }
       
    //bool on = false;
    //    if (SlotRight&&  WidthIndex > 0 && !grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().SlotLeft && grid[WidthIndex-1][HeightIndex ].GetComponent<TileObject>().IsOn) 
    //    {
    //        grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        on = true;
    //        IsOn = true;
    //        GetComponent<Renderer>().material.color = Color.blue;
    //    }
    //    if (SlotLeft&& WidthIndex < Generator.MaxIndexWidth -1  && !grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().SlotRight && grid[WidthIndex+1 ][HeightIndex ].GetComponent<TileObject>().IsOn)
    //    {
    //        grid[WidthIndex  +1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        on = true;
    //        IsOn = true;
    //        GetComponent<Renderer>().material.color = Color.blue;

    //    }

    //    if (SlotDown&& HeightIndex < Generator.MaxIndexHeight - 1 && !grid[WidthIndex][HeightIndex+1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex ][HeightIndex+1].GetComponent<TileObject>().SlotUp && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsOn)
    //    {
    //        grid[WidthIndex ][HeightIndex+1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        on = true;
    //        IsOn = true;
    //        GetComponent<Renderer>().material.color = Color.blue;

    //    }
    //    if (SlotUp&& HeightIndex >0 && !grid[WidthIndex][HeightIndex -1 ].GetComponent<TileObject>().IsEmpty && grid[WidthIndex ][HeightIndex-1].GetComponent<TileObject>().SlotDown && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsOn)
    //    {
    //        grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        on = true;
    //        IsOn = true;
    //        GetComponent<Renderer>().material.color = Color.blue;


    //    }
    //    if (!on)
    //    {
    //        IsOn = false;

    //        GetComponent<Renderer>().material.color = Color.black;

    //    }
    //}

    //private void ProcessTile(TileObject tile, ref List<List<GameObject>>  grid)
    //{
    //    int WidthIndex = tile.WidthIndex;
    //    int HeightIndex = tile.HeightIndex;
    //    if (SlotRight && WidthIndex > 0 && !grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().SlotLeft)
    //    {
    //        //grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        tile.GetComponent<Renderer>().material.color= Color.red;
    //    }
    //    if (SlotLeft && WidthIndex < Generator.MaxIndexWidth - 1 && !grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().SlotRight)
    //    {
    //        //grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        tile.GetComponent<Renderer>().material.color = Color.red;

    //    }

    //    if (SlotDown && HeightIndex < Generator.MaxIndexHeight - 1 && !grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().SlotUp)
    //    {
    //        // grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        tile.GetComponent<Renderer>().material.color = Color.red;

    //    }
    //    if (SlotUp && HeightIndex > 0 && !grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().SlotDown)
    //    {
    //        //grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //        tile.GetComponent<Renderer>().material.color = Color.red;

    //    }
    //}

   
    //public void TurnOnGrid(ref List<List<GameObject>> grid)
    //{
    //    if (IsOn)
    //        return;
    //    IsOn = true;

    //    GetComponent<Renderer>().material.color = Color.blue;
    //    if (SlotRight && WidthIndex > 0 && !grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().SlotLeft)
    //    {
    //        grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }
    //    if (SlotLeft && WidthIndex < Generator.MaxIndexWidth - 1 && !grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().SlotRight)
    //    {
    //        grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }

    //    if (SlotDown && HeightIndex < Generator.MaxIndexHeight - 1 && !grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().SlotUp)
    //    {
    //        grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }
    //    if (SlotUp && HeightIndex > 0 && !grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().SlotDown)
    //    {
    //        grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }
    //    //  TurnOff(ref grid);
    //}
    //public void TurnOffInit(ref List<List<GameObject>> grid)
    //{
    //    if (IsOn)
    //        return;
    //    IsOn = true;

    //    //GetComponent<Renderer>().material.color = Color.blue;
    //    if (SlotRight && WidthIndex > 0 && !grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().SlotLeft)
    //    {
    //        grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().TurnOffInit(ref grid);
    //    }
    //    if (SlotLeft && WidthIndex < Generator.MaxIndexWidth - 1 && !grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().SlotRight)
    //    {
    //       grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().TurnOffInit(ref grid);
    //    }

    //    if (SlotDown && HeightIndex < Generator.MaxIndexHeight - 1 && !grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().SlotUp)
    //    {
    //       grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().TurnOffInit(ref grid);
    //    }
    //    if (SlotUp && HeightIndex > 0 && !grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().SlotDown)
    //    {
    //      grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().TurnOffInit(ref grid);
    //    }
    //    //  TurnOff(ref grid);
    //}
    //public void TurnOnInit(ref List<List<GameObject>> grid)
    //{
    //    if (IsOn)
    //        return;
    //    IsOn = true;

    //    GetComponent<Renderer>().material.color = Color.blue;
    //    if (SlotRight && WidthIndex > 0 && !grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().SlotLeft)
    //    {
    //        grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }
    //    if (SlotLeft && WidthIndex < Generator.MaxIndexWidth - 1 && !grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().IsEmpty && grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().SlotRight )
    //    {
    //        grid[WidthIndex + 1][HeightIndex].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }

    //    if (SlotDown && HeightIndex < Generator.MaxIndexHeight - 1 && !grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().SlotUp )
    //    {
    //        grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }
    //    if (SlotUp && HeightIndex > 0 && !grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsEmpty && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().SlotDown )
    //    {
    //        grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().TurnOnInit(ref grid);
    //    }
    //  //  TurnOff(ref grid);
    //}
    public void MakeAxis()
    {
        if (!IsEmpty)
        {
            if (SlotDown)
            {
                var ax = Instantiate(AxisPrefab, transform.position + new Vector3(0, 0.1f, shift), Quaternion.identity);
                ax.transform.parent = transform;
            }
            if (SlotUp)
            {
                var ax = Instantiate(AxisPrefab, transform.position + new Vector3(0, 0.1f, -shift), Quaternion.identity);
                ax.transform.parent = transform;
            }
            if (SlotRight)
            {
                var ax = Instantiate(AxisPrefab, transform.position + new Vector3(-shift, 0.1f, 0.0f),
                    Quaternion.identity);
                ax.transform.parent = transform;
            }
            if (SlotLeft)
            {
                var ax = Instantiate(AxisPrefab, transform.position + new Vector3(shift, 0.1f, 0.0f),
                    Quaternion.identity);
                ax.transform.parent = transform;
            }
        }
    }
    public void Click(List<List<GameObject>> grid)
    {
        //print("w index:"+WidthIndex+ " H index: "+HeightIndex);
        //Vector3 pos = transform.position;
        //if (WidthIndex > 0 && grid[WidthIndex-1][HeightIndex].GetComponent<EmptyObject>() )
        //{
        //    var obj = Instantiate(grid[WidthIndex][HeightIndex], grid[WidthIndex - 1][HeightIndex].transform.position, grid[WidthIndex][HeightIndex].transform.rotation);
        //    var obj1 = Instantiate(grid[WidthIndex - 1][HeightIndex], grid[WidthIndex][HeightIndex].transform.position, grid[WidthIndex - 1][HeightIndex].transform.rotation);

        //    Destroy(grid[WidthIndex][HeightIndex]);
        //    Destroy(grid[WidthIndex - 1][HeightIndex]);

        //    grid[WidthIndex][HeightIndex] = obj;
        //    grid[WidthIndex - 1][HeightIndex] = obj1;
        //}
        //else if ( grid[WidthIndex + 1][HeightIndex].GetComponent<EmptyObject>())
        //{
        //    var obj = Instantiate(grid[WidthIndex][HeightIndex], grid[WidthIndex+1][HeightIndex].transform.position, grid[WidthIndex][HeightIndex].transform.rotation);
        //    var obj1 = Instantiate(grid[WidthIndex+1][HeightIndex ], grid[WidthIndex][HeightIndex].transform.position, grid[WidthIndex+1][HeightIndex ].transform.rotation);

        //    Destroy(grid[WidthIndex][HeightIndex]);
        //    Destroy(grid[WidthIndex+1][HeightIndex ]);

        //    grid[WidthIndex][HeightIndex] = obj;
        //    grid[WidthIndex+1][HeightIndex ] = obj1;
        //}
        //else if (grid[WidthIndex ][HeightIndex+1].GetComponent<EmptyObject>())
        //{

        //    var obj = Instantiate(grid[WidthIndex][HeightIndex], grid[WidthIndex][HeightIndex + 1].transform.position, grid[WidthIndex][HeightIndex].transform.rotation);
        //    var obj1 = Instantiate(grid[WidthIndex][HeightIndex + 1], grid[WidthIndex][HeightIndex].transform.position, grid[WidthIndex][HeightIndex + 1].transform.rotation);

        //    Destroy(grid[WidthIndex][HeightIndex]);
        //    Destroy(grid[WidthIndex][HeightIndex + 1]);

        //    grid[WidthIndex][HeightIndex] = obj;
        //    grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex++;
        //    grid[WidthIndex][HeightIndex + 1] = obj1;
        //    grid[WidthIndex][HeightIndex + 1].GetComponent<EmptyObject>().HeightIndex--;
        //}
        //else if (grid[WidthIndex][HeightIndex - 1].GetComponent<EmptyObject>())
        //{
        //    var obj=Instantiate(grid[WidthIndex][HeightIndex], grid[WidthIndex ][HeightIndex-1].transform.position, grid[WidthIndex][HeightIndex].transform.rotation);
        //    var obj1 = Instantiate(grid[WidthIndex][HeightIndex-1], grid[WidthIndex][HeightIndex].transform.position, grid[WidthIndex][HeightIndex-1].transform.rotation);

        //    Destroy(grid[WidthIndex][HeightIndex]);
        //    Destroy(grid[WidthIndex][HeightIndex-1]);

        //    grid[WidthIndex][HeightIndex] = obj;
        //    grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex--;
        //    grid[WidthIndex][HeightIndex-1] = obj1;
        //    grid[WidthIndex][HeightIndex-1].GetComponent<EmptyObject>().HeightIndex++;

        //}
        //return grid;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
