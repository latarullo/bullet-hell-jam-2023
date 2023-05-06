using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { private set; get; }

    public event EventHandler OnCameraZoomIn;
    public event EventHandler OnCameraZoomOut;

    private void Awake() {
        Instance = this;
    }

    void Update() {
        if (Input.mouseScrollDelta.y > 0) {
            OnCameraZoomIn?.Invoke(this, EventArgs.Empty);
        }
        if (Input.mouseScrollDelta.y < 0) {
            OnCameraZoomOut?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKey(KeyCode.P)) {
            //if (GameManager.Instance.IsPaused(){
            //OnGameResume?.Invoke(this);
            //}
            //else{
            //OnGamePause?.Invoke(this);
            //}
        }
    }
}
