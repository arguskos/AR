using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class OnGridSellection : ScriptableObject
{
    public virtual void Awake()
    {
    }

    protected MainGridDrawer MainGridDrawer;
    public abstract IEnumerator SequenceCourutine(MonoBehaviour runner);
}
