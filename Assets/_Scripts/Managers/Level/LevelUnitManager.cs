using System;
using System.Linq;
using UnityEngine;

public class LevelUnitManager : StaticInstance<LevelUnitManager>
{

    public void SpawnPlayer()
    {
        SpawnUnit(1, Environment.Instance.PlayerSpawnPoint, Environment.Instance.PlayerParent);
    }

    public void SpawningEnemies()
    {
        for (int i = 0; i < Environment.Instance.EnemySpawnPoints.Count; i++)
        {
            SpawnUnit(0, Environment.Instance.EnemySpawnPoints[i], Environment.Instance.EnemyParent);
        }
    }

    void SpawnUnit(int unitID, Transform transform, Transform transformParent)
    {
        var unit = ResourceSystem.Instance.GetExampleHero(unitID);

        var spawned = Instantiate(unit.Prefab, transform.position, transform.rotation, transformParent);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = unit.BaseStats;

        spawned.SetStats(stats);

        Environment.Instance.RegisterUnit(unitID, spawned);
    }
}
