namespace PEC3.Entities
{
    /// <summary>
    /// Class <c>Player</c> contains the methods and properties needed for the game.
    /// </summary>
    public class Player
    {
        /// <value>Property <c>Identifier</c> represents the player identifier.</value>
        public string Identifier;

        /// <value>Property <c>Name</c> represents the player name.</value>
        public string Name;
        
        /// <value>Property <c>Health</c> represents the player health.</value>
        public int Health;

        /// <value>Property <c>IsActive</c> represents the player status.</value>
        public bool IsActive;
        
        /// <value>Property <c>IsCPU</c> represents the player type.</value>
        public bool IsCPU;

        
        public Player(string identifier, string name, int health, bool isActive, bool isCPU)
        {
            Identifier = identifier;
            Name = name;
            Health = health;
            IsActive = isActive;
            IsCPU = isCPU;
        }

        public bool TakeDamage(int damage)
        {
            Health -= damage;
            return Health <= 0;
        }
    }
}
