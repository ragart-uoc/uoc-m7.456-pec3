using UnityEngine;
using UnityEngine.InputSystem;
using PEC3.Managers;
using PEC3.States;

namespace PEC3.Controllers
{
    /// <summary>
    /// Class <c>PlayerController</c> contains the methods and properties needed to control the player.
    /// </summary>
    public class PlayerController : MonoBehaviour
    {
        /// <value>Property <c>projectilePrefab</c> represents the projectile prefab.</value>
        public GameObject projectilePrefab;

        /// <value>Property <c>MinPower</c> represents the minimum power of the projectile.</value>
        private const float MinPower = 0;
        
        /// <value>Property <c>MaxPower</c> represents the maximum power of the projectile.</value>
        private const float MaxPower = 50;
        
        /// <value>Property <c>_currentPower</c> represents the current power of the projectile.</value>
        private float _currentPower;
        
        /// <value>Property <c>_isCHharging</c> defines if the player is charging the projectile.</value>
        private bool _isCharging;

        /// <value>Property <c>_gameManager</c> represents the GameManager instance.</value>
        private GameManager _gameManager;
        
        /// <value>Property <c>movingSpeed</c> defines the initial speed of the player.</value>
        [SerializeField] private float movingSpeed = 8f;
        
        /// <value>Property <c>jumpHeight</c> defines the jump force of the player.</value>
        [SerializeField] private float jumpHeight = 10f;

        /// <value>Property <c>_transform</c> represents the RigidBody2D component of the player.</value>
        private Transform _transform;
        
        /// <value>Property <c>_body</c> represents the RigidBody2D component of the player.</value>
        private Rigidbody2D _body;

        /// <value>Property <c>_renderer</c> represents the SpriteRenderer component of the player.</value>
        private SpriteRenderer _renderer;

        /// <value>Property <c>_animator</c> represents the Animator component of the player.</value>
        private Animator _animator;
        
        /// <value>Property <c>_inputX</c> represents the horizontal input of the player.</value>
        private float _inputX;
        
        /// <value>Property <c>_inputY</c> represents the vertical input of the player.</value>
        private float _inputY;

        /// <value>Property <c>AnimatorIsMoving</c> preloads the Animator isMoving parameter.</value>
        private static readonly int AnimatorIsMoving = Animator.StringToHash("isMoving");
        
        /// <value>Property <c>AnimatorIsJumping</c> preloads the Animator isJumping parameter.</value>
        private static readonly int AnimatorIsJumping = Animator.StringToHash("isJumping");

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _transform = transform;
            _body = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
        }
        
        /// <summary>
        /// Method <c>Update</c> is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        private void Update()
        {
            _body.velocity = new Vector2(_inputX * movingSpeed, _body.velocity.y);
            _animator.SetBool(AnimatorIsMoving, _inputX != 0);
            
            // Increase power while charging
            if (_isCharging)
            {
                _currentPower += (_currentPower < MaxPower) ? 0.5f : 0f;
            }
        }

        /// <summary>
        /// Method <c>Move</c> moves the player.
        /// </summary>
        /// <param name="context">The CallbackContext of the InputAction</param>
        public void Move(InputAction.CallbackContext context)
        {
            _inputX = context.ReadValue<Vector2>().x;
            _renderer.flipX = _inputX switch
            {
                > 0 => false,
                < 0 => true,
                _ => _renderer.flipX
            };
        }
        
        /// <summary>
        /// Method <c>Jump</c> makes the player jump.
        /// </summary>
        /// <param name="context">The CallbackContext of the InputAction</param>
        public void Jump(InputAction.CallbackContext context)
        {
            if (!context.performed || !IsGrounded()) return;
            _body.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            _animator.SetBool(AnimatorIsJumping, true);
        }

        /// <summary>
        /// Method <c>Fire</c> makes the player shoot.
        /// </summary>
        /// <param name="context">The CallbackContext of the InputAction</param>
        public void Fire(InputAction.CallbackContext context)
        {
            // Calculate power depending on hold duration
            if (context.started)
            {
                _currentPower = MinPower;
                _isCharging = true;
            }
            else if (context.performed)
            {
                // Stop charging
                _isCharging = false;
                // Get position depending on device
                Vector3 aimPosition;
                switch (context.control.device)
                {
                    case Mouse:
                        aimPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                        break;
                    case Touchscreen:
                        aimPosition = Camera.main.ScreenToWorldPoint(Touchscreen.current.primaryTouch.position.ReadValue());
                        break;
                    default:
                        return;
                }
                // Get player position
                var playerPosition = _transform.position;
                // Calculate direction
                var aimDirection = (aimPosition - playerPosition).normalized;
                aimDirection.y = (aimDirection.y < 0.5f) ? 0.5f : aimDirection.y;
                // Flip player if needed
                _renderer.flipX = aimDirection.x < 0f;
                // Define a offset from the player for the projectile
                var projectileOffset = new Vector3
                {
                    x = (aimDirection.x > 0) ? 1f : -1f,
                    y = aimDirection.y
                };
                // Shoot
                Shoot(playerPosition, projectileOffset, _currentPower, aimDirection);
                // Reset power
                _currentPower = MinPower;
                // Set GameManager state to ShotsFired
                _gameManager.SetState(new ShotsFired(_gameManager));
            }
        }
        
        public void Shoot(Vector3 position, Vector3 offset, float power, Vector3 direction)
        {
            // Instantiate projectile with an offset from the player
            var projectile = Instantiate(projectilePrefab, position + offset, Quaternion.identity);
            // Add force to projectile
            projectile.GetComponent<Rigidbody2D>().AddForce(direction * power, ForceMode2D.Impulse);
        }
        
        /// <summary>
        /// Method <c>OnCollisionEnter2D</c> is sent when an incoming collider makes contact with this object's collider.
        /// </summary>
        /// <param name="collision">The collision instance</param>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground") && IsGrounded())
            {
                _animator.SetBool(AnimatorIsJumping, false);
            }
        }
        
        /// <summary>
        /// Method <c>OnCollisionStay2D</c> is sent each frame where a collider on another object is touching this object's collider.
        /// </summary>
        /// <param name="collision">The collision instance</param>
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground") && IsGrounded())
            {
                _animator.SetBool(AnimatorIsJumping, false);
            }
        }
        
        /// <summary>
        /// Method <c>IsGrounded</c> check if the player is touching the ground.
        /// </summary>
        /// <returns>Boolean</returns>
        private bool IsGrounded()
        {
            Vector2 position = _transform.position;
            var direction = Vector2.down;
            var distance = _transform.localScale.y / 2 + 0.1f;

            Debug.DrawRay(position, direction, Color.green);
            var groundLayer = LayerMask.GetMask("Ground");
            var hit = Physics2D.Raycast(position, direction, distance, groundLayer);
            return hit.collider != null;
        }
    }
}
