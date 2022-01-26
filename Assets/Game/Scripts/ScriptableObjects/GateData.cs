using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Gate", menuName = "ScriptableObjects/GateData", order = 1)]
public class GateData : ScriptableObject
{
    public Sprite[] UpgradeSprites;
    public Sprite[] DowngradeSprites;
}
