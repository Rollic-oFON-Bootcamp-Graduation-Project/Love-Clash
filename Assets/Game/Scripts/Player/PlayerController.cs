using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform leftLimit, rightLimit, sideMovementRoot;
    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;
    private float forwardSpeed => SettingsManager.GameSettings.forwardSpeed;
    private float sideMovementSensivity => SettingsManager.GameSettings.sideMovementSensivity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        //Not sure if we trigger battle when it collides or in update
        //HandleBattle();
    }

    private void HandleBattle()
    {
        if (GameManager.Instance.CurrentGameState != GameState.BATTLE) return;
    }

    private void HandleMovement()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        HandleForwardMovement();
        HandleSideMovement();
    }

    private void HandleForwardMovement()
    {
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
    }

    private void HandleSideMovement()
    {
        var pos = sideMovementRoot.localPosition;
        pos.x += InputManager.Instance.MouseInput.x * sideMovementSensivity;
        pos.x = Mathf.Clamp(pos.x, leftLimitX, rightLimitX);
        sideMovementRoot.localPosition = Vector3.Lerp(sideMovementRoot.localPosition, pos, Time.deltaTime*20f);


        var moveDirection = Vector3.forward + InputManager.Instance.RawMouseInput.x*Vector3.right;
        var targetRotation = pos.x == leftLimitX || pos.x == rightLimitX ? Quaternion.LookRotation(Vector3.forward, Vector3.up) : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        sideMovementRoot.localRotation = Quaternion.Lerp(sideMovementRoot.localRotation, targetRotation, Time.deltaTime*5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            var myCollectable = other.attachedRigidbody.gameObject.GetComponent<Collectable>();
            if (myCollectable.IsCollected) return;
            myCollectable.IsCollected = true;
            Observer.AddToStack?.Invoke(myCollectable);
        }
    }
}
