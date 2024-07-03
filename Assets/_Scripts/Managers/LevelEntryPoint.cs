using UnityEngine;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private Transform _levelSpawnPoint;
    [SerializeField] private Transform _levelParent;
    private LevelData levelData;

    private Level _level;
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
    }

    private void SpawnLevel()
    {
        var levelPref = Resources.Load<Level>("MapPref/" + "Level_" + levelData.ChoosedMapID.ToString());

        _level  = Instantiate(levelPref, _levelSpawnPoint.position, _levelSpawnPoint.rotation, _levelParent);
    }

    private void SpawnPlayer()
    {

    }
    private void SpawnWeapon()
    {

    }
    private void InitUI()
    {

    }
}
