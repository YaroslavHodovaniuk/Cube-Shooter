using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class WindowUI : MonoBehaviour
{
    [SerializeField] protected List<Button> m_Buttons;

    public int m_Index;

    public UnityAction<int> GoToNextWindowAction;

    private void OnDisable()
    {
        DisableButtons();
    }
    public virtual void Init()
    {
        InitButtons();
    }
    protected virtual void InitButtons()
    {
        
    }
    protected void DisableButtons()
    {
        for (int i = 0; i < m_Buttons.Count; i++)
        {
            m_Buttons[i].onClick.RemoveAllListeners();
        }
    }
}
