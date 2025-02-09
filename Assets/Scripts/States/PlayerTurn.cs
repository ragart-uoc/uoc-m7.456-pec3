using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using PEC3.Controllers;
using PEC3.Entities;
using PEC3.Managers;

namespace PEC3.States
{
    /// <summary>
    /// Class <c>PlayerTurn</c> contains the logic for the PlayerTurn state.
    /// </summary>
    public class PlayerTurn : State
    {
        /// <value>Property <c>_currentPlayer</c> represents the current player.</value>
        private Player _currentPlayer;
        
        /// <value>Property <c>_playerRenderer</c> represents the player renderer.</value>
        private SpriteRenderer _playerRenderer;
        
        /// <value>Property <c>_playerController</c> represents the player controller.</value>
        private PlayerController _playerController;
        
        /// <value>Property <c>_playerInput</c> represents the player input.</value>
        private PlayerInput _playerInput;

        /// <value>Property <c>_timeLeft</c> represents the time left in the turn.</value>
        private float _timeLeft;

        /// <value>Property <c>_timerOn</c> represents whether the timer is on.</value>
        private bool _timerOn;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="gameManager">The gameManager instance</param>
        public PlayerTurn(GameManager gameManager) : base(gameManager)
        {
        }
        
        /// <summary>
        /// Method <c>Start</c> is called on the frame when a script is enabled just before any of the Update methods are called the first time.
        /// </summary>
        public override IEnumerator Start()
        {
            // Get the current player
            _currentPlayer = GameManager.GetCurrentPlayer();
            _playerRenderer = _currentPlayer.GameObject.GetComponent<SpriteRenderer>();
            _playerController = _currentPlayer.GameObject.GetComponent<PlayerController>();
            _playerInput = _currentPlayer.GameObject.GetComponent<PlayerInput>();

            // Make virtual camera follow the current player
            GameManager.virtualCamera.Follow = _currentPlayer.GameObject.transform;
            
            // Show the player information
            GameManager.playerNameText.text = _currentPlayer.Identifier + " - " + _currentPlayer.Name;
            GameManager.playerLivesText.text = "Lives: " + _currentPlayer.Health.ToString();
            GameManager.playerInfoGroup.alpha = 1;
            
            // Show the turn message
            GameManager.generalText.text = "Your turn, " + _currentPlayer.Identifier;
            GameManager.generalText.canvasRenderer.SetAlpha(1.0f);
            yield return new WaitForSeconds(1.5f);
            GameManager.generalText.canvasRenderer.SetAlpha(0.0f);
            yield return new WaitForSeconds(1.5f);
            
            // Set the time left
            _timeLeft = GameManager.TurnTime;
            UpdateTimeText();
            GameManager.generalText.canvasRenderer.SetAlpha(1.0f);
            
            // If the player is human, enable the player controls
            if (!_currentPlayer.IsCPU)
            {
                _playerController.enabled = true;
                _playerInput.enabled = true;
            }
            
            // Start the timer
            _timerOn = true;
            
            // If the player is controlled by the CPU, find the closest player
            if (_currentPlayer.IsCPU)
            {
                
                // Get the closest player
                Player targetPlayer = null;
                var minDistance = Mathf.Infinity;
                foreach (var player in GameManager.Players.Where(player => player.Value.Identifier != _currentPlayer.Identifier))
                {
                    var distance = Vector3.Distance(_currentPlayer.GameObject.transform.position, player.Value.GameObject.transform.position);
                    if (!(distance < minDistance)) continue;
                    minDistance = distance;
                    targetPlayer = player.Value;
                }
                
                // Calculate the direction and the force required to hit the target
                var playerPosition = _currentPlayer.GameObject.transform.position;
                var aimDirection = (targetPlayer.GameObject.transform.position - playerPosition).normalized;
                aimDirection.y = (aimDirection.y < 0.5f) ? 0.5f : aimDirection.y;
                var aimForce = (aimDirection * 10f).magnitude;

                // Flip player if needed
                _playerRenderer.flipX = aimDirection.x < 0f;
                
                // Instantiate projectile with an offset from the player
                var projectileOffset = new Vector3
                {
                    x = (aimDirection.x > 0) ? 1f : -1f,
                    y = aimDirection.y
                };
                
                // Shoot
                _playerController.Shoot(playerPosition, projectileOffset, aimForce, aimDirection);

                // Set GameManager state to ShotsFired
                GameManager.SetState(new ShotsFired(GameManager));
            }
        }
        
        /// <summary>
        /// Method <c>FixedUpdate</c> is called every fixed frame-rate frame.
        /// </summary>
        public override void FixedUpdate()
        {
            if (_timerOn)
            {
                if (_timeLeft >= 0)
                {
                    _timeLeft -= Time.deltaTime;
                    UpdateTimeText();
                }
                else
                {
                    _timerOn = false;
                    _playerController.enabled = false;
                    _playerInput.enabled = false;
                    GameManager.SetNextPlayer();
                    GameManager.SetState(new PlayerTurn(GameManager));
                }
            }
        }

        /// <summary>
        /// Method <c>UpdateTimeText</c> updates the time information on the UI.
        /// </summary>
        private void UpdateTimeText()
        {
            var seconds = _timeLeft > 0 ? (int) _timeLeft : 0;
            GameManager.generalText.text = seconds.ToString();
        }
    }
}
