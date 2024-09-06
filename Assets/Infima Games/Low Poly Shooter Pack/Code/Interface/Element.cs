//Copyright 2022, Infima Games. All Rights Reserved.

using UnityEngine;

namespace InfimaGames.LowPolyShooterPack.Interface
{
    /// <summary>
    /// Interface Element.
    /// </summary>
    public abstract class Element : MonoBehaviour
    {
        #region FIELDS
        
        /// <summary>
        /// Game Mode Service.
        /// </summary>
        protected IGameModeService gameModeService;
        
        /// <summary>
        /// Player Character.
        /// </summary>
        protected CharacterBehaviour characterBehaviour;
        /// <summary>
        /// Player Character Inventory.
        /// </summary>
        protected InventoryBehaviour inventoryBehaviour;

        /// <summary>
        /// Equipped Weapon.
        /// </summary>
        protected WeaponBehaviour equippedWeaponBehaviour;

        private bool isInited = false;

        #endregion

        #region UNITY
        /// <summary>
        /// Start.
        /// </summary>
        
        private void OnEnable()
        {
            LevelGameManager.OnAfterStateChanged +=  OnInitingUI;
        }
        private void OnDisable()
        {
            LevelGameManager.OnAfterStateChanged -= OnInitingUI;
        }
        /// <summary>
        /// Initing UI.
        /// </summary>
        protected virtual void OnInitingUI(LevelGameState gameState)
        {
            if (gameState != LevelGameState.InitUI)
                return;

            //Get Game Mode Service. Very useful to get Game Mode references.
            gameModeService = ServiceLocator.Current.Get<IGameModeService>();
            
            //Get Player Character.
            characterBehaviour = gameModeService.GetPlayerCharacter();
            //Get Player Character Inventory.
            inventoryBehaviour = characterBehaviour.GetInventory();
        }
        
        /// <summary>
        /// Update.
        /// </summary>
        private void Update()
        {
            if (LevelGameManager.Instance.State == LevelGameState.GameInProgress)
            {
                if (Equals(inventoryBehaviour, null))
                    return;

                equippedWeaponBehaviour = inventoryBehaviour.GetEquipped();

                Tick();
            }
        }

        #endregion

        #region METHODS

        /// <summary>
        /// Tick.
        /// </summary>
        protected virtual void Tick() {}

        #endregion
    }
}