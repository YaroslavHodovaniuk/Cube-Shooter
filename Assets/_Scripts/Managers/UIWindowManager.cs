using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{
    [SerializeField] private List<WindowUI> windowUIs;  // 0-startMenu; 1-weaponshop;   2-mapShop; ??-setting
    [SerializeField] private MoneyCountUI moneyCountUI;
    private WindowUI _currentWindow;

    private void OnEnable()
    {
        for (int i = 0; i < windowUIs.Count; i++)
        {
            WindowUI window = windowUIs[i];
            window.GoToNextWindowAction += OpenNewWindow;
            window.m_Index = i;
        }
        SetStartWindow();
        moneyCountUI.Init();
    }
    private void OnDisable()
    {
        foreach (var window in windowUIs)
        {
            window.GoToNextWindowAction -= OpenNewWindow;
        }
    }
    private void SetStartWindow()
    {
        OpenNewWindow(0);
    }

    private void OpenNewWindow(int index)
    {
        ClosePreviosWindow();
        _currentWindow = windowUIs[index];
        _currentWindow.gameObject.SetActive(true);
    }
    private void ClosePreviosWindow()
    {
        if (_currentWindow != null)
        {
            _currentWindow.gameObject.SetActive(false);
            _currentWindow = null;
        }
    }
}
