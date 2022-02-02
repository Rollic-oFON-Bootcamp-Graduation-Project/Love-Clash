using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveBar : MonoSingleton<LoveBar>
{
    [SerializeField] private GameObject loveBar;
    [SerializeField] private Image heartFill;

    // TODO
    // CREATE DATA FOR THIS
    [SerializeField] private float currLove;
    [SerializeField] private float maxLove = 2f;


    private void OnEnable()
    {
        Observer.StartBattle += ActiveLoveBar;
        Observer.StopBattle += DeActiveLoveBar;
    }
    private void OnDisable()
    {
        Observer.StartBattle -= ActiveLoveBar;
        Observer.StopBattle -= DeActiveLoveBar;

    }

    private void ActiveLoveBar()
    {
        loveBar.SetActive(true);
    }
    private void DeActiveLoveBar()
    {
        loveBar.SetActive(false);
    }

    public void UpdateLoveBar(float value)
    {
        currLove += value;

        if (currLove > maxLove) currLove = maxLove;
        var loveAmount = currLove / maxLove;
        Debug.Log($"Changed love: {loveAmount}");

        heartFill.fillAmount = loveAmount;
    }
}
