
using System.Collections;
using UnityEngine;

public class LevelUnitManager : StaticInstance<LevelUnitManager>
{

    [SerializeField] private int SpawnRateInWaveProgress;
    [SerializeField] private int SpawnRateInWaveCooldown;
    [Space]
    [SerializeField] private int WaveCount;
    [SerializeField, Range(5, 20)] private int enemyInOneWave;

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
        WaveManager.OnAfterWaveStateChanged += OnWaveHasStarted;
        WaveManager.OnAfterWaveStateChanged += OnWaveHasEnded;
        Environment.Instance.Player.OnHeroDeath += OnPlayerDied;
    }

    private UnitBase SpawnUnit(int unitID, Transform transform, Transform transformParent)
    {
        if (Environment.Instance.MaxSpawnedEnemy <= Environment.Instance.CurrentEnemyAlive)
            return null;

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
        for (int i = 0; i < enemyInOneWave; i++)
        {
            var enemy = SpawnUnit(0, Environment.Instance.GetRandomEnemySpawnPoint(), Environment.Instance.EnemyParent);
            yield return new WaitForSeconds(SpawnRateInWaveProgress);
        }
    }

    private void OnWaveHasStarted(WaveManager.WaveState waveState)
    {
        if (waveState == WaveManager.WaveState.WaveInProgress)
            _spawnCorotine = StartCoroutine(SpawnEnemyWaveCaroutinre());
    }
    private void OnWaveHasEnded(WaveManager.WaveState waveState)
    {
        if (waveState == WaveManager.WaveState.WaveOnCooldown)
            if (_spawnCorotine != null)
                StopCoroutine(_spawnCorotine);
    }
    private void OnPlayerDied(HeroUnitBase hero)
    {
        StopCoroutine(_spawnCorotine);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
