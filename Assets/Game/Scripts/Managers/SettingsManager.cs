using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class SettingsManager : MonoSingleton<SettingsManager>
{

    [SerializeField, Expandable] GameSettings gameSettings;
    public static GameSettings GameSettings => Instance.gameSettings;
}
