using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableMale : Collectable
{
    [SerializeField] private CollectableParticle maleParticle;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CollectableVisual.StackAnimation();
            CollectableParticle.UpdateParticle(ParticleType.LOVE);
        }
    }
}
