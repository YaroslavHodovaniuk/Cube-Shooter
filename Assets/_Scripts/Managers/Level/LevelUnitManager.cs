using HutongGames.PlayMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

    void SpawnUnit(int unitID, Transform transform, Transform transformParent)
    {
        var unit = ResourceSystem.Instance.GetExampleHero(unitID);

        var spawned = Instantiate(unit.Prefab, transform.position, transform.rotation, transformParent);

        // Apply possible modifications here such as potion boosts, team synergies, etc
        var stats = unit.BaseStats;

        spawned.SetStats(stats);

        var FSMs = spawned.GetComponentsInChildren<PlayMakerFSM>();
        if (FSMs.Length == 0)
        {
            Debug.LogWarning("No PlayMakerFSM components found in parent objects.");
            
        }
        else
        {
            PlayMakerFSM damageSystem = null;
            for (int i = 0; i < FSMs.Length; i++)
            {
                if (FSMs[i].Fsm.Name == "Damage System")
                {
                    damageSystem = FSMs[i];
                }
            }
            var states = damageSystem.Fsm.States;
            foreach (var state in states)
            {
                if (state.Fsm.Name == "Idle")
                {
                    Debug.Log("pidaras");
                    var onEnemyDeathAction = new UpdateGameLogicOnDeath();
                    onEnemyDeathAction.OnEnemyDeath += OnEnemyDeath;
                    // Convert the Actions list to a standard list if necessary
                    var actionsList = new System.Collections.Generic.List<FsmStateAction>(state.Actions);
                    actionsList.Add(onEnemyDeathAction);
                    // Assign the modified list back to the state
                    state.Actions = actionsList.ToArray(); // Convert back to array if needed
                }
            }
            Environment.Instance.RegisterUnit(unitID, spawned);
        }
    }
    
    private void OnEnemyDeath()
    {
        Debug.Log("pidar");
    }

    private IEnumerator SpawnEnemyWaveCaroutinre()
    {
        for (int i = 0; i < WaveCount; i++)
        {
            for (int j = 0; j < Environment.Instance.EnemySpawnPoints.Count; j++)
            {
                SpawnUnit(0, Environment.Instance.EnemySpawnPoints[j], Environment.Instance.EnemyParent);
            }
            yield return new WaitForSeconds(SpawnDelay);
        }
    }
}
