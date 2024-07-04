using UnityEngine;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _levelSpawnPoint;
    [SerializeField] private Transform _levelParent;
    [SerializeField] private Transform _playerParent;
    private Transform _weaponParent;

    private LevelData levelData;
    private PlayerRoot playerRoot;
    private Level _spawnedLevel;
    private void OnEnable()
    {
        levelData = Systems.Instance.LevelData;
    }
    private void OnDisable()
    {
        levelData = null;
    }
    private void Start()
    {
        SpawnLevel();
        SpawnPlayer();
        SpawnWeapon();
    }

    private void SpawnLevel()
    {
        var levelPref = Resources.Load<Level>("MapPref/" + "Level_" + levelData.ChoosedMapID.ToString());

        _spawnedLevel  = Instantiate(levelPref, _levelSpawnPoint.position, _levelSpawnPoint.rotation, _levelParent);
    }

    private void SpawnPlayer()
    {
        var playerobj = Instantiate(levelData.PlayerPref, _spawnedLevel.PlayerSpawnerPoint.position, _spawnedLevel.PlayerSpawnerPoint.rotation, _playerParent);
        playerRoot = playerobj.GetComponent<PlayerRoot>();
        _weaponParent = playerRoot.WeaponSetRoot;
    }
    private void SpawnWeapon()
    {
        var weaponPref = Resources.Load<GameObject>("WeaponPref/" + "Weapon_" + levelData.ChoosedWeaponID.ToString());

        Instantiate(weaponPref, _weaponParent.position, _weaponParent.rotation, _weaponParent);
    }
    private void InitUI()
    {

    }
}
