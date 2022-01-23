using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NaughtyAttributes;

public class Gate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI;
    [SerializeField] private GateType gateType;

    [MinValue(0), MaxValue(10)]
    [SerializeField] private int GateMaxValue;

    private void Start()
    {
        SetGateText();
    }

    private void SetGateText()
    {
        int randValue = Random.Range(0, 10);
        var gateText = (gateType != GateType.DOWNGRADE) ? randValue : -randValue;

        textUI.SetText(gateText.ToString());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // TODO
            // Upgrade or downgrade weapon
        }
    }
}