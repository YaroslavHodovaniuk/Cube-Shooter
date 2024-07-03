using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Transform _playerSpawnPoint;

    public Transform PlayerSpawnerPoint => _playerSpawnPoint;
}
