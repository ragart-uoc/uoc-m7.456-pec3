using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using PEC3.Entities;
using PEC3.States;

namespace PEC3.Managers
{
    /// <summary>
    /// Class <c>GameManager</c> contains the methods and properties needed for the game.
    /// </summary>
    public class GameManager : StateMachine
    {
        /// <value>Property <c>MinPlayers</c> represents the minimum number of players.</value>
        private const int MinPlayers = 2;
        
        /// <value>Property <c>MinPlayers</c> represents the maximum number of players.</value>
        private const int MaxPlayers = 4;
        
        /// <value>Property <c>InitialPlayerLives</c> represents the initial number of lives of the players.</value>
        private const int InitialPlayerLives = 3;
        
        /// <value>Property <c>TurnTime</c> represents the initial number of lives of the players.</value>
        public const int TurnTime = 10;
        
        /// <value>Property <c>Instance</c> represents the instance of the GameplayManager.</value>
        private static GameManager _instance;

        /// <value>Property <c>Instance</c> represents the UI element containing the game options (NewGame scene only).</value>
        public GameObject optionList;
        
        /// <value>Property <c>Instance</c> represents the UI element containing the start button (NewGame scene only).</value>
        public Button startButton;
        
        /// <value>Property <c>playerInfoGroup</c> represents the UI element containing the player information (Game scene only).</value>
        public CanvasGroup playerInfoGroup;
        
        /// <value>Property <c>playerNameText</c> represents the UI element containing the player name text (Game scene only).</value>
        public TextMeshProUGUI playerNameText;
        
        /// <value>Property <c>playerLivesText</c> represents the UI element containing the player lives text (Game scene only).</value>
        public TextMeshProUGUI playerLivesText;
        
        /// <value>Property <c>generalText</c> represents the UI element containing the general text (Game and End scenes only).</value>
        public TextMeshProUGUI generalText;
        
        /// <value>Property <c>winnerText</c> represents the UI element containing the winner text (End scene only).</value>
        public TextMeshProUGUI winnerText;
        
        /// <value>Property <c>playAgainButton</c> represents the UI element containing the play again button (End scene only).</value>
        public Button playAgainButton;
        
        /// <value>Property <c>PlayerSelectButton</c> represents the UI element containing the back to player select button (End scene only).</value>
        public Button playerSelectButton;
        
        /// <value>Property <c>MainMenuButton</c> represents the UI element containing the back to main menu button (Game scene only).</value>
        public Button mainMenuButton;
        
        /// <value>Property <c>virtualCamera</c> represents the Cinemachine Virtual Camera.</value>
        public CinemachineVirtualCamera virtualCamera;
        
        /// <value>Property <c>Players</c> represents the game players.</value>
        public readonly Dictionary<string, Player> Players = new Dictionary<string, Player>();

        /// <value>Property <c>_initialPlayers</c> represents the inital status of the game players.</value>
        private readonly  Dictionary<string, Player> _initialPlayers = new Dictionary<string, Player>();
        
        /// <value>Property <c>_playerCount</c> represents the number of players.</value>
        private int _playerCount;

        /// <value>Property <c>_playerOrder</c> represents the order of the players.</value>
        public List<string> playerOrder;
        
        /// <value>Property <c>_cameraAudioSource</c> represents the AudioSource component of the camera.</value>
        private AudioSource _cameraAudioSource;
        
        /// <value>Property <c>AudioClips</c> represents a dictionary containing all sounds and music for the game.</value>
        public Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();

        /// <summary>
        /// Method <c>Awake</c> is called when the script instance is being loaded.
        /// </summary>
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
            
            SetState(new Idle(this));
            
            Players.Add("P1", new Player("P1", "Rebel Potato Army", InitialPlayerLives, true, false));
            Players.Add("P2", new Player("P2", "The Onionite Empire", InitialPlayerLives, true, true));
            Players.Add("P3", new Player("P3", "Pina Colada Cult", InitialPlayerLives, false, true));
            Players.Add("P4", new Player("P4", "Napoli Heritage P. A.", InitialPlayerLives, false, true));
            
