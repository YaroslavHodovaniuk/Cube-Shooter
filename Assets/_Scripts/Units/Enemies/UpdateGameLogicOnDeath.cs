using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.Events;

public class UpdateGameLogicOnDeath : FsmStateAction
{
    public EnemyUnitBase _owner;

    public UnityAction<EnemyUnitBase> OnEnemyDeath;
    public UpdateGameLogicOnDeath(EnemyUnitBase owner)
    {
        _owner = owner;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        OnEnemyDeath?.Invoke(_owner);
    }
}
