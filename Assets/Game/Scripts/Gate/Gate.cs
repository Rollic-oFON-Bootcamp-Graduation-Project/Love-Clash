using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class Gate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private GateType gateType;
    [SerializeField] private int gateValue;


    [MinValue(0), MaxValue(10)]
    [SerializeField] private int GateMaxValue;

    private void Start()
    {
        SetGateText();
    }

    private void SetGateText()
    {
        int randValue = Random.Range(0, 10);
        gateValue = (gateType != GateType.DOWNGRADE) ? randValue : -randValue;

        textUI.SetText(gateValue.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Upgrade or downgrade weapon
            Observer.WeaponUpdate?.Invoke(gateType, gateValue);
        }
    }
}