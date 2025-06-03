using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EventCountdown : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private float countdownDuration = 5f;
    [SerializeField] private bool playOnAwake;

    [Header("Optional")]
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private float preDelay = 0f;
    [SerializeField] private float postDelay = 0f;

    [Space(20)]
    [SerializeField] private UnityEvent onCountdownEnd;



    private void Awake()
    {
        if (playOnAwake)
        {
            StartCoroutine(StartCountdown());
        }
    }

    public IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(preDelay);

        float remainingTime = countdownDuration;
        while (remainingTime > 0f)
        {
            if (countdownText != null)
            {
                countdownText.text = $"{Mathf.CeilToInt(remainingTime)}";
            }
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }
        if (countdownText != null)
        {
            countdownText.text = "0";
        }

        yield return new WaitForSeconds(postDelay);
        onCountdownEnd?.Invoke();
    }
}
