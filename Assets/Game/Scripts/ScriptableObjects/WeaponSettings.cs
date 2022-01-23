using UnityEngine;

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "ScriptableObjects/WeaponSettings", order = 1)]
public class WeaponSettings : ScriptableObject
{
    public int maxWeaponLevel;
    public float maxFireRate;
    public float maxWeaponDamage;

    
}
