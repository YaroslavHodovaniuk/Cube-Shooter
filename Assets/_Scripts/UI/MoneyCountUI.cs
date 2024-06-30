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
        _textMeshPro.text = "Money: " + value.ToString();
    }
    private void OnDisable()
    {
        Shop.Instance.PlayerBalanceHasChanged -= UpdateBalance;
    }
}
