using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ClickBox : MonoBehaviour {


    public int WidthIndex = 0;
    public int HeightIndex = 0;
    public Generator Generator;
    // Use this for initialization
    void Start () {
		
	}

    public void ClickRight(ref List<List<GameObject>> grid)

    {
        
    }

    public IEnumerator SlowShift(GameObject tile1, GameObject tile2, Vector3 pos1,Vector3 pos2)
    {
        Generator.BlockInput = true;
        float time = 0.0f;

        while (time<1.0f)
        {
            time +=0.1f;
            tile1.transform.position=Vector3.Lerp(tile1.transform.position, pos2, time);
            tile2.transform.position = Vector3.Lerp(tile2.transform.position, pos1, time);


            yield return null;
        }
        Generator.BlockInput = false;

    }
    public void Click(ref List<List<GameObject>> grid)
    {
        print("w index:" + WidthIndex + " H index: " + HeightIndex);
        Vector3 pos = transform.position;
        //if (grid[WidthIndex][HeightIndex].GetComponent<TileObject>().IsStart ||
        //    grid[WidthIndex][HeightIndex].GetComponent<TileObject>().IsFinish)
        //{
        //    return;
            
        //}
        if (grid[WidthIndex][HeightIndex].GetComponent<TileObject>().IsLoked)
        {
            return;
        }
        int tempW = grid[WidthIndex][HeightIndex].GetComponent<TileObject>().WidthIndex;
        int tempH = grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex;

        //if (WidthIndex > 0 && grid[WidthIndex - 1][HeightIndex].GetComponent<EmptyObject>())
        //{
        //    var obj = Instantiate(grid[WidthIndex][HeightIndex], grid[WidthIndex - 1][HeightIndex].transform.position, grid[WidthIndex][HeightIndex].transform.rotation);
        //    var obj1 = Instantiate(grid[WidthIndex - 1][HeightIndex], grid[WidthIndex][HeightIndex].transform.position, grid[WidthIndex - 1][HeightIndex].transform.rotation);

        //    Destroy(grid[WidthIndex][HeightIndex]);
        //    Destroy(grid[WidthIndex - 1][HeightIndex]);

        //    grid[WidthIndex][HeightIndex] = obj;
        //    grid[WidthIndex - 1][HeightIndex] = obj1;
        //}
        //else if (grid[WidthIndex + 1][HeightIndex].GetComponent<EmptyObject>())
        //{
        //    var obj = Instantiate(grid[WidthIndex][HeightIndex], grid[WidthIndex + 1][HeightIndex].transform.position, grid[WidthIndex][HeightIndex].transform.rotation);
        //    var obj1 = Instantiate(grid[WidthIndex + 1][HeightIndex], grid[WidthIndex][HeightIndex].transform.position, grid[WidthIndex + 1][HeightIndex].transform.rotation);

        //    Destroy(grid[WidthIndex][HeightIndex]);
        //    Destroy(grid[WidthIndex + 1][HeightIndex]);

        //    grid[WidthIndex][HeightIndex] = obj;
        //    grid[WidthIndex + 1][HeightIndex] = obj1;
        //}
        
        if (WidthIndex< Generator.MaxIndexWidth-1 && grid[WidthIndex+1][HeightIndex ].GetComponent<TileObject>().IsEmpty)
        {
            var o = grid[WidthIndex][HeightIndex];
            Vector3 pos1 = grid[WidthIndex][HeightIndex].transform.position;
            Vector3 pos2 = grid[WidthIndex + 1][HeightIndex].transform.position;
            int width1 = grid[WidthIndex][HeightIndex].GetComponent<TileObject>().WidthIndex;
            int width2 = grid[WidthIndex+1][HeightIndex].GetComponent<TileObject>().WidthIndex;



            print(grid[WidthIndex][HeightIndex].transform.position);
            grid[WidthIndex][HeightIndex] = grid[WidthIndex+1][HeightIndex ];
            print(grid[WidthIndex][HeightIndex].transform.position);

            grid[WidthIndex + 1][HeightIndex] = o;
            //grid[WidthIndex + 1][HeightIndex].transform.position = pos2;
            //grid[WidthIndex][HeightIndex].transform.position = pos1;\

            StartCoroutine(SlowShift(grid[WidthIndex + 1][HeightIndex], grid[WidthIndex ][HeightIndex], pos1,pos2));

            grid[WidthIndex+1][HeightIndex ].GetComponent<TileObject>().WidthIndex = width2;
            grid[WidthIndex][HeightIndex].GetComponent<TileObject>().WidthIndex = width1;




        }
        else if (WidthIndex>0&& grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().IsEmpty)
        {
            var o = grid[WidthIndex][HeightIndex];
            Vector3 pos1 = grid[WidthIndex][HeightIndex].transform.position;
            Vector3 pos2 = grid[WidthIndex-1][HeightIndex ].transform.position;
            int width1 = grid[WidthIndex][HeightIndex].GetComponent<TileObject>().WidthIndex;
            int width2 = grid[WidthIndex - 1][HeightIndex].GetComponent<TileObject>().WidthIndex;

            print(grid[WidthIndex][HeightIndex].transform.position);
            grid[WidthIndex][HeightIndex] = grid[WidthIndex-1][HeightIndex ];
            print(grid[WidthIndex][HeightIndex].transform.position);

            grid[WidthIndex-1][HeightIndex] = o;
            //grid[WidthIndex-1][HeightIndex].transform.position = pos2;
            //grid[WidthIndex][HeightIndex].transform.position = pos1;
            StartCoroutine(SlowShift(grid[WidthIndex - 1][HeightIndex], grid[WidthIndex][HeightIndex], pos1, pos2));

            grid[WidthIndex-1][HeightIndex].GetComponent<TileObject>().WidthIndex = width2;
            grid[WidthIndex][HeightIndex].GetComponent<TileObject>().WidthIndex = width1;


        }
        else if (HeightIndex < Generator.MaxIndexHeight - 1 && grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().IsEmpty)
        {
            //print("lol");
            //Vector3 pos1 = grid[WidthIndex][HeightIndex].transform.position;
            //grid[WidthIndex][HeightIndex].transform.position = grid[WidthIndex][HeightIndex + 1].transform.position;
            //grid[WidthIndex][HeightIndex + 1].transform.position = pos1;


            //grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex = grid[WidthIndex ][HeightIndex+1].GetComponent<TileObject>().HeightIndex;
            //grid[WidthIndex ][HeightIndex+1].GetComponent<TileObject>().HeightIndex = tempH;

            //grid[WidthIndex][HeightIndex+1].GetComponent<TileObject>().IsEmpty = true;
            //grid[WidthIndex][HeightIndex ].GetComponent<TileObject>().IsEmpty = false;


            var o = grid[WidthIndex][HeightIndex];
            Vector3 pos1 = grid[WidthIndex][HeightIndex].transform.position;
            Vector3 pos2 = grid[WidthIndex][HeightIndex  + 1].transform.position;

            int width1 = grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex;
            int width2 = grid[WidthIndex][HeightIndex+1].GetComponent<TileObject>().HeightIndex;


            print(grid[WidthIndex][HeightIndex].transform.position);
            grid[WidthIndex][HeightIndex] = grid[WidthIndex][HeightIndex + 1];
            print(grid[WidthIndex][HeightIndex].transform.position);

            grid[WidthIndex][HeightIndex + 1] = o;
            //grid[WidthIndex][HeightIndex + 1].transform.position = pos2;
            //grid[WidthIndex][HeightIndex].transform.position = pos1;
            StartCoroutine(SlowShift(grid[WidthIndex][HeightIndex+1], grid[WidthIndex][HeightIndex], pos1, pos2));

            grid[WidthIndex][HeightIndex + 1].GetComponent<TileObject>().HeightIndex = width2;
            grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex = width1;


        }
        else if (HeightIndex>0 && grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().IsEmpty)
        {
            //var obj = Instantiate(grid[WidthIndex][HeightIndex], grid[WidthIndex][HeightIndex - 1].transform.position, grid[WidthIndex][HeightIndex].transform.rotation);
            //var obj1 = Instantiate(grid[WidthIndex][HeightIndex - 1], grid[WidthIndex][HeightIndex].transform.position, grid[WidthIndex][HeightIndex - 1].transform.rotation);

            //Destroy(grid[WidthIndex][HeightIndex]);
            //Destroy(grid[WidthIndex][HeightIndex - 1]);

            //grid[WidthIndex][HeightIndex] = obj;
            //grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex--;
            //grid[WidthIndex][HeightIndex - 1] = obj1;
            //grid[WidthIndex][HeightIndex - 1].GetComponent<EmptyObject>().HeightIndex++;
            print("ke");

            //Vector3 pos1 = grid[WidthIndex][HeightIndex].transform.position;
            //grid[WidthIndex][HeightIndex ].transform.position = grid[WidthIndex][HeightIndex-1].transform.position;
            //grid[WidthIndex][HeightIndex - 1].transform.position = pos1;
            //grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex = grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().HeightIndex;
            //grid[WidthIndex][HeightIndex -1].GetComponent<TileObject>().HeightIndex = tempH;

            //grid[WidthIndex][HeightIndex-1].GetComponent<TileObject>().IsEmpty = false;
            //grid[WidthIndex][HeightIndex].GetComponent<TileObject>().IsEmpty = true;

            var o = grid[WidthIndex][HeightIndex];
            Vector3 pos1 = grid[WidthIndex][HeightIndex].transform.position;
            Vector3 pos2 = grid[WidthIndex][HeightIndex-1].transform.position;

            int width1 = grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex;
            int width2 = grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().HeightIndex;

            print(grid[WidthIndex][HeightIndex].transform.position);
            grid[WidthIndex][HeightIndex] = grid[WidthIndex][HeightIndex - 1];
            print(grid[WidthIndex][HeightIndex].transform.position);

            grid[WidthIndex][HeightIndex - 1] = o;
            //grid[WidthIndex][HeightIndex - 1].transform.position = pos2;
            //grid[WidthIndex][HeightIndex ].transform.position = pos1;
            StartCoroutine(SlowShift(grid[WidthIndex][HeightIndex - 1], grid[WidthIndex][HeightIndex], pos1, pos2));


            grid[WidthIndex][HeightIndex - 1].GetComponent<TileObject>().HeightIndex = width2;
            grid[WidthIndex][HeightIndex].GetComponent<TileObject>().HeightIndex = width1;



        }

    }
    // Update is called once per frame
    void Update () {
		
	}
}
