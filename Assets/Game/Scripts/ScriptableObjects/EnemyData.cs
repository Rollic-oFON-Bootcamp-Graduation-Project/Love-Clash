using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/New Enemy", order = 1)]
public class EnemyData : ScriptableObject
{
    public float enemyRange;
    public Material enemyMaterial;
    //We can change it to enemy models or add new things if we need it
}
