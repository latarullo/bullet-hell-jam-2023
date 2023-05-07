using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameManager.Instance.PauseGame();
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.LoadScene(Loader.Scene.MainMenuScene);
        });

        optionsButton.onClick.AddListener(() => {
            this.Hide();
            OptionsUI.Instance.Show(Show);
        });
    }


    private void Start() {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        this.Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        this.Hide();
    }
    private void GameManager_OnGamePaused(object sender, System.EventArgs e) {
        this.Show();
    }

    public void Show() {
        this.gameObject.SetActive(true);
        resumeButton.Select();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }

}