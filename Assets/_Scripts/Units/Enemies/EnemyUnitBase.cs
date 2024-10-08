
using HutongGames.PlayMaker;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyUnitBase : UnitBase
{
    [SerializeField] private float _despawnDistance;

    public UnityAction<EnemyUnitBase> DeathEvent = null;

    public override void SetStats(Stats stats)
    {
        Stats var = stats;

        var.MaxHealth = (int)LevelUnitManager.Instance.BaseEnemyHP * (1 << (WaveManager.Instance.WaveCount / (LevelUnitManager.Instance.MaxWaveCount / 12)));

        var.Health = var.MaxHealth;
        stats = var;    
        base.SetStats(stats);
        Debug.Log(stats.Health);

        var FSMs = transform.GetChild(0).GetComponents<PlayMakerFSM>();
        if (FSMs.Length == 0)
        {
            Debug.LogWarning("No PlayMakerFSM components found in parent objects.");

        }
        else
        {
            PlayMakerFSM damageSystem = null;
            for (int i = 0; i < FSMs.Length; i++)
            {
                if (FSMs[i].Fsm.Name == "Death")
                {
                    damageSystem = FSMs[i];
                    break;
                }
            }
            var states = damageSystem.Fsm.States;
            foreach (var state in states)
            {
                if (state.Name == "Idle")
                {
                    var onEnemyDeathAction = new UpdateGameLogicOnDeath(this);
                    onEnemyDeathAction.OnEnemyDeath += (EnemyUnitBase enemy) => AddScoreOnDeath();
                    onEnemyDeathAction.OnEnemyDeath += (EnemyUnitBase enemy) => InvokeDeathEvent();
                    
                    // Convert the Actions list to a standard list if necessary
                    var actionsList = new List<FsmStateAction>(state.Actions);
                    actionsList.Add(onEnemyDeathAction);
                    // Assign the modified list back to the state
                    state.Actions = actionsList.ToArray(); // Convert back to array if needed
                }
            }
        }
    }
    private void InvokeDeathEvent()
    {
        DeathEvent?.Invoke(this);
        Destroy(gameObject);
    }
    private void AddScoreOnDeath()
    {
        Environment.Instance.Player.AddScore(Stats.Score);
    }
    private void Update()
    {
        if (Vector3.Distance(Environment.Instance.Player.transform.GetChild(0).transform.position, transform.position) > _despawnDistance)
        {
            Environment.Instance.UnregisterUnit(this);
            Destroy(gameObject);
        }
    }
}
