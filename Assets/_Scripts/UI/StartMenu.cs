using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : WindowUI
{
    protected override void InitButtons()
    {
        m_Buttons[1].onClick.AddListener(OnChooseWeaponButtonPreesed);
    }
    protected override void DisableButtons()
    {
        m_Buttons[1].onClick.RemoveAllListeners();
    }
    private void OnChooseWeaponButtonPreesed()
    {
        GoToNextWindowAction?.Invoke(1);
    }
}
