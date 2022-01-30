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
            Observer.StartBattle += CollectableVisual.PlayBattle;
            Observer.StopBattle += CollectableVisual.StopBattle;
            CollectableVisual.StackAnimation();
            CollectableParticle.UpdateParticle(ParticleType.LOVE);
        }
    }
}
