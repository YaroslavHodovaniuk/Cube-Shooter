using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : Singleton<Shop>
{

    public int playerBalance;

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
        { 13, 35000 },
    };
    
    private void Start()
    {
        // Загрузка баланса игрока из PlayerPrefs
        playerBalance = PlayerPrefs.GetInt("PlayerBalance", 100000); // Начальный баланс - 1000 монет
    }

    public void PurchaseCar(int index)
    {
        if (playerBalance >= _weaponPrices[index])
        {
            playerBalance -= _weaponPrices[index];
            PlayerPrefs.SetInt("PlayerBalance", playerBalance);
            PlayerPrefs.SetInt(index.ToString(), 1); // Сохранение покупки 
            
            //tyt update ui
            //uiScript.Instance.UpdateMoneyText(playerBalance);
            //uiScript.Instance.UpdateButton();
            
            Debug.Log($" purchased!");
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
