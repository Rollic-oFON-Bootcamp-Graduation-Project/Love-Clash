using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform leftLimit, rightLimit, sideMovementRoot;
    [SerializeField] private Weapon weapon;
    [SerializeField] private PlayerStack stack;

    [SerializeField] private PlayerVisual playerVisual;

    private float oldLeftLimitX, oldRightLimitX;
    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;
    private float forwardSpeed => SettingsManager.GameSettings.forwardSpeed;
    private float sideMovementSensivity => SettingsManager.GameSettings.sideMovementSensivity;

    private void Start()
    {
        oldLeftLimitX = leftLimitX;
        oldRightLimitX = rightLimitX;
    }
    private void OnEnable()
    {
        Observer.PlayerUpdate += HandlePlayerVisual;
        //Observer.PlayerAnimationChange += HandlePlayerAnimation;
        //Observer.UpdatePlayerLimit += UpdatePlayerLimit;
        //Observer.PlayerStartBattle += HandleBattle;
        Observer.StopBattle += StopBattle;
        Observer.StartBattle += StartBattle;
        Observer.StartGame += StartGame;
    }
    private void OnDisable()
    {
        Observer.PlayerUpdate -= HandlePlayerVisual;
        //Observer.PlayerAnimationChange -= HandlePlayerAnimation;
        //Observer.UpdatePlayerLimit += UpdatePlayerLimit;
        //Observer.PlayerStartBattle -= HandleBattle;
        Observer.StopBattle -= StopBattle;
        Observer.StartBattle -= StartBattle;
        Observer.StartGame -= StartGame;
    }

    // Update is called once per frame
    void Update()
    {
        HandleForwardMovement();
        HandleSideMovement();
    }

    private void StartGame()
    {
        HandlePlayerAnimation();
    }

    private void StartBattle()
    {
        HandleBattle();
        UpdatePlayerLimit(SettingsManager.ArenaLeftLimitX, SettingsManager.ArenaRightLimitX);
    }

    private void HandleBattle()
    {
        if (GameManager.Instance.CurrentGameState != GameState.BATTLE) return;
        HandlePlayerAnimation();
        weapon.StartShooting();
    }

    private void StopBattle()
    {
        UpdatePlayerLimit(oldLeftLimitX, oldRightLimitX);
        playerVisual.StopShooting();
        weapon.StopShooting();
    }

    private void HandleForwardMovement()
    {
        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        transform.position += Vector3.forward * (forwardSpeed * Time.deltaTime);
    }

    private void HandleSideMovement()
    {
        var pos = sideMovementRoot.localPosition;
        pos.x += InputManager.Instance.MouseInput.x * sideMovementSensivity;
        pos.x = Mathf.Clamp(pos.x, leftLimitX, rightLimitX);
        sideMovementRoot.localPosition = Vector3.Lerp(sideMovementRoot.localPosition, pos, Time.deltaTime * 20f);

        if (GameManager.Instance.CurrentGameState != GameState.GAMEPLAY) return;
        var moveDirection = Vector3.forward + InputManager.Instance.RawMouseInput.x * Vector3.right;
        var targetRotation = pos.x == leftLimitX || pos.x == rightLimitX ? Quaternion.LookRotation(Vector3.forward, Vector3.up) : Quaternion.LookRotation(moveDirection.normalized, Vector3.up);
        sideMovementRoot.localRotation = Quaternion.Lerp(sideMovementRoot.localRotation, targetRotation, Time.deltaTime * 5f);
    }

    private void HandlePlayerAnimation()
    {
        if (GameManager.Instance.CurrentGameState == GameState.GAMEPLAY)
        {
            playerVisual.ChangeAnimState("Walking", true);
        }
        else if (GameManager.Instance.CurrentGameState == GameState.BATTLE)
        {
            playerVisual.PlayShooting();
        }
    }


    private void UpdatePlayerLimit(float left, float right)
    {
        var tempLeftLimit = leftLimit.localPosition;
        var tempRightLimit = rightLimit.localPosition;

        tempLeftLimit.x = left;
        tempRightLimit.x = right;

        leftLimit.localPosition = tempLeftLimit;
        rightLimit.localPosition = tempRightLimit;
    }

    private void HandleObstacleHit()
    {
        //PLAY PARTICLEC, ANIMATONS ETC

        transform.DOMove(Vector3.back * 5f, 1f)
            .SetEase(Ease.OutExpo)
            .SetRelative();
    }



    private void HandlePlayerVisual(int value)
    {
        if (value > 0)
        {
            playerVisual.UpgradeVisual();
        }
        else
        {
            playerVisual.DowngradeVisual();

        }
        HandlePlayerAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable") && GameManager.Instance.CurrentGameState == GameState.GAMEPLAY)
        {
            var myCollectable = other.attachedRigidbody.gameObject.GetComponent<Collectable>();
            myCollectable.AddToStack();
        } 
        else if (other.CompareTag("Obstacle"))
        {
            var myCollectable = Observer.RemoveFromStack?.Invoke();
            myCollectable.TakenByEnemy(HitType.OBSTACLE);
            HandleObstacleHit();
        }
    }
}
