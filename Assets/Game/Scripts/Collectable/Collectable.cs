using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    [SerializeField] public CollectableVisual CollectableVisual;
    [SerializeField] private CapsuleCollider collectableCollider;
    public bool IsCollected = false;
    public void DisableCollider()
    {
        collectableCollider.enabled = false;
    }
    public void EnableCollider()
    {
        collectableCollider.enabled = true;
    }


    //HANDLE WHEN COLLECTABLE IS HIT WITH A PROJECTILE
    public void ProjectileHit(Vector3 position)
    {

    }
}
