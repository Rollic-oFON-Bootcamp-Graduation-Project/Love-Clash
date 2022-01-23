using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float fireRate;
    [SerializeField] private int weaponLevel = 0;
    [SerializeField] private float weaponDamage = 20;

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

    private void UpdateWeapon(GateType gateType, int gateValue)
    {
        switch (gateType)
        {
            case GateType.UPGRADE:
                UpgradeWeapon(gateValue);
                break;
            case GateType.DOWNGRADE:
                DowngradeWeapon(gateValue);
                break;
        }
        ClampWeaponUpgrade(weaponLevel, fireRate, weaponDamage);
        Debug.Log($"WeaponLevel{weaponLevel}, FireRate{fireRate}, WeaponDamage{weaponDamage}");
    }

    private void UpgradeWeapon(int value)
    {
        // TODO
        // change the multiply value (0.5f)
        weaponLevel++;
        fireRate += value * 0.5f;
        weaponDamage += value * 0.5f;
    }

    private void DowngradeWeapon(int value)
    {
        // TODO
        // change the multiply value (0.5f)
        weaponLevel--;
        fireRate -= value * 0.5f;
        weaponDamage -= value * 0.5f;
    }

    private void ClampWeaponUpgrade(int level, float rate, float damage)
    {
        weaponLevel = Mathf.Clamp(weaponLevel, 0, maxWeaponLevel);
        fireRate = Mathf.Clamp(fireRate, 0, maxFireRate);
        weaponDamage = Mathf.Clamp(weaponDamage, 0, maxWeaponDamage);
    }
}
