using HutongGames.PlayMaker;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class HeroUnitBase : UnitBase {

    public UnityAction<HeroUnitBase> OnStatsUpdated;

    public UnityAction<HeroUnitBase> OnHeroDeath;

    private PlayMakerFSM fsm;

    public override void SetStats(Stats stats)
    {
        base.SetStats(stats);

        OnStatsUpdated += CheckIsDead;

        fsm = transform.GetChild(0).GetComponent<PlayMakerFSM>();
        InitFSM();
    }

    public override void TakeDamage(int dmg)
    {
        if (LevelGameManager.Instance.State == LevelGameState.GameInProgress)
        {
            var stats = Stats;
            stats.Health -= dmg;
            Stats = stats;
            Debug.Log("Taken damage: " + dmg);

            OnStatsUpdated?.Invoke(this);
        }
    }

    public void AddScore(int score)
    {
        var stat = Stats;
        stat.Score += score; 
        Stats = stat;
    }

    private void InitFSM()
    {
        if (fsm == null)
        {
            Debug.LogWarning("Player FSM is null");
            return;
        }

        InitSetStatState();
        InitHitState();

    }
    private void InitSetStatState()
    {

        var state = fsm.FsmStates.ToList().Find(statetmp => statetmp.Name == "SetStat");

        if (state == null)
        {
            Debug.LogWarning("Player FSM state: |SetStat| is null");
            return;
        }
        var action = new SetHealthPointFSMAction(this);
        action.SetHealthPoint += SetFSMHealthPoint;
        state.Actions = new FsmStateAction[1] { action };
    }

    private void InitHitState()
    {
        var state = fsm.FsmStates.ToList().Find(statetmp => statetmp.Name == "Damage");

        if (state == null)
        {
            Debug.LogWarning("Player FSM state: |Damage| is null");
            return;
        }
        var action = new HitFSMAction(this);
        action.Hit += OnPlayerHit; 
        state.Actions = new FsmStateAction[1] { action };
    }
    private void SetFSMHealthPoint(HeroUnitBase hero)
    {
        var fsmHealthPoint = fsm.FsmVariables.FindFsmFloat("Player Health");
        if (fsmHealthPoint == null)
        {
            Debug.LogWarning("Player FSM var: |Player Health| is null");
            return;
        }
        fsmHealthPoint.Value = hero.Stats.MaxHealth;
    }

    private void OnPlayerHit(HeroUnitBase hero)
    {
        var fsmDamageAmount = fsm.FsmVariables.FindFsmInt("Damage Amount");
        if (fsmDamageAmount == null)
        {
            Debug.LogWarning("Player FSM var: |Damage Amount| is null");
            return;
        }
        TakeDamage(fsmDamageAmount.Value);
    }

    private void CheckIsDead(HeroUnitBase hero)
    {
        if (hero == null)
        {
            Debug.Log("CheckIsDead hero is null");
            return ;
        }

        if (hero.Stats.Health <= 0)
        {
            hero.OnHeroDeath?.Invoke(hero);
        }
    }

    public void UpdateMoneyCountOnDeath(int val)
    {
        int prevValue = PlayerPrefs.GetInt("PlayerBalance");
        PlayerPrefs.SetInt("PlayerBalance", prevValue + val);
    }
}

public class SetHealthPointFSMAction : FsmStateAction
{
    private HeroUnitBase _owner;

    public UnityAction<HeroUnitBase> SetHealthPoint;

    public SetHealthPointFSMAction(HeroUnitBase unitBase)
    {
        _owner = unitBase;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        SetHealthPoint?.Invoke(_owner);
    }

}

public class HitFSMAction : FsmStateAction
{
    private HeroUnitBase heroUnitBase;

    public UnityAction<HeroUnitBase> Hit;

    public HitFSMAction(HeroUnitBase hero)
    {
        heroUnitBase = hero;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        Hit?.Invoke(heroUnitBase);
    }
}