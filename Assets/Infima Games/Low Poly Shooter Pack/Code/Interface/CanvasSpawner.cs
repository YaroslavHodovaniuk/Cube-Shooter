//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Player Interface.
    /// </summary>
    public class CanvasSpawner : StaticInstance<CanvasSpawner>
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Canvas prefab spawned at ui initing. Displays the player's user interface.")]
        [SerializeField]
        private GameObject canvasPrefab;
        private GameObject canvasPrefabInst;
        
        [Tooltip("Quality settings menu prefab spawned at ui initing. Used for switching between different quality settings in-game.")]
        [SerializeField]
        private GameObject qualitySettingsPrefab;
        private GameObject qualitySettingsPrefabInst;

        [Tooltip("Quality settings menu prefab spawned at ui initing. Used for showing player stats in-game.")]
        [SerializeField]
        private GameObject _playerUIStats;
        private GameObject _playerUIStatsInst;

        [SerializeField] private GameObject _waveInfo;
        private GameObject _waveInfoInst;

        [Tooltip("Quality settings menu prefab spawned at ui initing. Used for showing player stats in-game.")]
        [SerializeField]
        private GameObject EndGamePanel;
        private GameObject EndGamePanelInst;

        #endregion

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        public void OnUIInit()
        {
            //Spawn Interface.
            canvasPrefabInst = Instantiate(canvasPrefab, transform);
            //Spawn Quality Settings Menu.
            //qualitySettingsPrefabInst = Instantiate(qualitySettingsPrefab, transform);

            _playerUIStatsInst = Instantiate(_playerUIStats, transform);

            _waveInfoInst = Instantiate(_waveInfo, transform);

            LevelGameManager.OnAfterStateChanged += SetActiveEndGamePanel;
            LevelGameManager.OnLevelDestroy += DestroyUI;
        }


        private void SetActiveEndGamePanel(LevelGameState state)
        {
            if (state != LevelGameState.GameEnded)
                return;

            EndGamePanelInst =  Instantiate(EndGamePanel, transform);
            EndGamePanelInst.SetActive(true);
            EndGamePanelInst.GetComponent<EndGameViewPanel>().Init();
        }

        private void DestroyUI()
        {
            Destroy(canvasPrefabInst);
            //Destroy(qualitySettingsPrefabInst);
            Destroy(_playerUIStatsInst);
            Destroy(_waveInfoInst);
            Destroy(EndGamePanelInst);
        }
        #endregion
    }
}