using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class VisualManager : MonoBehaviour {

    public GameObject[] PuzzlePieces;
    public List<GameObject> Spheres = new List<GameObject>();
    public GameObject LineRendererTemplate;
    private int _maxAmountOfConnections = 75;

    [Range(0.0f, 10.0f)]
    public float SuspenseValue = 5.0f;
    private int _currentConnections;
    private float _maxLineRenderDistance = 100.0f;
    public List<GameObject> _lineRendererPool = new List<GameObject>();

    //Constants
    public readonly float BreathingSlowScale = 0.085f;
    public readonly float BreathingSlowSpeed = 0.75f;
    public readonly float BreathingPunchScale = 0.7f;
    public readonly float BreathingPunchSpeed = 1.25f;
    public readonly float BreathingFastPunchScale = 0.1f;
    public readonly float BreathingFastPunchSpeed = 2.0f;

    // Use this for initialization
    void Start () {
        PuzzlePieces = GameObject.FindGameObjectsWithTag("ARPiece");
        _maxAmountOfConnections = PuzzlePieces.Length;
        GenerateLineRendererPool(_maxAmountOfConnections+1, LineRendererTemplate);

        for (int i = 0; i < PuzzlePieces.Length; i++)
        {
            for (int j = 0; j < PuzzlePieces[i].GetComponent<PuzzlePieceVisuals>().SphereMesh.Length; j++)
            {
                Spheres.Add(PuzzlePieces[i].GetComponent<PuzzlePieceVisuals>().SphereMesh[j]);
            }
        }
        StartCoroutine(ShuffleList());
        StartCoroutine(InitializeConnections());
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    IEnumerator InitializeConnections()
    {
        while (_currentConnections < _maxAmountOfConnections)
        {
            MakeNewConnection();
            yield return new WaitForSeconds(0.025f);
        }
    }

    public GameObject RequestNewConnectionObject(GameObject currentPrimaryConnector)
    {
            GameObject bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            Vector3 currentPosition = currentPrimaryConnector.transform.position;
            foreach (GameObject potentialTarget in Spheres)
            {
                Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    if (potentialTarget.transform.parent.transform.parent.gameObject != currentPrimaryConnector)
                    {
                        if (dSqrToTarget < 2.0f)
                        {
                            bestTarget = potentialTarget;
                            break;
                        }
                        closestDistanceSqr = dSqrToTarget;
                        bestTarget = potentialTarget;
                    }

                }
            }

            return bestTarget;
    }

    public void DeactivateLineRenderer(GameObject obj)
    {
        _lineRendererPool.Add(obj);
        obj.SetActive(false);
    }

    public GameObject ActivateLineRenderer()
    {
        if (_lineRendererPool.Count > 0)
        {
            GameObject obj = null;
            for (int i = 0; i < _lineRendererPool.Count; i++)
            {
                if (_lineRendererPool[i] != null)
                {
                    obj = _lineRendererPool[i];
                    _lineRendererPool.RemoveAt(i);
                    break;
                }

            }
            obj.SetActive(true);
            LineRenderer lr = obj.GetComponent<LineRenderer>();
            lr.enabled = true;
            lr.useWorldSpace = true;
            lr.transform.position = transform.position;
            Vector3 root = new Vector3(0, 0, 0);
            lr.SetPosition(0, root);
            lr.SetPosition(1, root);
            return obj;
        }
        return null;
    }

    void GenerateLineRendererPool(int size, GameObject prefab)
    {
        for (int i = 0; i < size; i++)
        {
            var obj = Instantiate(prefab, gameObject.transform);
            _lineRendererPool.Add(obj);
        }
    }

    IEnumerator ShuffleList()
    {
        while (true)
        {
            Spheres.Sort((x, y) => Random.value < 0.5f ? -1 : 1);
            yield return new WaitForSeconds(1);
        }
    }


    public void MakeNewConnection()
    {
        GameObject lr;
        lr = ActivateLineRenderer();
        if (lr == null)
        {
            Debug.Log("COULD NOT FIND LR");
        }
        LineRendererVisuals lrv = lr.GetComponent<LineRendererVisuals>();
        int rnd = Random.Range(0, Spheres.Count);
        GameObject firstconnector = Spheres[rnd];
        GameObject secondconnector = RequestNewConnectionObject(firstconnector);

        lrv.ConnectionPointA = firstconnector;
        lrv.ConnectionPointB = secondconnector;
        _currentConnections++;
   }

    public void DecreaseConnections(int amounttodecrease)
    {
        _currentConnections -= amounttodecrease;
    }
}
