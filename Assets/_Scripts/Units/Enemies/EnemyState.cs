using UnityEngine;
using UnityEngine.Events;

public abstract class EnemyState 
{
    public UnityAction OnStateEnter;
    public UnityAction OnStateStay;
    public UnityAction OnStateExit;

    protected EnemyStateMachine _enemyStateMachine;
    protected Animator _animator;

    public EnemyState(EnemyStateMachine enemyStateMachine)
    {
        OnStateEnter += EnterState;
        OnStateStay += StayState;
        OnStateExit += ExitState;
        _enemyStateMachine = enemyStateMachine;

        _animator = _enemyStateMachine.EnemyUnitBase.GetComponent<Animator>();
    }

    protected virtual void EnterState()
    {

    }
    protected virtual void ExitState()
    {

    }
    protected virtual void StayState() 
    { 
    
    }
}
