using System;
using UnityEngine;

public class GameInput : MonoBehaviour {
    public static GameInput Instance { private set; get; }

    public event EventHandler OnCameraZoomScrollUp;
    public event EventHandler OnCameraZoomScrollDown;
    public event EventHandler OnPauseAction;
    public event EventHandler<OnPlayerMoveEventArgs> OnPlayerMove;
    public class OnPlayerMoveEventArgs : EventArgs {
        public Vector3 inputVector;
    }
    public event EventHandler OnCannonActivated;
    public event EventHandler OnRifleActivated;
    public event EventHandler OnSwordActivated;

    private void Awake() {
        Instance = this;
    }

    void Update() {
        Vector3 inputVector = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            inputVector.z += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            inputVector.z -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            inputVector.x += -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            inputVector.x += 1;
        }

        if (inputVector.magnitude != 0) {
            OnPlayerMove?.Invoke(this, new OnPlayerMoveEventArgs { inputVector = inputVector });
        }

        if (Input.GetKey(KeyCode.Alpha1)) {
            OnCannonActivated?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKey(KeyCode.Alpha2)) {
            OnRifleActivated?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKey(KeyCode.Alpha3)) {
            OnSwordActivated?.Invoke(this, EventArgs.Empty);
        }


        if (Input.mouseScrollDelta.y > 0) {
            OnCameraZoomScrollUp?.Invoke(this, EventArgs.Empty);
        }
        if (Input.mouseScrollDelta.y < 0) {
            OnCameraZoomScrollDown?.Invoke(this, EventArgs.Empty);
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
            OnPauseAction?.Invoke(this, EventArgs.Empty);
        }
    }
}
