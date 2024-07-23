using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class WaveManager : StaticInstance<WaveManager>
{
    [Tooltip("Duration wave in seconds")]
    [SerializeField] private int _waveDuration;

    [Tooltip("Cooldown wave in seconds")]
    [SerializeField] private int _waveCooldown;

    [Tooltip("Last wave count, then player win?")]
    [SerializeField] private int _lastWaveCount;

    private float _lastWaveStarted = 0;
    private float _lastWaveEnded = 0;

    private int _waveCount = 0;

    public enum WaveState
    {
        WaveInProgress = 0,
        WaveOnCooldown = 1,
    }

    public WaveState CurrentState { get; private set; }

    public static event Action<WaveState> OnBeforeStateChanged;
    public static event Action<WaveState> OnAfterStateChanged;

    public void Start()
    {
        LevelGameManager.OnAfterStateChanged += StartWaveChangingProcess;
    }

    private void StartWaveChangingProcess(LevelGameState gameState)
    {
        Debug.Log("qWE");
        if (gameState == LevelGameState.InitUI)
            StartCoroutine(WaveCaroutine());
    }
    private IEnumerator WaveCaroutine()
    {
        while (LevelGameManager.Instance.State == LevelGameState.GameInProgress)
        {
            Debug.Log("qWE");
            if (_waveCount == 0)
            {
                CurrentState = WaveState.WaveOnCooldown;
            }

            if (CurrentState == WaveState.WaveOnCooldown)
            {
                if (Time.time - _lastWaveEnded >= _waveCooldown)
                {
                    ChangeState(WaveState.WaveInProgress);
                    Debug.Log("WaveInProgress");
                    yield return new WaitForSeconds(_waveDuration);
                }
            }
            else if (CurrentState == WaveState.WaveInProgress)
            {
                if (Time.time - _lastWaveStarted >= _waveDuration)
                {
                    ChangeState(WaveState.WaveOnCooldown);
                    Debug.Log("WaveOnCooldown");
                    yield return new WaitForSeconds(_waveCooldown);
                }
            }
        }
    }
    private void ChangeState(WaveState state)
    {
        OnBeforeStateChanged?.Invoke(state);

        switch (state)
        {
            case WaveState.WaveInProgress:
                StartWave(); break;
            case WaveState.WaveOnCooldown:
                EndWave(); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        OnAfterStateChanged?.Invoke(state);
    }
    private void StartWave()
    {
        _lastWaveStarted = Time.time;
    }

    private void EndWave()
    {
        _lastWaveEnded = Time.time;
        _waveCount++;
    }
}
