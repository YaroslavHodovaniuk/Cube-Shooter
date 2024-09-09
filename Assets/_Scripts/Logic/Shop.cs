using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shop : Singleton<Shop>
{
    private const int _defaultEquipedWeaponIndex = 0;

    private int _currentEquipedWeaponIndex;

    private int playerBalance;

    private Dictionary<int, int> _weaponPrices = new Dictionary<int, int>()
    {
        { 0, 0 }, 
        { 1, 2000 }, 
        { 2, 2000 }, 
        { 3, 2000 }, 
        { 4, 2000 }, 
        { 5, 2000 },
        { 6, 2000 },
        { 7, 2000 },
        { 8, 2000 },
        { 9, 2000 },
        { 10, 2000 },
        { 11, 2000 },
        { 12, 2000 },
    };

    public int PlayerBalance { 
        get => playerBalance; 
        set 
        {   
            playerBalance = value;
            PlayerBalanceHasChanged?.Invoke(playerBalance);
        }  
    }

    public int CurrentEquipedWeaponIndex 
    { 
        get => _currentEquipedWeaponIndex;
        set 
        {
            _currentEquipedWeaponIndex = value;
            CurrentEquipedWeaponHasChanged?.Invoke();
        } 
    }

    public UnityAction<int> PlayerBalanceHasChanged;
    public UnityAction CurrentEquipedWeaponHasChanged;
    public UnityAction OnStartLevel;

    private void OnEnable()
    {
        
        OnStartLevel += TraslateDataToSystems;
    }
    private void OnDisable() 
    {
        OnStartLevel -= TraslateDataToSystems;
    }
    private void Start()
    {
        CurrentEquipedWeaponIndex = _defaultEquipedWeaponIndex;

        // Загрузка баланса игрока из PlayerPrefs
        PlayerBalance = PlayerPrefs.GetInt("PlayerBalance", 100000); // Начальный баланс - 1000 монет
        PurchaseWeapon(0);
    }

    public void PurchaseWeapon(int index)
    {
        if (PlayerBalance >= _weaponPrices[index])
        {
            PlayerBalance -= _weaponPrices[index];

            PlayerPrefs.SetInt("PlayerBalance", PlayerBalance);
            PlayerPrefs.SetInt(index.ToString(), 1); // Сохранение покупки 

            TryEquipWeapon(index);
        }
        else
        {
            Debug.Log("Not enough balance!");
        }
    }

    public bool TryEquipWeapon(int index)
    {
        if (CheckBoughtWeapon(index))
        {
            CurrentEquipedWeaponIndex = index;            
            return true;
        }
        else 
        {
            return false;
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

    private void TraslateDataToSystems()
    {
       
        Systems.Instance.LevelData.ChoosedWeaponID = CurrentEquipedWeaponIndex;
    }
}
