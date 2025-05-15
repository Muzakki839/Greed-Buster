using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private Slider slider;
    [SerializeField] private bool playOnStart = false;

    private float timeRemaining;
    private bool isTimerRunning = false;

    private void Start()
    {
        timeRemaining = timeLimit;
        slider.maxValue = timeLimit;
        slider.value = timeRemaining;

        if (playOnStart)
        {
            StartTimer();
        }
    }

    private void Update()
    {
        if (isTimerRunning)
        {
            timeRemaining -= Time.deltaTime;
            slider.value = timeRemaining;

            if (timeRemaining <= 0)
            {
                StopTimer();
                timeRemaining = 0;
                Debug.Log("Time's up!");
            }
        }
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        timeRemaining = timeLimit;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }
}
