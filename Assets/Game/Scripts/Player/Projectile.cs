using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public bool IsShooting = false;
    private float projectileSpeed => SettingsManager.WeaponSettings.projectileSpeed;
    private float maxDistance => SettingsManager.WeaponSettings.projectileMaxTravelDistance;
    private Coroutine shootingRoutine;
    private void OnEnable()
    {
        shootingRoutine = StartCoroutine(LifeTimeRoutine());
        Observer.StopBattle += StopBattle;
    }
    private void OnDisable()
    {
        StopCoroutine(shootingRoutine);
        IsShooting = false;
    }
    void Update()
    {
        if (IsShooting)
        {
            transform.position += Time.deltaTime * projectileSpeed * Vector3.forward;
        }
    }

    private void StopBattle()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator LifeTimeRoutine()
    {
        yield return new WaitForSeconds(maxDistance / projectileSpeed);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsShooting && other.CompareTag("Collectable"))
        {
            var collectable = other.attachedRigidbody.gameObject.GetComponent<Collectable>();
            if (collectable.IsCollected)
            {
                gameObject.SetActive(false);
                return;
            }
            //var newPos = Vector3.zero;
            Vector3 newPos = GameManager.Instance.GetAPointFromActiveArena();
            
            collectable.ShotWithProjectile(newPos);
            
            gameObject.SetActive(false);
        }
    }
}
