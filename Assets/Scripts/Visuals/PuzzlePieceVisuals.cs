using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePieceVisuals : MonoBehaviour {

    [Header("Gameobjects")]
    public GameObject CircleMesh;
    public GameObject BeamMesh;
    public GameObject[] SphereMesh;
    public GameObject ParticlePortalPrefab;
    [Space(10)]

    [Header("Materials")]
    public Material MatActive;
    public Material MatBeam;
    public Material MatBlack;
    public Material MatWhite;
    public Material MatBlocked;

    [Space(10)]

    [Header("Bools")]
    public bool IsCorrect = false;
    [Space(10)]

    private bool _isShaking;
    private float _suspenseValue;
    private float _randomOffset;
    private VisualManager _visualManager;
    private float _linerenderLifetime;
    private int _currentConnections;
    private GameObject _particlePortal;

    // Use this for initialization

    void Awake()
    {
        transform.localScale = new Vector3(MainGridDrawer.Instance.Properties.Scale, MainGridDrawer.Instance.Properties.Scale, MainGridDrawer.Instance.Properties.Scale);
        GetVisualManager();
        RandomizeVariables();
        //LoopRotationAndPositionOffsets();
        _particlePortal = Instantiate(ParticlePortalPrefab, CircleMesh.transform);
        _particlePortal.SetActive(false);
        _particlePortal.transform.localScale =CircleMesh.transform.parent.transform.localScale;

    }
    void Start ()
    {
     
    }
	
	// Update is called once per frame
	void Update ()
    {
        //_suspenseValue = _visualManager.SuspenseValue;
        //if (/*(Input.GetTouch(0).phase == TouchPhase.Began ||*/ (Input.GetKeyDown(KeyCode.Mouse0)) && !_isShaking)
        //{
        //    SetCorrect(IsCorrect);
        //}
    }

    public void SetCorrect(bool iscorrect)
    {
        IsCorrect = iscorrect;
        StartCoroutine(Shake(0.025f * (_suspenseValue), Random.Range(0.25f, 0.5f), CircleMesh));
        if (IsCorrect)
        {
            ParticleSystem ps = _particlePortal.GetComponent<ParticleSystem>();
            _particlePortal.SetActive(true);
            ps.Play();
            ParticleSystem.EmissionModule em = ps.emission;
            em.enabled = true;
            SetMaterials(CircleMesh, MatActive);
            SetMaterials(BeamMesh, MatBeam);

            for (int i = 0; i < SphereMesh.Length; i++)
            {
                SetMaterials(SphereMesh[i], MatActive);
            }
        }
        else
        {
            _particlePortal.SetActive(false);
            SetMaterials(CircleMesh, MatBlack);
            SetMaterials(BeamMesh, MatWhite);
            for (int i = 0; i < SphereMesh.Length; i++)
            {
                SetMaterials(SphereMesh[i], MatWhite);
            }
            SetMaterials(SphereMesh[0], MatWhite);
        }
    }

    void SetMaterials(GameObject rendergameobject, Material material)
    {
        Renderer materialrenderer = rendergameobject.GetComponent<Renderer>();

        Material[] newMaterials = new Material[materialrenderer.materials.Length];
        for (int m = 0; m < newMaterials.Length; m++)
        {
            newMaterials[m] = material;
        }
        materialrenderer.materials = newMaterials;
    }

    void LoopRotationAndPositionOffsets()
    {
        for (int i = 0; i < SphereMesh.Length; i++)
        {
            StartCoroutine(BreathePunch(_visualManager.BreathingPunchScale, _visualManager.BreathingPunchSpeed, SphereMesh[i]));
        }
        StartCoroutine(BreathePunch(_visualManager.BreathingFastPunchScale, _visualManager.BreathingFastPunchSpeed, CircleMesh));
        StartCoroutine(BreatheSlow(_visualManager.BreathingSlowScale, _visualManager.BreathingSlowSpeed, gameObject));
    }

    void RandomizeVariables()
    {
        _randomOffset = Random.Range(0.0f, 1.0f);
    }

    public void GetVisualManager()
    {
        _visualManager = GameObject.FindObjectOfType<VisualManager>();
        _suspenseValue = _visualManager.SuspenseValue;
        _linerenderLifetime = _visualManager.LineRendererTemplate.GetComponent<LineRendererVisuals>().LifeTime;
    }

    IEnumerator BreathePunch(float scaleAmount, float breathingSpeed, GameObject gameObjectToBreathe)
    {
        while (true)
        {
            float breathingSpeedOffset = Mathf.Abs((transform.position.x + (transform.position.z * 1.5f)) / 10) ;
            yield return new WaitForSeconds(breathingSpeedOffset);
            iTween.PunchScale(gameObjectToBreathe, new Vector3(scaleAmount, scaleAmount, scaleAmount), breathingSpeed);
            yield return new WaitForSeconds((breathingSpeed * 2) - breathingSpeedOffset);
        }
    }

    IEnumerator BreatheSlow(float scaleAmount, float breathingSpeed, GameObject gameObjectToBreathe)
    {
        while (true)
        {
            float breathingSpeedOffset = Mathf.Abs((transform.position.x* GetOneOrMinusOne() + (transform.position.z* GetOneOrMinusOne() * 1.5f)) / 10);
            yield return new WaitForSeconds(breathingSpeedOffset);
            iTween.ScaleAdd(gameObjectToBreathe, new Vector3(scaleAmount, scaleAmount, scaleAmount), breathingSpeed - breathingSpeedOffset);
            yield return new WaitForSeconds((breathingSpeed) - breathingSpeedOffset);
            iTween.ScaleTo(gameObjectToBreathe, new Vector3(1, 1, 1), breathingSpeed);
            yield return new WaitForSeconds(breathingSpeed);
        }
    }

    IEnumerator Shake(float shakeAmount, float shakeDuration, GameObject gameObjectToBreathe)
    {
        _isShaking = true;
        iTween.PunchPosition(gameObjectToBreathe, new Vector3(shakeAmount, shakeAmount, shakeAmount), shakeDuration);
        yield return new WaitForSeconds(shakeDuration);
        _isShaking = false;
    }

    private float GetOneOrMinusOne()
    {
        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            return -1.0f;
        }
        else
        {
            return 1.0f;
        }
    }
}
