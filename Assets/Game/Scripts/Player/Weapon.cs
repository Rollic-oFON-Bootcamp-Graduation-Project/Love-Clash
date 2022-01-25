using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private int weaponLevel = 0;
    [SerializeField] private float weaponDamage = 20;
    [SerializeField] private Transform shootPoint;

    private float maxFireRate => SettingsManager.WeaponSettings.maxFireRate;
    private int maxWeaponLevel => SettingsManager.WeaponSettings.maxWeaponLevel;
    private float maxWeaponDamage => SettingsManager.WeaponSettings.maxWeaponDamage;


    private void OnEnable()
    {
        Observer.WeaponUpdate += UpdateWeapon;
    }
    private void OnDisable()
    {
        Observer.WeaponUpdate -= UpdateWeapon;
    }

    private void UpdateWeapon(int value)
    {
        var level = (value > 0) ? 1 : -1;
        weaponLevel += level;

        fireRate += value * 0.5f;
        weaponDamage += value * 0.5f;

        ClampWeaponValue(weaponLevel, fireRate, weaponDamage);
        Debug.Log($"WeaponLevel{weaponLevel}, FireRate{fireRate}, WeaponDamage{weaponDamage}");

        // TODO
        // Change the projectile settings via updated weapon values
    }

    private void ClampWeaponValue(int level, float rate, float damage)
    {
        weaponLevel = Mathf.Clamp(weaponLevel, 0, maxWeaponLevel);
        fireRate = Mathf.Clamp(fireRate, 0, maxFireRate);
        weaponDamage = Mathf.Clamp(weaponDamage, 0, maxWeaponDamage);
    }
    public void StartShooting()
    {

    }

    private void Shoot()
    {

    }



    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            if (GameManager.Instance.CurrentGameState != GameState.BATTLE) break;
            yield return new WaitForSeconds(1 / fireRate);
            Shoot();
        }
    }
}
