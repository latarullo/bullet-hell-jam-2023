using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public event EventHandler OnGameOver;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnScoreUpdate;

    public static GameManager Instance { get; private set; }

    private enum State {
        GamePlaying,
        GamePaused,
        GameOver,
    }
    private State previousState;
    private State state;
    private bool isPaused = false;
    private float survivalScore = 0;

    private void Awake() {
        Instance = this;
        state = State.GamePlaying;
    }

    private void Start() {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnCannonActivated += GameInput_OnCannonActivated;
    }

    private void GameInput_OnCannonActivated(object sender, EventArgs e) {
        EnemySpawner.Instance.StunAllEnemies();
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        PauseGame();
    }

    [Obsolete]
    private void Update() {
        switch (state) {
            case State.GamePlaying:
                survivalScore += Time.deltaTime;
                OnScoreUpdate?.Invoke(this, EventArgs.Empty);
                break;
            case State.GamePaused:
                break;
            case State.GameOver:
                GameOverUI.Instance.Show();
                Time.timeScale = 0;
                previousState = state;
                state = State.GamePaused;
                break;
        }
        Debug.Log("State : " + state);
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    public int GetScore() {
        return Convert.ToInt32(survivalScore);
    }

    public float GetSurvivalScore() {
        return survivalScore;
    }

    public void PauseGame() {
        isPaused = !isPaused;
        if (isPaused) {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0;
            previousState = state;
            state = State.GamePaused;
            GameUI.Instance.Hide();
        } else {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1;
            state = previousState;
            GameUI.Instance.Show();
        }
    }
}