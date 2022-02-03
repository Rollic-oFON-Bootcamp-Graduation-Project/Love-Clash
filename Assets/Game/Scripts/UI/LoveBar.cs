using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoveBar : MonoBehaviour
{
    [SerializeField] private Image heart;

    // TODO
    // CREATE DATA FOR THIS
    [SerializeField] private float currLove;
    [SerializeField] private float maxLove = 2f;

    public void UpdateLoveBar(float value)
    {
        currLove += value;
        currLove = Mathf.Clamp(currLove, 0, maxLove);
        var loveAmount = currLove / maxLove;

        heart.fillAmount = loveAmount;
    }
}
