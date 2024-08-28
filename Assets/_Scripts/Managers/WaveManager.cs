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

    [Tooltip("Last wave count, then player win?")]
    [SerializeField] private bool _isSpawnOnlyInWaveProgress;

    private float _lastWaveStarted = 0;
    private float _lastWaveEnded = 0;

    private int _waveCount = 0;

    public enum WaveState
    {
        WaveInProgress = 0,
        WaveOnCooldown = 1,
    }

    public WaveState CurrentState { get; private set; }
    public int WaveCount { get => _waveCount; private set => _waveCount = value; }

    public bool IsSpawnOnlyInWaveProgress => _isSpawnOnlyInWaveProgress;

    public static event Action<WaveState> OnBeforeWaveStateChanged;
    public static event Action<WaveState> OnAfterWaveStateChanged;

    public void StartWaveChangingProcess()
    {
        if (LevelGameManager.Instance.State != LevelGameState.GameInProgress)
            return;

        ChangeState(WaveState.WaveOnCooldown);
    }

    public float GetTimeToNextState()
    {
        if (CurrentState == WaveState.WaveInProgress)
        {
            return Mathf.Abs(Time.time - (_lastWaveStarted + _waveDuration));
        }
        else
        {
            return Mathf.Abs(Time.time - (_lastWaveEnded + _waveCooldown));
        }
    }

    private void OnDestroy()
    {
        OnAfterWaveStateChanged = null;
    }
    private void Update()
    {
        if (LevelGameManager.Instance.State != LevelGameState.GameInProgress)
        {
            return;
        }
        if (CurrentState == WaveState.WaveOnCooldown)
        {
            if (Time.time - _lastWaveEnded > _waveCooldown)
            {
                ChangeState(WaveState.WaveInProgress);
            }
        }
        else if (CurrentState == WaveState.WaveInProgress)
        {
            if (Time.time - _lastWaveStarted > _waveDuration)
            {
                ChangeState(WaveState.WaveOnCooldown);
            }
        }
    }
    private void ChangeState(WaveState state)
    {
        OnBeforeWaveStateChanged?.Invoke(state);
        CurrentState = state;
        switch (state)
        {
            case WaveState.WaveInProgress:
                StartWave(); break;
            case WaveState.WaveOnCooldown:
                EndWave(); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }

        OnAfterWaveStateChanged?.Invoke(state);
    }
    private void StartWave()
    {
        _lastWaveStarted = Time.time;
    }

    private void EndWave()
    {
        _lastWaveEnded = Time.time;
        WaveCount++;
    }
}
