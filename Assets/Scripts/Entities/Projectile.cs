using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using PEC3.Managers;
using PEC3.States;

namespace PEC3.Entities
{
    /// <summary>
    /// Class <c>Projectile</c> represents a projectile instance.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        /// <value>Property <c>explosionPrefab</c> represents the prefab containing the explosion animation.</value>
        public GameObject explosionPrefab;
        
        /// <value>Property <c>_groundTilemap</c> represents the Tilemap for the ground.</value>
        private Tilemap _groundTilemap;
        
        /// <value>Property <c>_gameManager</c> represents the GameManager instance.</value>
        private GameManager _gameManager;
        
        /// <value>Property <c>_audiosource</c> represents the AudioSource for the projectile.</value>
        private AudioSource _audiosource;
        
        
        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            _groundTilemap = GameObject.Find("Ground").GetComponent<Tilemap>();
            _gameManager = FindObjectOfType<GameManager>();
            _audiosource = GetComponent<AudioSource>();
        }
        
        /// <summary>
        /// Method <c>OnCollisionEnter2D</c> is sent when an incoming collider makes contact with this object's collider.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player") || col.gameObject.CompareTag("Ground"))
            {
                StartCoroutine(Explode());
            }
        }
        
        /// <summary>
        /// Method <c>Explode</c> is called when the projectile collides with the player or the ground.
        /// </summary>
        private IEnumerator Explode()
        {
            var position = transform.position;
            
            // Stop the projectile from moving
            var rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            
            // Disable the projectile's sprite renderer
            var sr = GetComponent<SpriteRenderer>();
            sr.enabled = false;
            
            // Disable the projectile's collider
            var col = GetComponent<Collider2D>();
            col.enabled = false;

            // Show explosion effect
            Instantiate(explosionPrefab, position, Quaternion.identity);
            
            // Play the explosion sound
            _audiosource.PlayOneShot(_gameManager.AudioClips.TryGetValue("sound-explosion", out AudioClip clip) ? clip : null);
            
            // Remove all tiles in a 5x5 area around the projectile.
            var tilePosition = _groundTilemap.WorldToCell(position);
            for (var x = -2; x <= 2; x++)
            {
                for (var y = -2; y <= 2; y++)
                {
                    _groundTilemap.SetTile(new Vector3Int(tilePosition.x + x, tilePosition.y + y, 0), null);
                }
            }
            
            // Damage all players in a 5x5 area around the projectile
            var objects = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (var obj in objects)
            {
                if (!obj.CompareTag("Player")) continue;
                // Push the player away from the projectile
                var direction = (obj.transform.position - transform.position).normalized;
                obj.GetComponent<Rigidbody2D>().AddForce(direction * 100f);
                // Damage the player
                var playerIdentifier = obj.gameObject.name;
                var player = _gameManager.Players[playerIdentifier];
                player.TakeDamage(1);
            }

            yield return new WaitForSeconds(1.5f);
            
            // Change the state
            _gameManager.SetState(new End(_gameManager));
        }
        
        /// <summary>
        /// Method <c>OnDrawGizmosSelected</c> draws a gizmo if the object is selected.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 5f);
        }
    }
}
