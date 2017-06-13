using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Camera shift and grid selection", menuName = "OnGridClickEvents/Camera shift and grid selection", order = 1)]
public class CameraShiftAndSelection : SimpleRemoveCollider
{
    private GameObject _target;

    public override void Awake()
    {
        base.Awake();
        _target = GameObject.FindWithTag("ImageTarget");
    }

    public void OnEnable()
    {
        _target = GameObject.FindWithTag("ImageTarget");
    }

    public override IEnumerator SequenceCourutine(MonoBehaviour runner)
    {
        CamerasManager.Instance.Switching();
        return MainGridDrawer.Instance.SelectGrid();

    }
}
