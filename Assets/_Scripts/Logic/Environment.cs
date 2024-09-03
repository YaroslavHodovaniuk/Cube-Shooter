using InfimaGames.LowPolyShooterPack;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Environment : StaticInstance<Environment>
{
    [SerializeField] private int maxSpawnedEnemy;

    private int _allSpawedEnemy = 0;
    private int _currentEnemyAlive = 0;

    private Transform _playerSpawnPoint;
    private List<Transform> enemySpawnPoints;

    private Level _level;
    private HeroUnitBase _player;
    private List<EnemyUnitBase> _enemyUnits = new();

    private UnityAction<int> AliveEnemyCountHasChenged;

    public Transform PlayerParent => transform.GetChild(0).GetChild(0);
    public Transform EnemyParent => this.transform.GetChild(0).GetChild(1);

    public int CurrentEnemyAlive 
    {   
        get => _currentEnemyAlive; 
        private set
        {
            AliveEnemyCountHasChenged?.Invoke(value);
            _currentEnemyAlive = value;
        }
    }

    public Transform PlayerSpawnPoint 
    {
        get 
        { 
            if (_playerSpawnPoint == null)
            {
                _playerSpawnPoint = PlayerParent.GetChild(0);                
            }
            return _playerSpawnPoint;
        }
    }

    public List<Transform> EnemySpawnPoints 
    {
        get
        {
            if (enemySpawnPoints == null)
            {
                enemySpawnPoints = new();               
                for (int i = 0; i < EnemyParent.childCount; i++)
                {
                    enemySpawnPoints.Add(EnemyParent.GetChild(i));
                }
            }
            return enemySpawnPoints;
        }
    }

    public HeroUnitBase Player => _player;

    public int MaxSpawnedEnemy { get => maxSpawnedEnemy; }

    public void RegisterUnit(UnitBase unit)
    {
        if (unit == null)
            return;

        if (unit.GetType() == typeof(HeroUnitBase))
        {
            _player = unit as HeroUnitBase;
        }
        else 
        {
            var enemy = unit as EnemyUnitBase;
            _enemyUnits.Add(enemy);

            enemy.DeathEvent += UnregisterUnit;

            _allSpawedEnemy++;
            CurrentEnemyAlive++;
        }
    }

    public void UnregisterUnit(UnitBase unit) 
    {
        if (unit == null)
            return;

        if (unit.GetType() == typeof(HeroUnitBase))
        {
            _player = null;
        }
        else
        {
            var enemy = unit as EnemyUnitBase;
            _enemyUnits.Remove(enemy);

            enemy.DeathEvent -= UnregisterUnit;

            CurrentEnemyAlive--;
        }
    }

    public Transform GetRandomEnemySpawnPoint()
    {
        return Player.EnemySpawnCollider.GetSpawnGroup().GetSpawnPoint();
    }
}
