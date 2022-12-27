using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using PEC3.Controllers;
using PEC3.Entities;
using PEC3.Managers;

namespace PEC3.States
{
    /// <summary>
    /// Class <c>ShotsFired</c> contains the logic for the ShotsFired state.
    /// </summary>
    public class ShotsFired : State
    {
        /// <value>Property <c>_currentPlayer</c> represents the current player.</value>
        private Player _currentPlayer;
        
        /// <value>Property <c>_playerController</c> represents the player controller.</value>
        private PlayerController _playerController;
        
        /// <value>Property <c>_playerInput</c> represents the player input.</value>
        private PlayerInput _playerInput;

        /// <value>Property <c>_projectile</c> represents the projectile game object.</value>
        private GameObject _projectile;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameManager">The gameManager instance</param>
        public ShotsFired(GameManager gameManager) : base(gameManager)
        {
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        public override IEnumerator Start()
        {
            // Get the current player
            _currentPlayer = GameManager.GetCurrentPlayer();
            _playerController = _currentPlayer.GameObject.GetComponent<PlayerController>();
            _playerInput = _currentPlayer.GameObject.GetComponent<PlayerInput>();
            _projectile = GameObject.FindGameObjectWithTag("Projectile");
            
            // Disable the player input
            _playerController.enabled = false;
            _playerInput.enabled = false;
            
            // Make virtual camera follow the projectile
            GameManager.virtualCamera.Follow = _projectile.transform;

            yield break;
        }
    }
}
