using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using TMPro;
using PEC3.Entities;
using PEC3.States;

namespace PEC3.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> contains the methods and properties needed for the game.
    /// </summary>
    public class GameManager : StateMachine
    {
        /// <value>Property <c>Instance</c> represents the instance of the GameplayManager.</value>
        public static GameManager Instance;

        /// <value>Property <c>Instance</c> represents the UI element containing the game options (NewGame scene only).</value>
        public GameObject optionList;
        
        /// <value>Property <c>generalText</c> represents the UI element containing the general text (Game scene only).</value>
        public TextMeshProUGUI generalText;
        
        /// <value>Property <c>_players</c> represents the game players.</value>
        private readonly Dictionary<string, Player> _players = new Dictionary<string, Player>();
        
        /// <value>Property <c>_playerCount</c> represents the number of players.</value>
        private int _playerCount;

        /// <value>Property <c>MinPlayers</c> represents the minimum number of players.</value>
        private const int MinPlayers = 2;
        
        /// <value>Property <c>MinPlayers</c> represents the maximum number of players.</value>
        private const int MaxPlayers = 4;
        
        /// <value>Property <c>InitialPlayerLives</c> represents the initial number of lives of the players.</value>
        private const int InitialPlayerLives = 3;
        
        /// <value>Property <c>_playerOrder</c> represents the order of the players.</value>
        public List<string> playerOrder;
        
        /// <value>Property <c>_currentPlayer</c> represents the current player.</value>
        public Player CurrentPlayer;

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            _players.Add("P1", new Player("P1", "Rebel Potato Army", InitialPlayerLives, true, false));
            _players.Add("P2", new Player("P2", "The Onionite Empire", InitialPlayerLives, true, true));
            _players.Add("P3", new Player("P3", "Pina Colada Cult", InitialPlayerLives, false, true));
            _players.Add("P4", new Player("P4", "Napoli Heritage P. A.", InitialPlayerLives, false, true));
            
            _players["P1"].SetWinnerMessage("");
            _players["P2"].SetWinnerMessage("All hail the onion.");
            _players["P3"].SetWinnerMessage("");
            _players["P4"].SetWinnerMessage("The madness has stopped.\n\nNo more pineapple.\nNo more onion\nNo more Spanish omelette.\n\nThere's only pizza.");
            
            _playerCount = 2;
        }
        
        /// <summary>
        /// Method <c>OnEnable</c> is called when the object becomes enabled and active.
        /// </summary>
        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        
        /// <summary>
        /// Method <c>OnDisable</c> is called when the behaviour becomes disabled.
        /// </summary>
        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        
        /// <summary>
        /// Method <c>OnSceneLoaded</c> is called when the scene is loaded.
        /// </summary>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "NewGame":
                    UpdateActivePlayers(_playerCount);
                    break;
                case "Game":
                    playerOrder = _players.Values.Where(p => p.IsActive).Select(p => p.Identifier).ToList();
                    playerOrder.Shuffle();
                    CurrentPlayer = _players[playerOrder[0]];
                    SetState(new Begin(this));
                    break;
            }
        }

        /// <summary>
        /// Method <c>StartGame</c> starts a new game.
        /// </summary>
        public void StartGame()
        {
            SceneManager.LoadScene("Game");
        }
        
        /// <summary>
        /// Method <c>RestartGame</c> destroys the instance and returns to the player selection screen.
        /// </summary>
        public void RestartGame()
        {
            Destroy(this.gameObject);
            Instance = null;
            SceneManager.LoadScene("NewGame");
        }
        
        /// <summary>
        /// Method <c>ExitGame</c> destroys the instance and returns to the main menu screen.
        /// </summary>
        public void ExitGame()
        {
            Destroy(this.gameObject);
            Instance = null;
            SceneManager.LoadScene("MainMenu");
        }

        /// <summary>
        /// Method <c>ChangeNumberOfPlayers</c> changes the number of players.
        /// </summary>
        public void ChangeNumberOfPlayers(bool increase)
        {
            _playerCount = int.Parse(optionList.transform.Find("NumberOfPlayers/PlayerOptionSelector/PlayerOptionText").GetComponent<TextMeshProUGUI>().text);
            _playerCount += (increase) ? 1 : -1;
            _playerCount = _playerCount switch
            {
                < MinPlayers => MaxPlayers,
                > MaxPlayers => MinPlayers,
                _ => _playerCount
            };
            UpdateActivePlayers(_playerCount);
        }
        
        /// <summary>
        /// Method <c>ChangePlayerType</c> changes the type of a player.
        /// </summary>
        public void ChangePlayerType()
        {
            var player = EventSystem.current.currentSelectedGameObject.transform.parent.parent.name;
            var playerOption = optionList.transform.Find(player + "/PlayerOptionSelector/PlayerOptionText");
            _players[player].IsCPU = !_players[player].IsCPU;
            playerOption.GetComponent<TextMeshProUGUI>().text = _players[player].IsCPU ? "CPU" : "Human";
        }
        
        /// <summary>
        /// Method <c>UpdateActivePlayerOptions</c> updates the options list depending on the number of active players.
        /// </summary>
        private void UpdateActivePlayers(int activePlayers)
        {
            _players["P3"].IsActive = (activePlayers >= 3);
            _players["P4"].IsActive = (activePlayers == 4);
            optionList.transform.Find("NumberOfPlayers/PlayerOptionSelector/PlayerOptionText").GetComponent<TextMeshProUGUI>().text = _playerCount.ToString();
            optionList.transform.Find("P3").gameObject.SetActive(_players["P3"].IsActive);
            optionList.transform.Find("P4").gameObject.SetActive(_players["P4"].IsActive);
        }
        
        /// <summary>
        /// Method <c>SetNextPlayer</c> sets the next player.
        /// </summary>
        private void SetNextPlayer()
        {
            var currentPlayerIndex = playerOrder.IndexOf(CurrentPlayer.Identifier);
            var nextPlayerIndex = (currentPlayerIndex + 1) % playerOrder.Count;
            CurrentPlayer = _players[playerOrder[nextPlayerIndex]];
        }
    }
}
