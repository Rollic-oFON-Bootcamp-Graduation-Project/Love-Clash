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
    private float maxLove = 3f;
    private void OnDisable()
    {
        ResetLoveBar();
    }
    public void UpdateLoveBar(float value)
    {
        //value *= maxLove;
        currLove = (float)value / (float)maxLove;
        //currLove = Mathf.Clamp(currLove, 0, maxLove);
        var loveAmount = ((float)value / (float)maxLove);

        heart.fillAmount = loveAmount;
    }
    public void SetMaxLove(float value)
    {
        maxLove = value;
    }
    public void ResetLoveBar()
    {
        heart.fillAmount = 0;
    }
}
