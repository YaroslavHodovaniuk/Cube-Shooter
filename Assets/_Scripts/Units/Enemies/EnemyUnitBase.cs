
using UnityEngine;

public class EnemyUnitBase : UnitBase
{
    private EnemyStateMachine stateMachine;

    public void OnEnable()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        stateMachine.Init(this);
    }

    public void MoveToTarget(Transform target)
    {

    }
}
