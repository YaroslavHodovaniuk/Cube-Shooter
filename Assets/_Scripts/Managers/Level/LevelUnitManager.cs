
using System.Collections;
using UnityEngine;

public class LevelUnitManager : StaticInstance<LevelUnitManager>
{

    [SerializeField] private int SpawnRateInWaveProgress;
    [SerializeField] private int SpawnRateInWaveCooldown;
    [Space]
    [SerializeField] private int WaveCount;

    public void SpawnPlayer()
    {
        SpawnUnit(1, Environment.Instance.PlayerSpawnPoint, Environment.Instance.PlayerParent);
    }

    public void SpawningEnemies()
    {
        if (SpawnRateInWaveProgress <= 0 || SpawnRateInWaveCooldown <= 0)
        {
            Debug.LogWarning("SpawnRateInWaveProgress or SpawnRateInWaveCooldown less then zero");
            return;
        }
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
            var enemy = SpawnUnit(0, Environment.Instance.GetRandomEnemySpawnPoint(), Environment.Instance.EnemyParent);


            if (WaveManager.Instance.CurrentState == WaveManager.WaveState.WaveInProgress)
                yield return new WaitForSeconds(SpawnRateInWaveProgress);
            else
                yield return new WaitForSeconds(SpawnRateInWaveCooldown);
        }
    }
}
