using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
////For larger and more complex games, use state machines. But this will serve just fine for this game.
/// </summary>

public class GameManager : StaticInstance<GameManager> {
    public static event Action<GameState> OnBeforeStateChanged;
    public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }


    public AppScope appScope;
    public AudioClip mainMusic;

    void Awake() {
        appScope = Resources.Load<AppScope>("AppScope");
    
    }

    // Kick the game off with the first state
    void Start() => ChangeState(GameState.Starting);

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.Starting:
                HandleStarting();
                break;
            case GameState.SpawningAssassin:
                HandleSpawningAssasin();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnAfterStateChanged?.Invoke(newState);
        
        Debug.Log($"New state: {newState}");
    }

    private void HandleStarting() {
        // Do some start setup, could be environment, cinematics etc
        // Eventually call ChangeState again with your next state

        //MainMenuState.Instance.SpawnMainMenu(appScope);
        Instantiate(appScope.mainMenuPrefab);
        AddPlayButtonListener();
        AudioSystem.Instance.PlayMusic(mainMusic);


        ///ChangeState(GameState.SpawningAssassin);
    }

    private void HandleSpawningAssasin() {
        //ExampleUnitManager.Instance.SpawnAssasin();
        
        //ChangeState(GameState.SpawningEnemies);
    }
  
    private void HandlePlayButtonClick() {

        // Load game scene
        SceneManager.LoadScene("2. Game");
        
        // Change state to spawn the assasin
        ChangeState(GameState.SpawningAssassin);

    }
    public void AddPlayButtonListener() {

         // Get reference to PlayGame button
        GameObject playButton = GameObject.Find("UI_MainButton Play"); 
        Button playButtonComponent = playButton.GetComponent<Button>();

        // Add listener for button click
        playButtonComponent.onClick.AddListener(HandlePlayButtonClick); 

    }
}

/// <summary>
/// You can use a similar manager for controlling your menu states or dynamic-cinematics, etc
/// </summary>
[Serializable]
public enum GameState {
    Starting = 0,
    SpawningAssassin = 1,
    Win = 5,
    Lose = 6,
}