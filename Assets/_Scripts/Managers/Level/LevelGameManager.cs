using InfimaGames.LowPolyShooterPack.Interface;
using System;
using UnityEngine;
using UnityEngine.Events;

public class LevelGameManager : StaticInstance<LevelGameManager>
{
    private LevelData levelData;
    private Level _spawnedLevel;

    private float _gameInProgressStartTime;

    public static UnityAction<LevelGameState> OnBeforeStateChanged;
    public static UnityAction<LevelGameState> OnAfterStateChanged;
    public static UnityAction OnLevelDestroy;

    public LevelGameState State { get; private set; }
    public LevelData LevelData => levelData;

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
            case LevelGameState.GameInProgress:
                HandleGameInProgress();
                break; 
            case LevelGameState.InitUI:
                HandleInitUI();
                break;
            case LevelGameState.GameEnded:
                HandleGameEnded();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
    }

    public float GetGameInProgressTimeDuration()
    {
        return Time.time - _gameInProgressStartTime;
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

        ChangeState(LevelGameState.InitUI);
    }

    private void HandleInitUI()
    {
        CanvasSpawner.Instance.OnUIInit();

        ChangeState(LevelGameState.GameInProgress);
    }
    private void HandleGameInProgress()
    {
        Environment.Instance.Player.OnHeroDeath += OnPlayerDied;
        _gameInProgressStartTime = Time.time;
        WaveManager.Instance.StartWaveChangingProcess();
        LevelUnitManager.Instance.SpawningEnemies();
        
    }
    private void HandleGameEnded()
    {
        
    }
    private void OnPlayerDied(HeroUnitBase hero)
    {
        ChangeState(LevelGameState.GameEnded);
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
    InitUI = 3,
    GameInProgress = 4,
    GameEnded = 5,
}