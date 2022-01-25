using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class Gate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;

    [MinValue(1), MaxValue(10)]
    [SerializeField] private int GateMaxValue;
    [SerializeField] private int gateValue;


    private void Start()
    {
        SetGateText();
    }

    private void SetGateText()
    {
        gateValue = Random.Range(1, GateMaxValue);
        textUI.SetText(gateValue.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Upgrade or downgrade weapon
            Observer.WeaponUpdate?.Invoke(gateValue);
            Observer.PlayerUpdate?.Invoke();
        }
    }
}