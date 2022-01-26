using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

public class Gate : MonoBehaviour
{
    [SerializeField] private Image gateImage;
    [SerializeField] GateData gateData;
    [SerializeField] GateType gateType;

    private void Start()
    {
        SetGateSprite();
    }

    private void SetGateSprite()
    {
        var sprites = (gateType == GateType.UPGRADE) ? gateData.UpgradeSprites : gateData.DowngradeSprites;
        var randIndex = Random.Range(0, sprites.Length);
        gateImage.sprite = sprites[randIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Upgrade or downgrade weapon
            var value = (gateType == GateType.UPGRADE) ? 1 : -1;
            Observer.WeaponUpdate?.Invoke(value);
            Observer.PlayerUpdate?.Invoke(value);
        }
    }
}