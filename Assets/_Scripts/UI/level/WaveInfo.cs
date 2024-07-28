using InfimaGames.LowPolyShooterPack.Interface;
using TMPro;
using UnityEngine;

public class WaveInfo : CanvasAlpha
{
    [Title(label: "Fields")]
    [SerializeField] private TextMeshProUGUI _textWaveCounter;

    [SerializeField] private TextMeshProUGUI _textTimeToNextStage;

    private float timeToNextStage;

    protected override void OnInitingUI(LevelGameState gameState)
    {
        base.OnInitingUI(gameState);
        WaveManager.OnAfterWaveStateChanged += OnWaveStageChange;
    }
    protected override void Tick()
    {
        base.Tick();
        UpdateTimeToNextStage();
    }

    private void UpdateTimeToNextStage()
    {
        _textTimeToNextStage.text = "Remaning :" + WaveManager.Instance.GetTimeToNextState();
    }
    private void OnWaveStageChange(WaveManager.WaveState state)
    {
        if (state == WaveManager.WaveState.WaveInProgress)
        {
            _textWaveCounter.text = "Wave: " + WaveManager.Instance.WaveCount;
        }
    }
}
