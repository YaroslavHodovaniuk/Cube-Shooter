using InfimaGames.LowPolyShooterPack.Interface;
using TMPro;
using UnityEngine;

public class WaveInfo : CanvasAlpha
{
    [Title(label: "Counter")]
    [SerializeField] private TextMeshProUGUI _textWaveCounter;
    [SerializeField] private TextMeshProUGUI _textTimeToNextStage;

    [Title(label: "Tips")]
    [SerializeField] private Animator _waveStartedTipAnimator;
    [SerializeField] private Animator _waveEndedTipAnimator;


    private float timeToNextStage;

    protected override void OnInitingUI(LevelGameState gameState)
    {
        base.OnInitingUI(gameState);
        WaveManager.OnAfterWaveStateChanged += OnWaveStageChange;
        
    }
    private void OnDestroy()
    {
        WaveManager.OnAfterWaveStateChanged -= OnWaveStageChange;
    }
    protected override void Tick()
    {
        base.Tick();
        UpdateTimeToNextStage();
    }

    private void UpdateTimeToNextStage()
    {
        if (LevelGameManager.Instance.State != LevelGameState.GameInProgress)
            return;

        _textTimeToNextStage.text = "Remaning :" + WaveManager.Instance.GetTimeToNextState().ToString().ToLower();
    }
    private void OnWaveStageChange(WaveManager.WaveState state)
    {
        if (state == WaveManager.WaveState.WaveInProgress)
        {
            Debug.Log(this);
            _waveStartedTipAnimator.SetTrigger("WaveStarted");
            _textWaveCounter.text = "Wave: " + WaveManager.Instance.WaveCount;
        }
        else if (state == WaveManager.WaveState.WaveOnCooldown)
        {
            Debug.Log(this);
            _waveEndedTipAnimator.SetTrigger("WaveEnded");
        }
    }
}
