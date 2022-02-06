using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WinScreen : UIBase
{
    [SerializeField] private Image heart, glow, winText;

    public void NextLevel()
    {
        UIManager.Instance.StartScreen.EnablePanel();
        UIManager.Instance.WinScreen.DisablePanel();
        MySceneManager.Instance.LoadNextLevel();
    }
    public override void EnablePanel()
    {
        base.EnablePanel();
        NextLevelAnimation();
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        ResetUITransform();
    }

    private void NextLevelAnimation()
    {
        winText.transform.localScale = heart.transform.localScale =  Vector3.zero;
        winText.transform.DOScale(1, 1f);
        heart.transform.DOScale(1, 1f);


        //sequence.Play();
    }

    private void ResetUITransform()
    {
        winText.transform.localScale = heart.transform.localScale =  Vector3.one;
    }

}
