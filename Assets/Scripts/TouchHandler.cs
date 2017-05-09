using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchHandler : MonoBehaviour
{

    // Use this for initialization

    public GameObject[] Tubes;

    public Camera Cam;
    void Start()
    {

    }

    // Update is called once per frame
    public void Clicked(RaycastHit raycastHit)
    {
        //TubePlace place = raycastHit.collider.gameObject.GetComponent<TubePlace>();
        //int spawn = place.State;
        //if (place.Tube)
        //{
        //    Destroy(place.Tube.gameObject);
        //}
        //if (spawn < 4)
        //{
        //    var obj = Instantiate(Tubes[spawn], Vector3.zero, Quaternion.Euler(0, 0, 90));
        //    obj.transform.parent = raycastHit.collider.transform.parent;
        //    obj.transform.localPosition = raycastHit.collider.transform.localPosition;
        //    place.Tube = obj;
        //    raycastHit.collider.gameObject.GetComponent<Renderer>().enabled = false;
        //}
        //else
        //{
        //    raycastHit.collider.gameObject.GetComponent<Renderer>().enabled = true;
        //}
        //place.State++;
        //if (place.State > 4)
        //{
        //    place.State = 0;
        //}


        if (raycastHit.collider.GetComponent<TubeID>())
        {
            TubesGrid.TileGrid[raycastHit.collider.GetComponent<TubeID>().WidthIndex][raycastHit.collider.GetComponent<TubeID>().HeightIndex]
                .Shift();
        }

    }
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            Ray raycast = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit raycastHit;
            if (Physics.Raycast(raycast, out raycastHit))
            {
                Debug.Log("Something Hit");
                if (raycastHit.collider.name == "Soccer")
                {
                    Debug.Log("Soccer Ball clicked");
                }
                Clicked(raycastHit);
                //OR with Tag

                if (raycastHit.collider.CompareTag("SoccerTag"))
                {
                    Debug.Log("Soccer Ball clicked");
                }
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    RaycastHit hit;
        //    Ray ray   = Cam.ScreenPointToRay(Input.mousePosition);
        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        Clicked(hit);

        //    }
        //}
    }
}
