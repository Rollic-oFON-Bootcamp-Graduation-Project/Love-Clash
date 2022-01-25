using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SettingsManager : MonoSingleton<SettingsManager>
{

    [SerializeField, Expandable] GameSettings gameSettings;
    public static GameSettings GameSettings => Instance.gameSettings;
    
    [SerializeField, Expandable] WeaponSettings weaponSettings;
    public static WeaponSettings WeaponSettings => Instance.weaponSettings;

    [SerializeField, Range(0,10)]
    private float stackGap;
    public static float StackGap => Instance.stackGap;
}
