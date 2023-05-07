using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour {

    public static CameraSystem Instance { get; private set; }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float fieldOfViewMin = 20f;
    [SerializeField] private float fieldOfViewMax = 100f;
    [SerializeField] private bool isZoomUpIn = true;

    private float fieldOfView = 50f;

    private void Awake() {
        Instance = this;
    }

    void Start() {
        GameInput.Instance.OnCameraZoomScrollUp += Instance_OnCameraZoomScrollUp;
        GameInput.Instance.OnCameraZoomScrollDown += OnCameraZoomScrollDown;
    }

    private void Instance_OnCameraZoomScrollUp(object sender, System.EventArgs e) {
        float zoom = isZoomUpIn ? -5 : 5;
        HandleZoom(zoom);
    }

    private void OnCameraZoomScrollDown(object sender, System.EventArgs e) {
        float zoom = isZoomUpIn ? 5 : -5;
        HandleZoom(zoom);
    }

    private void HandleZoom(float zoom) {
        fieldOfView += zoom;
        fieldOfView = Mathf.Clamp(fieldOfView, fieldOfViewMin, fieldOfViewMax);
        cinemachineVirtualCamera.m_Lens.FieldOfView = fieldOfView;
    }

    public void ChangeReverseZoomDirection() {
        isZoomUpIn = !isZoomUpIn;
    }

    public bool IsReverseZoomDirection() {
        return isZoomUpIn;
    }
}
