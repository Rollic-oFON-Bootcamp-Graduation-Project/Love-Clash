using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private GateData data;
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
        var feedbacks = (value > 0) ? data.UpgradeFeedback : data.DowngradeFeedback;
        var randFeedback = feedbacks[Random.Range(0, feedbacks.Length)];
        StartCoroutine(FeedbackRoutine(randFeedback));
    }

    IEnumerator FeedbackRoutine(string text)
    {
        feedbackText.SetText(text);
        yield return new WaitForSeconds(1f);
        feedbackText.SetText("");
    }
}
