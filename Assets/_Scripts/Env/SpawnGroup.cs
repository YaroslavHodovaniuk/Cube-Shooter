using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour
{
    [SerializeField] private List<Transform> transforms;

    public Transform GetSpawnPoint()
    {
        return transforms[Random.Range(0, transforms.Count)];
    }
}
