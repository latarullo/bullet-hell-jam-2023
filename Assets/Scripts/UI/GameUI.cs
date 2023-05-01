using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI lblEnemies;
    [SerializeField] private Image imgExperienceBar;

    private float currentExperience = 0;


    // Start is called before the first frame update
    void Start() {
        Enemy.OnEnemyKilled += Enemy_OnEnemyKilled;
        DevilSpawner.Instance.OnEnemyCreated += Instance_OnEnemyCreated;
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
        this.lblEnemies.text = DevilSpawner.Instance.GetEnemiesAlive().ToString();
    }

    private void UpdateExperienceBar() {
        imgExperienceBar.GetComponent<Slider>().value = currentExperience / 100;
    }
}
