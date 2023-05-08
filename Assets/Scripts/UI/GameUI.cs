using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;

public class GameUI : MonoBehaviour {

    public event EventHandler OnCannonCooldownOver;
    public static GameUI Instance { get; private set; }
    
    [SerializeField] private Image imgHealthBar;
    [SerializeField] private TextMeshProUGUI lblWaveTimer;
    [SerializeField] private TextMeshProUGUI lblEnemiesKilled;
    [SerializeField] private TextMeshProUGUI lblEnemiesAlive;
    [SerializeField] private TextMeshProUGUI lblCannonCount;
    [SerializeField] private Image imgCannon;

    private float cannonCooldownDurationInSeconds = 3;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        Enemy.OnEnemyKilled += Enemy_OnEnemyKilled;
        EnemySpawner.Instance.OnEnemyCreated += EnemySpawner_OnEnemyCreated;
        TantoMovement.Instance.OnLifeChange += Player_OnLifeChange;
        GameManager.Instance.OnCannonCountChanged += GameManager_OnCannonCountChanged;
        GameManager.Instance.OnWaveTimeUpdate += GameManager_OnWaveTimeUpdate;
        this.UpdateUI();
    }

    private void GameManager_OnWaveTimeUpdate(object sender, EventArgs e) {
        this.UpdateUI();
    }

    private void GameManager_OnCannonCountChanged(object sender, EventArgs e) {
        this.UpdateUI();
    }

    private void Player_OnLifeChange(object sender, EventArgs e) {
        this.UpdateUI();
    }

    private void EnemySpawner_OnEnemyCreated(object sender, System.EventArgs e) {
        this.UpdateUI();
    }

    private void Enemy_OnEnemyKilled(object sender, System.EventArgs e) {
        this.UpdateUI();
    }

    private void UpdateUI() {
        this.lblEnemiesAlive.text = EnemySpawner.Instance.GetEnemiesAlive().ToString();
        this.lblEnemiesKilled.text = TantoMovement.Instance.GetEnemiesKilled().ToString();
        this.lblWaveTimer.text = Convert.ToInt32(GameManager.Instance.GetWaveTimeLeft()).ToString();
        this.lblCannonCount.text = GameManager.Instance.GetCannonCount().ToString();
        this.imgHealthBar.GetComponent<Slider>().value = 1 - (TantoMovement.Instance.GetLife() / 100f);
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

    public void Show() {
        this.gameObject.SetActive(true);
    }

    public IEnumerator CoolDownCannon() {
        imgCannon.fillAmount = 0;
        while (imgCannon.fillAmount < 1) {
            imgCannon.fillAmount += 0.1f;
            yield return new WaitForSeconds(cannonCooldownDurationInSeconds / 10);
        }
        OnCannonCooldownOver?.Invoke(this, EventArgs.Empty);
    }
}
