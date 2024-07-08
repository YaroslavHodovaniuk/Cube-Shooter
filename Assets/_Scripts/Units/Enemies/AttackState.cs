using UnityEngine;

public class AttackState : EnemyState
{
    private float lastApplyAttackTime;

    private HeroUnitBase _hero = null;
    private EnemyStateMachine _stateMachine = null;
    public AttackState(EnemyStateMachine enemyStateMachine, HeroUnitBase hero) : base(enemyStateMachine)
    {
        _stateMachine = enemyStateMachine;
        _hero = hero; 
    }

    protected override void EnterState()
    {
        StartAttack();        
    }
    protected override void StayState()
    {
        base.StayState();
        if (Time.time - lastApplyAttackTime < _stateMachine.EnemyUnitBase.Stats.AttackSpeed)
        {
            StartAttack();
        }
        float distance = Vector3.Distance(_hero.transform.position, _enemyStateMachine.EnemyUnitBase.transform.position);
        // Check if the distance is less than the enemy's angry distance threshold
        if (distance > _enemyStateMachine.EnemyUnitBase.Stats.AttackDistance)
        {
            // Change the enemy's state to chasing
            _enemyStateMachine.ChangeStateToChasing();
        }
    }

    private void StartAttack()
    {
        lastApplyAttackTime = Time.time;

        //animator
        Debug.Log("attack!!!");
        _hero.TakeDamage(_stateMachine.EnemyUnitBase.Stats.Damage);
    }
}
