using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCell : MonoBehaviour
{
    [SerializeField] private GameObject _conteiner;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button _buyButton;
    [SerializeField] private Button _equipButton;

    private int _index;
    private GameObject weaponObj;
    public void Init(GameObject Icon, int index)   //add icon, not GameObject
    {
        _index = index;
        weaponObj = Icon;
        text.text = Shop.Instance.GetPrice(_index).ToString();
        InitButtons();

        var _ = Instantiate(weaponObj, _conteiner.transform);
        _.transform.localScale = Vector3.one * 300f;
        _.transform.rotation = Quaternion.Euler(0f,90f,0f);

        _.layer = this.gameObject.layer;
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
