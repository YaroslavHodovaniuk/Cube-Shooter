using UnityEngine;
using System;
using UnityEngine.UI;

public class WeaponShopUI : WindowUI
{
    [SerializeField] private ShopGrid shopGrid;

    public void UpdateButton()
    {
        throw new NotImplementedException();
    }

    public void UpdateMoneyText(int playerBalance)
    {
        throw new NotImplementedException();
    }
    public override void Init()
    {
        base.Init();
        shopGrid.Init();
    }

    protected override void InitButtons()
    {
        m_Buttons[0].onClick.AddListener(OnBackToStartMenuButtonPreesed);
    }
    protected override void DisableButtons()
    {
        m_Buttons[0].onClick.RemoveAllListeners();
    }
    private void OnBackToStartMenuButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(0);
    }
    

}
