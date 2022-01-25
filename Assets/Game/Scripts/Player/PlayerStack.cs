using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField, ReadOnly] private List<Collectable> stack;
    private float stackGap => SettingsManager.StackGap;


    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddToStack()
    {

    }
}
