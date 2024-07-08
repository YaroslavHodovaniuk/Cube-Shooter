using InfimaGames.LowPolyShooterPack;
using System.Collections.Generic;
using UnityEngine;

public class Environment : StaticInstance<Environment>
{
    private Transform _playerSpawnPoint;
    private List<Transform> enemySpawnPoints;

    private Level _level;
    private HeroUnitBase _player;
    private List<EnemyUnitBase> _enemyUnits = new();

    public Transform PlayerParent => transform.GetChild(0).GetChild(0);
    public Transform EnemyParent => this.transform.GetChild(0).GetChild(1).GetChild(0);

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

    public void RegisterUnit(int index, UnitBase unit)
    {
        if (unit == null)
            return;

        if (index == 1)
        {
            _player = unit as HeroUnitBase;
        }
        else 
        {
            _enemyUnits.Add(unit as EnemyUnitBase);
        }
    }
}
