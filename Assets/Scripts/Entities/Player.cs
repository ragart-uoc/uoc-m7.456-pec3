namespace PEC3.Entities
{
    /// <summary>
    /// Class <c>Player</c> contains the methods and properties needed for the game.
    /// </summary>
    public class Player
    {
        public int Health;
        public bool IsCPU;

        public Player(int health)
        {
            Health = health;
        }

        public bool TakeDamage(int damage)
        {
            Health -= damage;
            return Health <= 0;
        }
    }
}
