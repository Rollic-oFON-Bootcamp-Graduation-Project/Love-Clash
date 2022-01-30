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

    private void OnEnable()
    {
        Observer.ArenaSetPositions += PlayPreBattleState;
    }

    private void OnDisable()
    {
        Observer.ArenaSetPositions -= PlayPreBattleState;
    }

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
    }

    public void ChangeAnimState(string name, int value)
    {
        var lowerBodyLayerWeight = GameManager.Instance.CurrentGameState == GameState.BATTLE ? 1 : 0;
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("LowerBody"), lowerBodyLayerWeight);
        playerAnimator.SetFloat(name, value);
    }

    private void PlayPreBattleState()
    {
        playerAnimator.SetTrigger("PreBattle");
    }

    public void PlayShooting()
    {
        playerAnimator.SetBool("Shooting", true);
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("LowerBody"), 1);
        playerAnimator.SetFloat("SideDirection", InputManager.Instance.RawMouseInput.normalized.x);
    }

    public void GateAnimation()
    {
        transform.DOLocalRotate(new Vector3(0, 360, 0), 1f, RotateMode.FastBeyond360);
    }

    public void StopShooting()
    {
        playerAnimator.SetLayerWeight(playerAnimator.GetLayerIndex("LowerBody"), 0);
        ChangeAnimState("Shooting", false);
        ChangeAnimState("Walking", true);
    }
    public void UpgradeVisual()
    {
        visualIndex++;
        currentVisual.SetActive(false);
        currentVisual = playerVisuals[visualIndex];
        playerAnimator.avatar = avatars[visualIndex];
        currentVisual.SetActive(true);
        weapon.transform.position = rightHands[visualIndex].position;
        weapon.transform.SetParent(rightHands[visualIndex]);

        ChangeAnimState("WalkType", visualIndex);
        GateAnimation();
    }

    public void DowngradeVisual()
    {
        visualIndex--;
        visualIndex = (visualIndex < 0) ? 0 : visualIndex;

        currentVisual.SetActive(false);
        currentVisual = playerVisuals[visualIndex];
        playerAnimator.avatar = avatars[visualIndex];
        currentVisual.SetActive(true);
        weapon.transform.position = rightHands[visualIndex].position;
        weapon.transform.SetParent(rightHands[visualIndex]);

        ChangeAnimState("WalkType", visualIndex);
        GateAnimation();
    }

}
