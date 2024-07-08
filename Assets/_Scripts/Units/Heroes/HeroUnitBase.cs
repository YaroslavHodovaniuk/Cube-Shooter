using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HeroUnitBase : UnitBase {

    public UnityAction<HeroUnitBase> OnStatsUpdated;

    //private IEnumerator auch()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        TakeDamage(1);
    //        yield return new WaitForSeconds(1);
    //    }
    //}
    public override void SetStats(Stats stats)
    {
        base.SetStats(stats);
        //StartCoroutine(auch());
    }

    public override void TakeDamage(int dmg)
    {
        base.TakeDamage(dmg);
        OnStatsUpdated?.Invoke(this);
    }
}