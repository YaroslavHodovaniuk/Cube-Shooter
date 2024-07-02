using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
    [SerializeField] private Image _conteiner;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;

    [SerializeField] private Color _equipedcolor;
    [SerializeField] private Color _unequipedcolor;

    private int _index;
    public void Init(Sprite Icon, int index)   //add icon, not GameObject
    {
        _index = index;
        priceText.text = Shop.Instance.GetPrice(_index).ToString();
        InitButtons();
        Shop.Instance.CurrentEquipedWeaponHasChanged += ChangeEquipButtonByShop;
        _conteiner.sprite = Icon;
    }
    private void OnDisable()
    {
        _buyButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
        Shop.Instance.CurrentEquipedWeaponHasChanged -= ChangeEquipButtonByShop;
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
        CheckIsPurchased();
    }

    private void OnBuyButtonPressed()
    {
        Shop.Instance.PurchaseWeapon(_index);
        CheckIsPurchased();
    }
    private void CheckIsPurchased()
    {
        if (Shop.Instance.CheckBoughtWeapon(_index))
        {
            _equipButton.gameObject.SetActive(true);
            _buyButton.gameObject.SetActive(false);
            priceText.text = "";
        }
    }

    private void OnEquipButtonPressed()
    {
        if (Shop.Instance.TryEquipWeapon(_index))
        {
            priceText.text = "";
            ChangeEquipButton(true); 
        }
    }

    private void ChangeEquipButtonByShop()
    {
        ChangeEquipButton(false);
    }

    private void ChangeEquipButton(bool key)
    {
        if (key)
        {
            priceText.text = "Equiped";

            _equipButton.GetComponent<Image>().color = _equipedcolor;
        }
        else
        {
            priceText.text = "Equip";
            _equipButton.GetComponent<Image>().color = _unequipedcolor;
        }
    }
}
