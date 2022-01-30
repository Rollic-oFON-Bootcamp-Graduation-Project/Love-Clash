using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour
{
    [SerializeField] public CollectableVisual CollectableVisual;
    [SerializeField] public CollectableParticle CollectableParticle;
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

    public void AddToStack()
    {
        if (IsCollected) return; 
        IsCollected = true;
        Observer.StartBattle += CollectableVisual.PlayBattle;
        Observer.StopBattle += CollectableVisual.StopBattle;
        CollectableVisual.StackAnimation();
        CollectableParticle.UpdateParticle(ParticleType.LOVE);
        Observer.AddToStack?.Invoke(this);
    }

    public void ShotWithProjectile(Vector3 newPos)
    {
        transform.position = newPos;
        CollectableVisual.UpdateAnimState(MaleAnimState.WIN);
        AddToStack();
        Observer.RemoveFromArena?.Invoke(this);
        
    }

    public void TakenByEnemy(HitType type)
    {
        IsCollected = true;
        switch (type)
        {
            case HitType.OBSTACLE:
                CollectableVisual.UpdateAnimState(MaleAnimState.TAKEN, type);
                CollectableParticle.UpdateParticle(ParticleType.HATE);
                //SET DEAD ANIMATION
                break;
            case HitType.ARENA:
                CollectableVisual.UpdateAnimState(MaleAnimState.TAKEN, type);
                CollectableParticle.UpdateParticle(ParticleType.HATE);
                break;
            default:
                break;
        }

        Observer.StartBattle -= CollectableVisual.PlayBattle;
        Observer.StopBattle -= CollectableVisual.StopBattle;

    }


    //HANDLE WHEN COLLECTABLE IS HIT WITH A PROJECTILE
    public void ProjectileHit(Vector3 position)
    {

    }
}
