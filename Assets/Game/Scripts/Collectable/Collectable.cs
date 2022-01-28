using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] public CollectableVisual CollectableVisual;
    public bool IsCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        //PROJECTILE HIT CONTROL
    }
}
