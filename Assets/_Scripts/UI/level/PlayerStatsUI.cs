using InfimaGames.LowPolyShooterPack.Interface;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : CanvasAlpha
{
    [SerializeField] private Slider slider;

    private HeroUnitBase HeroUnitBase;
    protected override void OnInitingUI(LevelGameState gameState)
    {
        base.OnInitingUI(gameState);
        HeroUnitBase = Environment.Instance.Player;
        HeroUnitBase.OnStatsUpdated += UpdateSlider;
    }
    private void OnDestroy()
    {
        HeroUnitBase.OnStatsUpdated -= UpdateSlider;
    }
    private void UpdateSlider(HeroUnitBase HeroUnitBase)
    {
        slider.value = (float)HeroUnitBase.Stats.Health / HeroUnitBase.Stats.MaxHealth;
        
    }
}
