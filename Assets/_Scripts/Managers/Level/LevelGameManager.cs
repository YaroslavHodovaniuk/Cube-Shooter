using System;
using UnityEngine;

public class LevelGameManager : StaticInstance<LevelGameManager>
{
    private LevelData levelData;
    private Level _spawnedLevel;

    public static event Action<LevelGameState> OnBeforeStateChanged;
    public static event Action<LevelGameState> OnAfterStateChanged;

    public LevelGameState State { get; private set; }

    // Kick the game off with the first state
    void Start() => ChangeState(LevelGameState.Starting);

    public void ChangeState(LevelGameState newState)
    {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState)
        {
            case LevelGameState.Starting:
                HandleStarting();
                break;
            case LevelGameState.SpawningHero:
                HandleSpawningHeroes();
                break;
            case LevelGameState.SpawningEnemies:
                HandleSpawningEnemies();
                break;
            case LevelGameState.GameInProgress:
                HandleGameInProgress();
                break; 
            case LevelGameState.InitUI:
                HandleInitUI();
                break;
            case LevelGameState.Win:
                break;
            case LevelGameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);

        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting()
    {
        levelData = Systems.Instance.LevelData;
        //spawn level
        var level = Resources.Load<GameObject>($"MapPref/Level_" + levelData.ChoosedMapID);
        var levelRoot = Environment.Instance.transform.GetChild(1);
        _spawnedLevel = Instantiate(level, levelRoot.position, levelRoot.rotation, levelRoot).GetComponent<Level>();

        ChangeState(LevelGameState.SpawningHero);
    }

    private void HandleSpawningHeroes()
    {
        LevelUnitManager.Instance.SpawnPlayer();

        ChangeState(LevelGameState.SpawningEnemies);
    }

    private void HandleSpawningEnemies()
    {
        LevelUnitManager.Instance.SpawningEnemies();

        ChangeState(LevelGameState.InitUI);
    }

    private void HandleInitUI()
    {

    }
    private void HandleGameInProgress()
    {

    }

}

/// <summary>
/// This is obviously an example and I have no idea what kind of game you're making.
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum LevelGameState
{
    Starting = 0,
    SpawningHero = 1,    
    SpawningEnemies = 2,
    InitUI = 3,
    GameInProgress = 4,
    Win = 5,
    Lose = 6,
}