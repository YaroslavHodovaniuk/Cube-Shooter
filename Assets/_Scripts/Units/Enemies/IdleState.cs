using UnityEngine;

public class IdleState : EnemyState
{
    private HeroUnitBase _heroUnitBase;
    public IdleState(EnemyStateMachine enemyStateMachine, HeroUnitBase heroUnitBase) : base(enemyStateMachine)
    {
        _heroUnitBase = heroUnitBase;
    }

    protected override void EnterState()
    {
        base.EnterState();
        //animator
        Debug.Log("I like my life");
    }
    protected override void StayState()
    {
        // Calculate the distance between the hero and the enemy
        float distance = Vector3.Distance(_heroUnitBase.transform.position, _enemyStateMachine.EnemyUnitBase.transform.position);
        // Check if the distance is less than the enemy's angry distance threshold
        if (distance < _enemyStateMachine.EnemyUnitBase.Stats.AngryDistance) { 
            // Change the enemy's state to chasing
            _enemyStateMachine.ChangeStateToChasing();
        }

    }
}
