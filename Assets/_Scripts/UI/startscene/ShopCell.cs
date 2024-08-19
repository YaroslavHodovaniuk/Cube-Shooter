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

    private int _index;
    public void Init(Sprite Icon, int index)   //add icon, not GameObject
    {
        _index = index;
        priceText.text = Shop.Instance.GetPrice(_index).ToString();

        _conteiner.sprite = Icon;

        InitButtons();
    }

    private void OnEnable()
    {
        InitButtons();
    }
    private void OnDisable()
    {
        _buyButton.onClick.RemoveAllListeners();
        _equipButton.onClick.RemoveAllListeners();
        if(Shop.Instance != null) Shop.Instance.CurrentEquipedWeaponHasChanged -= ChangeEquipButtonByShop;
    }
    private void InitButtons()
    {
        _buyButton.onClick.AddListener(OnBuyButtonPressed);
        _equipButton.onClick.AddListener(OnEquipButtonPressed);

        _buyButton.gameObject.SetActive(false);
        _equipButton.gameObject.SetActive(false);

        Shop.Instance.CurrentEquipedWeaponHasChanged += ChangeEquipButtonByShop;
        CheckIsPurchased();
        ChangeEquipButtonByShop();
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
        else
        {
            _equipButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(true);
            priceText.text = "";
        }
    }

    private void OnEquipButtonPressed()
    {
        Shop.Instance.TryEquipWeapon(_index);
    }

    private void TrySetEquiped()
    {
        if (Shop.Instance.CurrentEquipedWeaponIndex == _index)
        {
            ChangeEquipButton(true);
        }
    }

    private void ChangeEquipButtonByShop()
    {
        if (Shop.Instance.CurrentEquipedWeaponIndex == _index)
        {
            ChangeEquipButton(true);
        }
        else
        {
            ChangeEquipButton(false);
        }
    }

    private void ChangeEquipButton(bool key)
    {
        if (key)
        {
            _equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equiped";
        }
        else
        {
            _equipButton.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
        }
    }
}
