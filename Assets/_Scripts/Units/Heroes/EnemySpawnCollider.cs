
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnCollider : MonoBehaviour
{
    private SphereCollider collider;

    private List<SpawnGroup> spawnGroups = new();

    public void Init()
    {
        collider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<SpawnGroup>() != null)
        {
            spawnGroups.Add(other.gameObject.GetComponent<SpawnGroup>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SpawnGroup>() != null)
        {
            var spawnGroup = spawnGroups.Find(r => other.gameObject.name == r.gameObject.name);
            spawnGroups.Remove(spawnGroup);
        }
    }

    public SpawnGroup GetSpawnGroup()
    {
        return  spawnGroups[Random.Range(0, spawnGroups.Count)];
    }
}
