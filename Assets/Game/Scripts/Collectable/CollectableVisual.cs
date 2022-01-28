using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableVisual : MonoBehaviour
{
    [SerializeField] private Animator collectableAnimator;

    private void OnEnable()
    {
        Observer.PlayerStartBattle += BattleAnimation;
    }

    private void OnDisable()
    {
        Observer.PlayerStartBattle -= BattleAnimation;
    }

    private void Start()
    {
        SetRandomIdle();
    }

    public void StackAnimation()
    {
        this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast);
        ChangeAnimState("IsWalking", true);
    }

    public void SetBattleResult(bool IsWin)
    {
        if (IsWin)
        {
            WinAnimation();
        }
        else
        {
            LoseAnimation();
        }

    }

    private void ChangeAnimState(string name, bool value)
    {
        collectableAnimator.SetBool(name, value);
    }

    private void SetRandomIdle()
    {
        var rand = Random.Range(0, 4);
        collectableAnimator.SetFloat("IdleType", rand);
    }

    private void BattleAnimation()
    {
        this.transform.DOLocalRotate(Vector3.up * 180f, 1f, RotateMode.Fast).SetEase(Ease.OutSine);
        ChangeAnimState("IsBattleState", true);
    }

    private void WinAnimation()
    {
        collectableAnimator.SetFloat("BattleResult", 1);
    }

    private void LoseAnimation()
    {
        this.transform.DOLocalRotate(Vector3.up * 360f, 1f, RotateMode.Fast).SetEase(Ease.OutSine);
        collectableAnimator.SetFloat("BattleResult", 2);
    }
}