            Players["P1"].WinnerMessage = "No more onion.\n\nCivilization has reached a new level of peace and prosperity.\n\nWar is no longer necessary.\n\nWait, what do you mean you don't like half-cooked omelettes?";
            Players["P2"].WinnerMessage = "With no omelette without onion in the world, the rest of the factions have surrendered to the Onionite Empire.\n\nAll hail the onion.";
            Players["P3"].WinnerMessage = "The Cult has taken over the world.\n\nEverything has pineapple now...\n\nEven the Spanish omelette.";
            Players["P4"].WinnerMessage = "The madness has stopped.\n\nNo more pineapple.\nNo more onion\nNo more Spanish omelette.\n\nThere's only pizza.";
            
            _playerCount = 2;
            
            AudioClips.Add("music-game", Resources.Load<AudioClip>("Music/theme-boss_battle"));
            AudioClips.Add("music-end", Resources.Load<AudioClip>("Music/theme-assassins_assault"));
            
            AudioClips.Add("sound-shot", Resources.Load<AudioClip>("Sounds/flaunch"));
            AudioClips.Add("sound-explosion", Resources.Load<AudioClip>("Sounds/rlaunch"));
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
                    // Get the UI element containing the game options
                    optionList = GameObject.Find("PlayerOptionsGroup");
                    // Get the UI element containing the start button and add a listener
                    startButton = GameObject.Find("StartButton").GetComponent<Button>();
                    startButton.onClick.AddListener(StartGame);
                    // Update the UI element containing the game options
                    UpdateActivePlayers(_playerCount);
                    break;
                case "Game":
                    // Get the main camera source
                    _cameraAudioSource = Camera.main.GetComponent<AudioSource>();
                    _cameraAudioSource.clip = AudioClips.TryGetValue("music-game", out var clipgame) ? clipgame : null;
                    _cameraAudioSource.Play();
                    // Make a backup of the intial elements of the game
                    _initialPlayers.Clear();
                    foreach (var player in Players)
                    {
                        var playerObject = new Player(player.Value.Identifier, player.Value.Name, player.Value.Health, player.Value.IsActive, player.Value.IsCPU);
                        _initialPlayers.Add(player.Value.Identifier, playerObject); 
                    }
                    // Get the UI elements
                    playerInfoGroup = GameObject.Find("PlayerInfoGroup").GetComponent<CanvasGroup>();
                    playerNameText = GameObject.Find("PlayerNameText").GetComponent<TextMeshProUGUI>();
                    playerLivesText = GameObject.Find("PlayerLivesText").GetComponent<TextMeshProUGUI>();
                    generalText = GameObject.Find("GeneralText").GetComponent<TextMeshProUGUI>();
                    // Hide the UI elements
                    playerInfoGroup.alpha = 0;
                    generalText.canvasRenderer.SetAlpha(0.0f);
                    // Get the UI element containing the virtual camera
                    virtualCamera = GameObject.Find("Cinemachine VCam").GetComponent<CinemachineVirtualCamera>();
                    // Get the game objects containing the players and shows them if they are active
                    foreach (var player in Players)
                    {
                        player.Value.GameObject = GameObject.Find(player.Value.Identifier);
                        player.Value.GameObject.SetActive(player.Value.IsActive);
                    }
                    // Determine the order of the players
                    playerOrder = Players.Values.Where(p => p.IsActive).Select(p => p.Identifier).ToList();
                    playerOrder.Shuffle();
                    Players[playerOrder[0]].IsCurrent = true;
                    // Begin the game
                    SetState(new Begin(this));
                    break;
                case "End":
                    // Get the main camera source
                    _cameraAudioSource = Camera.main.GetComponent<AudioSource>();
                    _cameraAudioSource.clip = AudioClips.TryGetValue("music-end", out AudioClip clip) ? clip : null;
                    _cameraAudioSource.Play();
                    // Get the UI elements
                    generalText = GameObject.Find("GeneralText").GetComponent<TextMeshProUGUI>();
                    winnerText = GameObject.Find("WinnerText").GetComponent<TextMeshProUGUI>();
                    playAgainButton = GameObject.Find("PlayAgainButton").GetComponent<Button>();
                    playerSelectButton = GameObject.Find("PlayerSelectButton").GetComponent<Button>();
                    mainMenuButton = GameObject.Find("MainMenuButton").GetComponent<Button>();
                    // Add listeners to the UI elements
                    playAgainButton.onClick.AddListener(RestartGame);
                    playerSelectButton.onClick.AddListener(NewGame);
                    mainMenuButton.onClick.AddListener(ExitGame);
                    // Determine the winner
                    var winner = Players.Values.FirstOrDefault(p => p.IsActive);
                    // Update the UI element containing the general text
                    generalText.text = winner.Identifier + " - " + winner.Name + " wins!";
                    winnerText.text = winner.WinnerMessage;
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
        /// Method <c>RestartGame</c> recovers the initial status of the game and restarts it.
        /// </summary>
        public void RestartGame()
        {
            Players.Clear();
            foreach (var player in _initialPlayers)
            {
                var playerObject = new Player(player.Value.Identifier, player.Value.Name, player.Value.Health, player.Value.IsActive, player.Value.IsCPU);
                Players.Add(player.Value.Identifier, playerObject); 
            }
            SceneManager.LoadScene("Game");
        }
        
