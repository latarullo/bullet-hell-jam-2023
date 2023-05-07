using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour {

    public static OptionsUI Instance { get; private set; }

    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private Button musicButton;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private Toggle cameraBasedMovement;
    [SerializeField] private Toggle reverseZoomDirection;
    [SerializeField] private Button closeButton;

    private Action onCloseButtonAction;


    private void Awake() {
        Instance = this;
        soundEffectsButton.onClick.AddListener(() => {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        musicButton.onClick.AddListener(() => {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });

        cameraBasedMovement.onValueChanged.AddListener((value) => {
            TantoMovement.Instance.ChangeCameraBasedMovement();
            UpdateVisual();
        });

        reverseZoomDirection.onValueChanged.AddListener((value) => {
            CameraSystem.Instance.ChangeReverseZoomDirection();
            UpdateVisual();
        });

        closeButton.onClick.AddListener(() => {
            this.Hide();
            onCloseButtonAction();
        });
    }

    private void Start() {
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
    }

    private void GameManager_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }

    private void UpdateVisual() {
        soundEffectsText.text = "Sound Effects: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f).ToString();
        cameraBasedMovement.isOn = TantoMovement.Instance.IsCameraBasedMovement();
        reverseZoomDirection.isOn = CameraSystem.Instance.IsReverseZoomDirection(); 
    }

    public void Show(Action onCloseButtonAction) {
        this.onCloseButtonAction = onCloseButtonAction;
        this.gameObject.SetActive(true);
        UpdateVisual();
        soundEffectsButton.Select();
    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}