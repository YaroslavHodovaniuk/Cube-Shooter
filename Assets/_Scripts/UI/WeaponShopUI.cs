using UnityEngine;
using System;
using UnityEngine.UI;

public class WeaponShopUI : WindowUI
{
    [SerializeField] private ShopGrid shopGrid;

    public override void Init()
    {
        base.Init();
        shopGrid.Init();
    }

    protected override void InitButtons()
    {
        m_Buttons[0].onClick.AddListener(OnBackMenuButtonPreesed);
        m_Buttons[1].onClick.AddListener(GoToStartMenuButtonPreesed);
        m_Buttons[2].onClick.AddListener(GoToMapMenuButtonPreesed);
        m_Buttons[3].onClick.AddListener(GoToWeaponMenuButtonPreesed);
        m_Buttons[4].onClick.AddListener(OnStartLevelButtonPreesed);
    }
    private void OnBackMenuButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(m_Index - 1);
    }
    private void GoToStartMenuButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(0);
    }
    private void GoToMapMenuButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(1);
    }
    private void GoToWeaponMenuButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(2);
    }
    private void OnStartLevelButtonPreesed()
    {
        Shop.Instance.OnStartLevel?.Invoke();
        Systems.Instance.StartLevel();
    }
}
