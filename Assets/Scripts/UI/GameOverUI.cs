using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {
    public static GameOverUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        Instance = this;

        retryButton.onClick.AddListener(() => {
            Time.timeScale = 1;
            Loader.LoadScene(Loader.Scene.GameScene);
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });
    }

    private void Start() {
        Hide();
    }

    public void Show() {
        this.gameObject.SetActive(true);

        int score = GameManager.Instance.GetScore();
        scoreText.text = score.ToString();

        retryButton.Select();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
