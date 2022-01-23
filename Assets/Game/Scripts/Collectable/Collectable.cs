using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private int upgradeValue = 2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //UPGRADE WEAPON
            Observer.WeaponUpdate?.Invoke(upgradeValue);
            Destroy(gameObject);
        }
    }
}
