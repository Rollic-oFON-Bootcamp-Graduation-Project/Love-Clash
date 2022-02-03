using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class GameOverScreen : UIBase
{
    [SerializeField] private Image leftHeart, rightHeart, failedText;
    private Sequence sequence;

    public void Restart()
    {
        //UIManager.Instance.DisableUI(UIManager.Instance.GameOverScreen, UIManager.Instance.StartScreen);
        UIManager.Instance.StartScreen.EnablePanel();
        UIManager.Instance.GameOverScreen.DisablePanel();
        MySceneManager.Instance.RestartActiveScene();
    }

    public override void EnablePanel()
    {
        base.EnablePanel();
        GameOverAnimation();
    }
    public override void DisablePanel()
    {
        base.DisablePanel();
        ResetUITransform();
    }

    private void GameOverAnimation()
    {
        failedText.transform.localScale = Vector3.zero;
        failedText.transform.DOScale(1, 1f);
        var heartRotate = new Vector3(0, 0, 10);
        leftHeart.transform.DORotate(heartRotate, 1f);
        rightHeart.transform.DORotate(-heartRotate, 1f);

        //sequence.Play();
    }

    private void ResetUITransform()
    {
        failedText.transform.localScale = Vector3.one;
        leftHeart.transform.rotation = rightHeart.transform.rotation = Quaternion.identity;
    }
    //GAMEOVER SCREEN FUNCTIONS, RESTART, SHOW SCORE ETC
}
