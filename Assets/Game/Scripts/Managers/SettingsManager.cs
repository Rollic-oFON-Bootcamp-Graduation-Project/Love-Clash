using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] GameSettings gameSettings;
    public static GameSettings GameSettings => Instance.gameSettings;
    private static SettingsManager instance;
    public static SettingsManager Instance => instance ?? (instance = FindObjectOfType<SettingsManager>());

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }
}