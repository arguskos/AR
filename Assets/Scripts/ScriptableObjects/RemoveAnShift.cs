using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RemoveColliderAndShift", menuName = "OnGridClickEvents/RemoveColliderAndShift", order = 1)]

public class RemoveAnShift : SimpleRemoveCollider
{
    private  GameObject _target;

    public  override void Awake()
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
        return MainGridDrawer.Instance.SelectGrid();

    }
}
