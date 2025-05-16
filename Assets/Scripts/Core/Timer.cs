using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLimit = 30f;
    [SerializeField] private Slider slider;
    [SerializeField] private bool playOnStart = false;

    public float timeRemaining { get; private set; }
    
    private bool isTimerRunning = false;

    private void Start()
    {
        slider.maxValue = timeLimit;
        ResetTimer();

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
                PauseTimer();
                timeRemaining = 0;
                Debug.Log("Time's up!");
            }
        }
    }

    public void SetTimeLimit(float newTimeLimit)
    {
        timeLimit = newTimeLimit;
        slider.maxValue = timeLimit;
        ResetTimer();
    }

    public void StartTimer()
    {
        isTimerRunning = true;
        timeRemaining = timeLimit;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
        ResetTimer();
    }

    public void PauseTimer()
    {
        isTimerRunning = false;
    }

    public void ResumeTimer()
    {
        isTimerRunning = true;
    }

    public void ResetTimer()
    {
        timeRemaining = timeLimit;
        slider.value = timeRemaining;
    }
}
