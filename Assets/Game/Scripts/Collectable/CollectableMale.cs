using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMale : Collectable
{
    [SerializeField] private ParticleSystem maleParticle;

   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            maleParticle.Play();
            CollectableVisual.StackAnimation();
        }
    }
}
