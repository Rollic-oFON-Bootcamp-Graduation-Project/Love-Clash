using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMale : Collectable
{
    [SerializeField] private CapsuleCollider maleCollider;
    [SerializeField] private ParticleSystem maleParticle;
    [SerializeField] private CollectableVisual maleVisual;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            maleParticle.Play();
            maleVisual.StackAnimation();
        }
    }


}
