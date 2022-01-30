using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CollectableVisual : MonoBehaviour
{
    [SerializeField] private Animator collectableAnimator;
    [SerializeField] private MaleAnimState currentState;

    private void OnEnable()
    {
        Observer.PlayerStartBattle += PlayBattle;
        Observer.StopBattle += StopBattle;
    }
    private void OnDisable()
    {
        Observer.PlayerStartBattle -= PlayBattle;
        Observer.StopBattle -= StopBattle;
    }

    public void UpdateAnimState(MaleAnimState newState)
    {
        currentState = newState;
        switch (newState)
        {
            case MaleAnimState.IDLE:
                PlayRandomIdle();
                break;
            case MaleAnimState.WALKING:
                ChangeAnimState("IsWalking", true);
                break;
            case MaleAnimState.ONBATTLE:
                ChangeAnimState("IsBattleState", true);
                break;
            case MaleAnimState.OFFBATTLE:
                ChangeAnimState("IsBattleState", false);
                StackAnimation();
                break;
            case MaleAnimState.LOVE:
                PlayLoveAnim();
                break;
            case MaleAnimState.HATE:
                PlayHateAnim();
                break;
            default:
                Debug.LogError(this);
                break;
        }
    }
    private void Start()
    {
        PlayRandomIdle();
    }

    public void StackAnimation()
    {
        if (this.gameObject.activeSelf)
        {
            this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast);
            ChangeAnimState("IsWalking", true);
        }
    }

    private void ChangeAnimState(string name, bool value)
    {
        collectableAnimator.SetBool(name, value);
    }

    private void PlayRandomIdle()
    {
        var rand = Random.Range(0, 4);
        collectableAnimator.SetFloat("IdleType", rand);
    }

    private void PlayLoveAnim()
    {
        collectableAnimator.SetFloat("BattleResult", 1);
    }

    private void PlayHateAnim()
    {
        this.transform.DOLocalRotate(Vector3.zero, 1f, RotateMode.Fast)
            .SetEase(Ease.OutSine)
            .OnComplete(() => collectableAnimator.SetFloat("BattleResult", 2));
    }
    private void PlayBattle()
    {
        UpdateAnimState(MaleAnimState.ONBATTLE);
    }
    private void StopBattle()
    {
        UpdateAnimState(MaleAnimState.OFFBATTLE);
    }
}
