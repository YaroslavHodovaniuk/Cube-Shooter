
using System.Collections;
using UnityEngine;

public class LevelUnitManager : StaticInstance<LevelUnitManager>
{

    private float SpawnRateProgress = 0.1f;
    private float SpawnRateCooldown = 5f;
    [Space]
    [SerializeField] private int _waveCount;
    [SerializeField] private float _baseEnemyHP;
    [SerializeField, Range(5, 20)] private int enemyInOneWave;

    public int MaxWaveCount => _waveCount;
    public float BaseEnemyHP => _baseEnemyHP;
    private Coroutine _spawnCorotine;

    public void SpawnPlayer()
    {
        SpawnUnit(1, Environment.Instance.PlayerSpawnPoint, Environment.Instance.PlayerParent);
    }

    public void SpawningEnemies()
    {
        if (SpawnRateProgress <= 0 || SpawnRateCooldown <= 0)
        {
            Debug.LogWarning("SpawnRateProgress or SpawnRateCooldown less then zero");
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
        for (; ; )
        {
            var enemy = SpawnUnit(0, Environment.Instance.GetRandomEnemySpawnPoint(), Environment.Instance.EnemyParent);
            yield return new WaitForSeconds(SpawnRateProgress);
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
