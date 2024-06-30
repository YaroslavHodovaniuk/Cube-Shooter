using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class WindowUI : MonoBehaviour
{
    [SerializeField] protected List<Button> m_Buttons;

    public int m_Index;

    public UnityAction<int> GoToNextWindowAction;

    private void OnEnable()
    {
        Init();
    }
    private void OnDisable()
    {
        DisableButtons();
    }
    protected virtual void Init()
    {
        InitButtons();
    }
    protected virtual void InitButtons()
    {
        
    }
    protected virtual void DisableButtons()
    {

    }
}
