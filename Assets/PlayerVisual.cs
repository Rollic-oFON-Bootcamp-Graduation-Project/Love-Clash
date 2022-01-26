using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Avatar[] avatars;
    [SerializeField] private GameObject[] playerVisuals;
    [SerializeField] private Transform[] rightHands;
    [SerializeField] private GameObject currentVisual;
    [SerializeField] private Weapon weapon;

    [SerializeField] private int visualIndex = 0;

    private void Start()
    {
        weapon.transform.position = rightHands[visualIndex].position;
        weapon.transform.SetParent(rightHands[visualIndex]);
    }

    public void EnableVisual()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableVisual()
    {
        this.gameObject.SetActive(false);
    }

    public void ChangeAnimState(string name, bool value)
    {
        playerAnimator.SetBool(name, value);

        if (name == "Shooting") StartCoroutine(ShootingState());
    }

    public void ShootingAnim()
    {
        playerAnimator.SetBool("Shooting", true);
        Debug.Log(InputManager.Instance.RawMouseInput.normalized.x);
        playerAnimator.SetFloat("SideDirection", InputManager.Instance.RawMouseInput.normalized.x);
    }


    public void ChangeAnimState(string name, int value)
    {
        playerAnimator.SetFloat(name, value);
    }

    public void UpgradeAnimation()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360);
    }
    IEnumerator ShootingState()
    {
        // TODO : When battle finish we need to stop this coroutine 
        // while(GameManager.Instance.currentGameState == GameState.BATTLE) is not working!!

        if (InputManager.Instance.RawMouseInput.x != 0)
        {
            if (InputManager.Instance.RawMouseInput.x < 0)
            {
                playerAnimator.SetFloat("SideDirection", 0);
                transform.DOLocalRotate(new Vector3(0, 30f, 0), 0.5f, RotateMode.Fast);
            }
            else
            {
                playerAnimator.SetFloat("SideDirection", 1);
                transform.DOLocalRotate(new Vector3(0, -30f, 0), 0.5f, RotateMode.Fast);

            }
        }

        yield return null;
    }

    public void ChangeVisual()
    {
        visualIndex++;
        currentVisual.SetActive(false);
        currentVisual = playerVisuals[visualIndex];
        playerAnimator.avatar = avatars[visualIndex];
        currentVisual.SetActive(true);
        weapon.transform.position = rightHands[visualIndex].position;
        weapon.transform.SetParent(rightHands[visualIndex]);

        ChangeAnimState("WalkType", visualIndex);
    }

}
