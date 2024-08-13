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

        [SerializeField] private GameObject _endGamePanel;
       
        #endregion

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        public void OnUIInit()
        {
            _endGamePanel.SetActive(false);

            LevelGameManager.OnAfterStateChanged += SetActiveEndGamePanel;
        }


        private void SetActiveEndGamePanel(LevelGameState state)
        {
            if (state != LevelGameState.GameEnded)
                return;

            _endGamePanel.GetComponent<EndGameViewPanel>().Init();
            _endGamePanel.SetActive(true);
        }
        private void OnDestroy() {
            LevelGameManager.OnAfterStateChanged -= SetActiveEndGamePanel;
        }
        #endregion
    }
}