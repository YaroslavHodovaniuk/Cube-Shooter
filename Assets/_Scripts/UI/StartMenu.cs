using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : WindowUI
{
    protected override void InitButtons()
    {
        m_Buttons[2].onClick.AddListener(OnChooseWeaponButtonPreesed);
        m_Buttons[1].onClick.AddListener(OnChooseMapButtonPreesed);
    }
    private void OnChooseWeaponButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(2);
    }
    private void OnChooseMapButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(1);
    }
}
