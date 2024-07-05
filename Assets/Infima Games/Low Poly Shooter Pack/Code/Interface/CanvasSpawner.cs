//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Player Interface.
    /// </summary>
    public class CanvasSpawner : MonoBehaviour
    {
        #region FIELDS SERIALIZED

        [Title(label: "Settings")]
        
        [Tooltip("Canvas prefab spawned at ui initing. Displays the player's user interface.")]
        [SerializeField]
        private GameObject canvasPrefab;
        
        [Tooltip("Quality settings menu prefab spawned at ui initing. Used for switching between different quality settings in-game.")]
        [SerializeField]
        private GameObject qualitySettingsPrefab;

        [Tooltip("Quality settings menu prefab spawned at ui initing. Used for showing player stats in-game.")]
        [SerializeField]
        private GameObject _playerUIStats;
        #endregion

        #region UNITY

        /// <summary>
        /// Awake.
        /// </summary>
        private void Awake()
        {
            //Spawn Interface.
            Instantiate(canvasPrefab, transform);
            //Spawn Quality Settings Menu.
            Instantiate(qualitySettingsPrefab, transform);

            Instantiate(_playerUIStats, transform);
        }

        #endregion
    }
}