using TMPro;
using UnityEngine;

public class MoneyCountUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    public void Init()
    {
        Shop.Instance.PlayerBalanceHasChanged += UpdateBalance;
        UpdateBalance(Shop.Instance.PlayerBalance);
    }
    private void UpdateBalance(int value)
    {
        _textMeshPro.text = value.ToString();
    }
    private void OnDisable()
    {
        if (Shop.Instance != null)
        {
            Shop.Instance.PlayerBalanceHasChanged -= UpdateBalance;
        }
    }
}
