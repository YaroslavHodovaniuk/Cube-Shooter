
using HutongGames.PlayMaker;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyUnitBase : UnitBase
{
    public UnityAction<EnemyUnitBase> DeathEvent = null;

    public override void SetStats(Stats stats)
    {
        base.SetStats(stats);

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
                    DeathEvent = onEnemyDeathAction.OnEnemyDeath;
                    // Convert the Actions list to a standard list if necessary
                    var actionsList = new List<FsmStateAction>(state.Actions);
                    actionsList.Add(onEnemyDeathAction);
                    // Assign the modified list back to the state
                    state.Actions = actionsList.ToArray(); // Convert back to array if needed
                }
            }
        }
    }
    public void MoveToTarget(Transform target)
    {

    }

    private void AddScoreOnDeath()
    {
        Environment.Instance.Player.AddScore(Stats.Score);
    }
}
