using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public int wave = 1;
    [Header("Events")]
    [SerializeField] private UnityEvent gameOverEvent;
    [SerializeField] private UnityEvent waveTransition;

    [Header("Init Wave Config")]
    // target wave
    [SerializeField] private int init_TargetScore = 200;
    [SerializeField] private float init_TimerDuration = 30f;
    [Header("")]
    // mole difficulty
    [SerializeField] private float init_SpawnInterval = 0.6f;
    [SerializeField] private float init_WaitDurationMuliplier = 1f;
    [Header("")]

    [Header("Next Wave Config")]
    // target wave
    [SerializeField] private int next_TargetScoreMultiplier = 5;
    [Header("")]
    // mole difficulty
    [SerializeField] private float next_SpawnInterval = 0.95f;
    [SerializeField] private float next_WaitDurationMuliplier = 0.7f;
    [Header("")]
    // reward
    [SerializeField] private int next_PointMultiplier = 2;

    

    private MoleSpawner moleSpawner;
    private MolePoolConfig molePoolConfig;
    private ScoreManager scoreManager;
    private Timer timer;

    private void Start()
    {
        moleSpawner = MoleSpawner.Instance;
        molePoolConfig = FindFirstObjectByType<MolePoolConfig>();
        scoreManager = ScoreManager.Instance;
        timer = FindFirstObjectByType<Timer>();

        // set initial config
        scoreManager.SetTargetScore(init_TargetScore);
        timer.SetTimeLimit(init_TimerDuration);
        moleSpawner.spawnInterval = init_SpawnInterval;
        molePoolConfig.waitDurationMultiplier = init_WaitDurationMuliplier;

        // start game
        moleSpawner.StartSpawn();
        timer.StartTimer();
    }

    private void Update()
    {
        if (timer.timeRemaining <= 0)
        {
            moleSpawner.StopSpawn();
            gameOverEvent?.Invoke();
        }

        if (scoreManager.IsTargetScoreReached())
        {
            waveTransition?.Invoke();
            NextWave();
        }
    }

    public void NextWave()
    {
        // increase all point reward
        scoreManager.pointMultiplier *= next_PointMultiplier;
        // decrease time between next spawn
        moleSpawner.spawnInterval *= next_SpawnInterval;
        // decrease mole wait duration
        molePoolConfig.waitDurationMultiplier *= next_WaitDurationMuliplier;
        // set next wave target
        scoreManager.SetTargetScore(scoreManager.GetTargetScore() * next_TargetScoreMultiplier);

        timer.ResetTimer();
        wave++;
        timer.StartTimer();
    }

    public void PauseWave()
    {
        timer.PauseTimer();
    }
}
