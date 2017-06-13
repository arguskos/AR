using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RemoveCollider", menuName = "OnGridClickEvents/RemoveCollider", order = 1)]

public class SimpleRemoveCollider : OnGridSellection
{
    public override IEnumerator SequenceCourutine(MonoBehaviour runner)
    {
        Destroy( runner.gameObject.GetComponent<BoxCollider>());
        yield return null;
    }
}
