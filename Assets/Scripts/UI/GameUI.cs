using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI lblEnemies;


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
    }

    private void UpdateUI() {
        this.lblEnemies.text = DevilSpawner.Instance.GetEnemiesAlive().ToString();
    }
}
