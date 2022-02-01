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
        if (transform.parent != null) transform.parent = null;
        transform.position = newPos;
        CollectableVisual.UpdateAnimState(MaleAnimState.WIN);
        AddToStack();
        Observer.RemoveFromArena?.Invoke(this);
        
    }

    private void HandleObstacleHit(Vector3 hitPoint)
    {
        var direction = new Vector3(-Random.value, 0, Random.value);

        var newPos = hitPoint + direction;
        newPos.x = Mathf.Clamp(newPos.x ,SettingsManager.ArenaLeftLimitX, SettingsManager.ArenaRightLimitX);
    }

    public void TakenByEnemy(HitType type, Vector3? position = null)
    {
        position ??= Vector3.zero; 
        IsCollected = true;
        switch (type)
        {
            case HitType.OBSTACLE:
                CollectableVisual.UpdateAnimState(MaleAnimState.TAKEN, type);
                CollectableParticle.UpdateParticle(ParticleType.HATE);
                HandleObstacleHit((Vector3)position);
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
