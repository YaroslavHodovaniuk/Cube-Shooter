using System.Collections.Generic;
using UnityEngine;

public class UIWindowManager : MonoBehaviour
{
    [SerializeField] private List<WindowUI> windows;  // 0-startMenu;  1-mapShop; 2-weaponshop; ??-setting
    [SerializeField] private MoneyCountUI moneyCountUI;
    private WindowUI _currentWindow;

    private void Start()
    {
        for (int i = 0; i < windows.Count; i++)
        {
            WindowUI window = windows[i];            
            window.GoToNextWindowAction += OpenNewWindow;
            window.m_Index = i;
        }
        SetStartWindow();
        moneyCountUI.Init();
    }
    private void OnDisable()
    {
        foreach (var window in windows)
        {
            window.GoToNextWindowAction -= OpenNewWindow;
        }
    }
    private void SetStartWindow()
    {
        foreach(var window in windows)
        {
            window.gameObject.SetActive(false);
        }
        OpenNewWindow(0);
    }

    private void OpenNewWindow(int index)
    {
        ClosePreviosWindow();
        _currentWindow = windows[index];
        _currentWindow.gameObject.SetActive(true);
        _currentWindow.Init();
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
