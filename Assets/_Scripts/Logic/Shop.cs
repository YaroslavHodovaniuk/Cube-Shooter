using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : Singleton<Shop>
{

    private int playerBalance;

    private Dictionary<int, int> _weaponPrices = new Dictionary<int, int>()
    {
        { 0, 2000 }, 
        { 1, 2000 }, 
        { 2, 25000 }, 
        { 3, 30000 }, 
        { 4, 22000 }, 
        { 5, 27000 },
        { 6, 28000 },
        { 7, 29000 },
        { 8, 30000 },
        { 9, 31000 },
        { 10, 32000 },
        { 11, 33000 },
        { 12, 34000 },
    };

    public int PlayerBalance { 
        get => playerBalance; 
        set 
        {   
            playerBalance = value;
            PlayerBalanceHasChanged?.Invoke(playerBalance);
        }  
    }
    public UnityAction<int> PlayerBalanceHasChanged;

    private void Start()
    {
        // Загрузка баланса игрока из PlayerPrefs
        PlayerBalance = PlayerPrefs.GetInt("PlayerBalance", 100000); // Начальный баланс - 1000 монет
        
    }

    public void PurchaseWeapon(int index)
    {
        if (PlayerBalance >= _weaponPrices[index])
        {
            PlayerBalance -= _weaponPrices[index];
            PlayerPrefs.SetInt("PlayerBalance", PlayerBalance);
            PlayerPrefs.SetInt(index.ToString(), 1); // Сохранение покупки 

            //WeaponShopUI.Instance.UpdateMoneyText(playerBalance);

            Debug.Log($"weapon {index} purchased!");
        }
        else
        {
            Debug.Log("Not enough balance!");
        }
    }

    public bool CheckBoughtWeapon(int id)
    {
        if (PlayerPrefs.GetInt(id.ToString(), 0) == 1)
            return true;
        else
            return false;
    }
    
    

    public int GetPrice(int id)
    {
        return _weaponPrices[id];
    }

}
