using InfimaGames.LowPolyShooterPack.Interface;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsUI : CanvasAlpha
{
    [SerializeField] private Slider slider;

    [SerializeField] private Animator applingDamage;

    private HeroUnitBase HeroUnitBase;
    protected override void OnInitingUI(LevelGameState gameState)
    {
        base.OnInitingUI(gameState);
        HeroUnitBase = Environment.Instance.Player;
        HeroUnitBase.OnStatsUpdated += UpdateSlider;
        HeroUnitBase.OnDamageTaken += applyAnimation;
    }
    private void OnDestroy()
    {
        HeroUnitBase.OnStatsUpdated -= UpdateSlider;
        HeroUnitBase.OnDamageTaken -= applyAnimation;
    }
    private void UpdateSlider(HeroUnitBase HeroUnitBase)
    {
        slider.value = (float)HeroUnitBase.Stats.Health / HeroUnitBase.Stats.MaxHealth;
    }
    private void applyAnimation(HeroUnitBase HeroUnitBase)
    {
        applingDamage.SetTrigger("Run");
    }
}
