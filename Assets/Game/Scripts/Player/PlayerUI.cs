using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private GateData data;
    private int textLevel = -1;

    private void OnEnable()
    {
        Observer.PlayerUpdate += PlayFeedback;
    }
    private void OnDestroy()
    {
        Observer.PlayerUpdate -= PlayFeedback;
    }

    private void PlayFeedback(int value)
    {
        if (value > 0)
        {
            var feedbacks = data.UpgradeFeedback;
            var textColor = data.UpgradeTextColor;

            textLevel++;
            textLevel = Mathf.Clamp(textLevel, 0, feedbacks.Length);
            StartCoroutine(FeedbackRoutine(feedbacks[textLevel], textColor[textLevel]));

        }
        else
        {
            textLevel--;
        }
    }

    IEnumerator FeedbackRoutine(string text, Color32 newColor)
    {
        feedbackText.SetText(text);
        feedbackText.color = newColor;
        yield return new WaitForSeconds(1f);
        feedbackText.SetText("");
    }
}
