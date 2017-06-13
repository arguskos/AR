using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Propertie", menuName = "GridPropertie", order = 1)]
[System.Serializable]
public class GridProperties : ScriptableObject
{
    public float Scale;
    public float DistanceFromCenter;
}
