
using System.Collections;
using UnityEngine;

public class LevelUnitManager : StaticInstance<LevelUnitManager>
{

    [SerializeField] private int SpawnDelay;

    [SerializeField] private int WaveCount;

    public void SpawnPlayer()
    {
        SpawnUnit(1, Environment.Instance.PlayerSpawnPoint, Environment.Instance.PlayerParent);
    }

    public void SpawningEnemies()
    {
        StartCoroutine(SpawnEnemyWaveCaroutinre());
    }

    private UnitBase SpawnUnit(int unitID, Transform transform, Transform transformParent)
    {
        var unit = ResourceSystem.Instance.GetExampleHero(unitID);

        var spawned = Instantiate(unit.Prefab, transform.position, transform.rotation, transformParent);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = unit.BaseStats;

        spawned.SetStats(stats);

        Environment.Instance.RegisterUnit(spawned);
        return spawned;
    }
    

    private IEnumerator SpawnEnemyWaveCaroutinre()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int j = 0; j < Environment.Instance.EnemySpawnPoints.Count; j++)
            {
                var enemy = SpawnUnit(0, Environment.Instance.EnemySpawnPoints[j], Environment.Instance.EnemyParent);
            }
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}
