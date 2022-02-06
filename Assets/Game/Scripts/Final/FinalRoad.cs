using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoad : MonoBehaviour
{
    [SerializeField] FinalPathGenerator finalPathGenerator;
    private bool isTriggered = false;
    private void OnEnable()
    {
        Observer.PreFinal += SetCollectablePositions;
    }
    private void OnDisable()
    {
        Observer.PreFinal -= SetCollectablePositions;
    }

    private void SetCollectablePositions()
    {
        Observer.StackHandleFinal?.Invoke(finalPathGenerator.FinalPath);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isTriggered) return;
            isTriggered = true;
            Debug.Log("Final");
            GameManager.Instance.StartFinal();
        }
    }
}
