using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerStack : MonoBehaviour
{
    [SerializeField] private Transform playerSideMovementRoot;
    [SerializeField, ReadOnly] private List<Collectable> stack = new List<Collectable>();
    private float stackGap => SettingsManager.StackGap;
    private Vector3 offset;

    private void OnEnable()
    {
        Observer.AddToStack += AddToStack;
    }
    private void OnDisable()
    {
        Observer.AddToStack -= AddToStack;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateOffSet();
        FollowPlayer();
        HandleStackMovement();
    }
    private void UpdateOffSet()
    {
        offset = Vector3.back * stackGap;
    }
    private void FollowPlayer()
    {
        transform.position = playerSideMovementRoot.position + offset;
    }
    private void HandleStackMovement()
    {
        if (stack.Count == 0) return;
        stack[0].transform.position = Vector3.Lerp(transform.position + offset, playerSideMovementRoot.transform.position, 0.8f);
        for(int i = 1; i < stack.Count; i++)
        {
            stack[i].transform.position = Vector3.Lerp(stack[i - 1].transform.position + offset, stack[i].transform.position, 0.8f);
        }
    }

    private void AddToStack(Collectable collectable)
    {
        stack.Add(collectable);
    }
}
