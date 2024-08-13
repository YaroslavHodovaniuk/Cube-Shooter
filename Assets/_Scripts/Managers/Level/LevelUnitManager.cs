
using System.Collections;
using UnityEngine;

public class LevelUnitManager : StaticInstance<LevelUnitManager>
{

    [SerializeField] private int SpawnRateInWaveProgress;
    [SerializeField] private int SpawnRateInWaveCooldown;
    [Space]
    [SerializeField] private int WaveCount;

    private Coroutine _spawnCorotine;

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
        _spawnCorotine = StartCoroutine(SpawnEnemyWaveCaroutinre());
        Environment.Instance.Player.OnHeroDeath += OnPlayerDied;
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
            if (WaveManager.Instance.CurrentState == WaveManager.WaveState.WaveInProgress)
            {
                var enemy = SpawnUnit(0, Environment.Instance.GetRandomEnemySpawnPoint(), Environment.Instance.EnemyParent);
                yield return new WaitForSeconds(SpawnRateInWaveProgress);
            }
            else
            {
                if (WaveManager.Instance.IsSpawnOnlyInWaveProgress)
                {
                    var enemy = SpawnUnit(0, Environment.Instance.GetRandomEnemySpawnPoint(), Environment.Instance.EnemyParent);
                    yield return new WaitForSeconds(SpawnRateInWaveCooldown);
                }   
                else
                    yield return null;
            }
                
        }
    }

    private void OnPlayerDied(HeroUnitBase hero)
    {
        StopCoroutine(_spawnCorotine);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
