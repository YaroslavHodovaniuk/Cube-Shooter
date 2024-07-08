using UnityEngine;

public class ChasingState : EnemyState
{
    private HeroUnitBase _heroUnitBase;

    public ChasingState(EnemyStateMachine enemyStateMachine, HeroUnitBase heroUnitBase) : base(enemyStateMachine)
    {
        _heroUnitBase = heroUnitBase;
    }

    protected override void EnterState()
    {
        base.EnterState();
        //animator
        Debug.Log("Wait, I want to speak with u <3");
    }
    protected override void StayState() 
    { 
        if (Vector3.Distance(_heroUnitBase.transform.position, _enemyStateMachine.EnemyUnitBase.transform.position) < _enemyStateMachine.EnemyUnitBase.Stats.AttackDistance)
        {
            _enemyStateMachine.ChangeStateToAttack();
        }

        if (Vector3.Distance(_heroUnitBase.transform.position, _enemyStateMachine.EnemyUnitBase.transform.position) > _enemyStateMachine.EnemyUnitBase.Stats.AngryDistance)
        {
            _enemyStateMachine.ChangeStateToIdle();
        }
    }
}