        /// <summary>
        /// Method <c>NewGame</c> destroys the instance and returns to the player selection screen.
        /// </summary>
        public void NewGame()
        {
            Destroy(this.gameObject);
            _instance = null;
            SceneManager.LoadScene("NewGame");
        }
        
        /// <summary>
        /// Method <c>ExitGame</c> destroys the instance and returns to the main menu screen.
        /// </summary>
        public void ExitGame()
        {
            Destroy(this.gameObject);
            _instance = null;
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
            Players[player].IsCPU = !Players[player].IsCPU;
            playerOption.GetComponent<TextMeshProUGUI>().text = Players[player].IsCPU ? "CPU" : "Human";
        }
        
        /// <summary>
        /// Method <c>UpdateActivePlayerOptions</c> updates the options list depending on the number of active players.
        /// </summary>
        private void UpdateActivePlayers(int activePlayers)
        {
            Players["P3"].IsActive = (activePlayers >= 3);
            Players["P4"].IsActive = (activePlayers == 4);
            optionList.transform.Find("NumberOfPlayers/PlayerOptionSelector/PlayerOptionText").GetComponent<TextMeshProUGUI>().text = _playerCount.ToString();
            optionList.transform.Find("P3").gameObject.SetActive(Players["P3"].IsActive);
            optionList.transform.Find("P4").gameObject.SetActive(Players["P4"].IsActive);
        }
        
        /// <summary>
        /// Method <c>GetCurrentPlayer</c> returns the current player.
        /// </summary>
        public Player GetCurrentPlayer()
        {
            return Players.Values.First(p => p.IsCurrent);
        }
        
        /// <summary>
        /// Method <c>SetNextPlayer</c> sets the next player.
        /// </summary>
        public void SetNextPlayer()
        {
            var currentPlayer = GetCurrentPlayer();
            Players[currentPlayer.Identifier].IsCurrent = false;
            var currentPlayerIndex = playerOrder.IndexOf(currentPlayer.Identifier);
            var nextPlayerIndex = (currentPlayerIndex + 1) % playerOrder.Count;
            var nextPlayerIdentifier = playerOrder[nextPlayerIndex];
            Players[nextPlayerIdentifier].IsCurrent = true;
        }
        
        
    }
}
