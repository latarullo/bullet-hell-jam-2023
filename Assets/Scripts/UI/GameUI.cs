using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    public static GameUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI lblEnemies;
    [SerializeField] private Image imgExperienceBar;

    private float currentExperience = 0;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        Enemy.OnEnemyKilled += Enemy_OnEnemyKilled;
        EnemySpawner.Instance.OnEnemyCreated += Instance_OnEnemyCreated;
    }

    private void Instance_OnEnemyCreated(object sender, System.EventArgs e) {
        this.UpdateUI();
    }

    private void Enemy_OnEnemyKilled(object sender, System.EventArgs e) {
        this.UpdateUI();
        currentExperience += 10;
        UpdateExperienceBar();
    }

    private void UpdateUI() {
        this.lblEnemies.text = EnemySpawner.Instance.GetEnemiesAlive().ToString();
    }

    private void UpdateExperienceBar() {
        imgExperienceBar.GetComponent<Slider>().value = currentExperience / 100;
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

    public void Show() {
        this.gameObject.SetActive(true);
    }
}
