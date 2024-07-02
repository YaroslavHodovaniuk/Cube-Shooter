using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
    [SerializeField] private Image _conteiner;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;

    private int _index;
    public void Init(Sprite Icon, int index)   //add icon, not GameObject
    {
        _index = index;
        text.text = Shop.Instance.GetPrice(_index).ToString();
        InitButtons();

        _conteiner.sprite = Icon;
    }
    private void OnDisable()
    {
        _buyButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
    }
    private void InitButtons()
    {
        _buyButton.onClick.AddListener(OnBuyButtonPressed);
        //equiped button assign

        _buyButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);

        if (Shop.Instance.CheckBoughtWeapon(_index))
        {
            _equipButton.gameObject.SetActive(true);
        }
        else
        {
            _buyButton.gameObject.SetActive(true);
        }
    }

    private void OnBuyButtonPressed()
    {
        Shop.Instance.PurchaseWeapon(_index);
        if (Shop.Instance.CheckBoughtWeapon(_index))
        {
            _equipButton.gameObject.SetActive(true);
            _buyButton.gameObject.SetActive(false);
        }
    }
}
