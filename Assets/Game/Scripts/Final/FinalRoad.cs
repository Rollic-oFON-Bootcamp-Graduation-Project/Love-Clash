using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalRoad : MonoBehaviour
{
    [SerializeField] FinalPathGenerator finalPathGenerator;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Final");
            //SET STACK COLLECTABLES TO FINAL GAME POSITIONS.
        }
    }
}
