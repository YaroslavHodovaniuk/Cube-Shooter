using System;
using System.IO.Enumeration;
using UnityEngine;

/// <summary>
/// Keeping all relevant information about a unit on a scriptable means we can gather and show
/// info on the menu screen, without instantiating the unit prefab.
/// </summary>
[CreateAssetMenu(menuName = "f/DefaultUnit", order = 1)]
public class ScriptableUnit : ScriptableObject {
    public Faction Faction;

    [SerializeField] private Stats _stats;
    public Stats BaseStats => _stats;

    // Used in game
    public UnitBase Prefab;
    
    // Used in menus
    public string Description;
    public Sprite MenuSprite;
}

/// <summary>
/// Keeping base stats as a struct on the scriptable keeps it flexible and easily editable.
/// We can pass this struct to the spawned prefab unit and alter them depending on conditions.
/// </summary>
[Serializable]
public struct Stats {
    public int Health;
    public int TravelDistance;

    public int Score;

    public int Damage;
    public float AttackSpeed;
    public float AttackDistance;
    public float AngryDistance;

    public int MaxHealth;
}

[Serializable]
public enum Faction {
    Heroes = 0,
    Enemies = 1
}