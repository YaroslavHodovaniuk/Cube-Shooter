using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public EnemyUnitBase EnemyUnitBase { get; private set; }

   
    private List<EnemyState> enemyStates = new List<EnemyState>();
    private EnemyState _currentState;
    public EnemyState CurrentState => _currentState;
    public void Init(EnemyUnitBase enemyUnitBase)
    {
        EnemyUnitBase = enemyUnitBase;
        var idleState = new IdleState(this, Environment.Instance.Player);
        var chasingState = new ChasingState(this, Environment.Instance.Player);
        var attackState = new AttackState(this, Environment.Instance.Player);

        enemyStates.Add(idleState);
        enemyStates.Add(chasingState);
        enemyStates.Add(attackState);
        ChangeStateToIdle();
    }
    public void ChangeStateToIdle()
    {
        ChangeState(enemyStates[0]);
    }
    public void ChangeStateToChasing()
    {
        ChangeState(enemyStates[1]);
    }
    public void ChangeStateToAttack()
    {
        ChangeState(enemyStates[2]);
    }

    private void ChangeState(EnemyState newState)
    {
        ExiteState();

        EnterState(newState);
    }

    private void Update()
    {
        if (CurrentState != null) 
        {
            CurrentState.OnStateStay?.Invoke();
        }
    }

    private void ExiteState()
    {
        if (_currentState != null)
        {
            CurrentState.OnStateExit?.Invoke();
            _currentState = null;
        }
    }
    private void EnterState(EnemyState newState)
    {
        _currentState = newState;
        CurrentState.OnStateEnter?.Invoke();
    }
}

