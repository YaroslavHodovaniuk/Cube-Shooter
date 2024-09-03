
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
        if (other.gameObject.tag == "SpawnPointGrop")
        {
            spawnGroups.Add(other.gameObject.GetComponent<SpawnGroup>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "SpawnPointGrop")
        {
            spawnGroups.Remove(other.gameObject.GetComponent<SpawnGroup>());
        }
    }
    public SpawnGroup GetSpawnGroup()
    {
        return  spawnGroups[Random.Range(0, spawnGroups.Count)];
    }
}
