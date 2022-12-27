using UnityEngine;

namespace PEC3.Entities
{
    /// <summary>
    /// Class <c>Player</c> represents a player instance.
    /// </summary>
    public class Player
    {
        /// <value>Property <c>Identifier</c> represents the player identifier.</value>
        public readonly string Identifier;

        /// <value>Property <c>Name</c> represents the player name.</value>
        public readonly string Name;
        
        /// <value>Property <c>WinnerMessage</c> represents the player winning message.</value>
        public string WinnerMessage;
        
        /// <value>Property <c>Health</c> represents the player health.</value>
        public int Health;

        /// <value>Property <c>AimAngle</c> represents the player aim angle.</value>
        public float AimAngle = 0.0f;

        /// <value>Property <c>IsActive</c> represents the player status.</value>
        public bool IsActive;
        
        /// <value>Property <c>IsCPU</c> represents the player type.</value>
        public bool IsCPU;

        /// <value>Property <c>IsCurrent</c> represents the current player status.</value>
        public bool IsCurrent = false;

        /// <value>Property <c>PlayerObject</c> represents the game object of the player.</value>
        public GameObject GameObject;
        
        /// <summary>
        /// Method <c>Player</c> is the constructor of the class.
        /// </summary>
        /// <param name="identifier">The player identifier</param>
        /// <param name="name">The name of the player</param>
        /// <param name="health">The starter health for the player</param>
        /// <param name="isActive">The status of the player</param>
        /// <param name="isCPU">The type of the player</param>
        public Player(string identifier, string name, int health, bool isActive, bool isCPU)
        {
            Identifier = identifier;
            Name = name;
            Health = health;
            IsActive = isActive;
            IsCPU = isCPU;
        }

        /// <summary>
        /// Method <c>TakeDamage</c> is used to reduce the player health.
        /// </summary>
        /// <param name="damage">The damage taken</param>
        /// <returns>Whether the player is still alive or not</returns>
        public bool TakeDamage(int damage)
        {
            Health -= damage;
            return Health <= 0;
        }
    }
}
