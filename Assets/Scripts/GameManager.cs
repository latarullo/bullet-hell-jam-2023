using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public event EventHandler OnGameOver;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnScoreUpdate;
    public event EventHandler OnWaveTimeUpdate;
    public event EventHandler OnCannonCountChanged;

    public static GameManager Instance { get; private set; }

    private enum State {
        GamePlaying,
        GamePaused,
        GameOver,
    }
    private State previousState;
    private State state;
    private bool isPaused = false;

    private float waveTimeLeft = 30;
    private float waveTimeLimit = 30;

    private int cannonCount;
    private bool isCannonInCooldown = false;

    private float accumulatedDeltaTime = 0;

    private void Awake() {
        Instance = this;
        state = State.GamePlaying;
    }

    private void Start() {
        cannonCount = 5;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnCannonActivated += GameInput_OnCannonActivated;
        GameUI.Instance.OnCannonCooldownOver += GameUI_OnCannonCooldownOver;
    }

    private void GameUI_OnCannonCooldownOver(object sender, EventArgs e) {
        this.isCannonInCooldown = false;
    }

    private void GameInput_OnCannonActivated(object sender, EventArgs e) {
        if (cannonCount > 0 && !isCannonInCooldown) {
            cannonCount--;
            StartCoroutine(GameUI.Instance.CoolDownCannon());
            isCannonInCooldown = true;
            OnCannonCountChanged?.Invoke(sender, e);
            EnemySpawner.Instance.StunAllEnemies();
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e) {
        PauseGame();
    }

    private void Update() {
        switch (state) {
            case State.GamePlaying:
                accumulatedDeltaTime += Time.deltaTime;
                if (accumulatedDeltaTime > 0) {
                    accumulatedDeltaTime = 0;
                    OnWaveTimeUpdate?.Invoke(this, EventArgs.Empty);
                }
                waveTimeLeft -= Time.deltaTime;
                if (waveTimeLeft < 0) {
                    waveTimeLeft = waveTimeLimit;
                    cannonCount+=2;
                    StartCoroutine(EnemySpawner.Instance.SpawnEnemyWave());
                }
                OnScoreUpdate?.Invoke(this, EventArgs.Empty);
                if (TantoMovement.Instance.GetLife() < 0) {
                    this.state = State.GameOver;
                }
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
        return TantoMovement.Instance.GetEnemiesKilled();
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

    public float GetWaveTimeLeft() {
        return waveTimeLeft;
    }

    public int GetCannonCount() {
        return cannonCount;
    }

}