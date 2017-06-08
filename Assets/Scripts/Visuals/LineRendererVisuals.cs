using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineRendererVisuals : MonoBehaviour
{
    public Vector3[] Pieces = new Vector3[2];
    public float LifeTime = 5.0f;

    public GameObject ConnectionPointA;
    public GameObject ConnectionPointB;

    public AnimationCurve LifeTimeTint;

    private LineRenderer _lineRenderer;
    private VisualManager _visualManager;
    private float _timeParam;
    private float _width = 0.05f;
    private float _offset;
    private Color _origTint;

    // Use this for initialization
    void Start()
    {
        _offset = Random.Range(0, LifeTime / 4);
        _lineRenderer = transform.GetComponent<LineRenderer>();
        _lineRenderer.startWidth = _width/2;
        _lineRenderer.endWidth = _width;
        _origTint = _lineRenderer.material.GetColor("_TintColor");
        GetVisualManager();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLines();
    }

    void ResetParameters()
    {
        _offset = Random.Range(0, LifeTime / 4);
        _timeParam = 0;
}

    void LateUpdate()
    {
        _lineRenderer.SetPosition(0, ConnectionPointA.transform.position);
        _lineRenderer.SetPosition(1, ConnectionPointB.transform.position);
    }

    public void GetVisualManager()
    {
        _visualManager = GameObject.FindGameObjectWithTag("VisualManager").GetComponent<VisualManager>();
    }

    public void UpdateLines()
    {
        _timeParam += Time.deltaTime/8;

        float newtint =  _origTint.a * LifeTimeTint.Evaluate(_timeParam / (LifeTime + _offset));
        _lineRenderer.material.SetColor("_TintColor", new Color(1, 1, 1, newtint));

        if (_timeParam >= (LifeTime + _offset))
        {
            _visualManager.MakeNewConnection();
            _visualManager.DecreaseConnections(1);
            ResetParameters();
            _visualManager.DeactivateLineRenderer(gameObject);
        }
    }
}