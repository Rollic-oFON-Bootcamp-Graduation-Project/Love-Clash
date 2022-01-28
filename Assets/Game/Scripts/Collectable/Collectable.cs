using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public CollectableVisual CollectableVisual;
    public bool IsCollected = false;

    //HANDLE WHEN COLLECTABLE IS HIT WITH A PROJECTILE
    public void ProjectileHit(Vector3 position)
    {

    }
}
