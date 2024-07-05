using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This will share logic for any unit on the field. Could be friend or foe, controlled or not.
/// Things like taking damage, dying, animation triggers etc
/// </summary>
public abstract class UnitBase : MonoBehaviour {

    public Stats Stats { get; protected set; }
    public virtual void SetStats(Stats stats) 
    {  
        Stats = stats;
    }

    
    public virtual void TakeDamage(int dmg) {
        var stats = Stats;
        stats.Health -= dmg;
        Stats = stats;
    }
}