using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Events;

public class UpdateGameLogicOnDeath : FsmStateAction
{
    public UnityAction OnEnemyDeath;

    public override void OnEnter()
    {
        base.OnEnter();
        OnEnemyDeath?.Invoke();
    }
}
