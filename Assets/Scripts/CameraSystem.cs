using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField] private float fieldOfViewMin = 20f;
    [SerializeField] private float fieldOfViewMax = 100f;

    private float fieldOfView = 50f;

    void Start() {
        GameInput.Instance.OnCameraZoomIn += Instance_OnCameraZoomIn;
        GameInput.Instance.OnCameraZoomOut += Instance_OnCameraZoomOut;
    }

    private void Instance_OnCameraZoomOut(object sender, System.EventArgs e) {
        HandleZoom(5f);
    }

    private void Instance_OnCameraZoomIn(object sender, System.EventArgs e) {
        HandleZoom(-5f);
    }

    private void HandleZoom(float zoom) {
        fieldOfView += zoom;
        fieldOfView = Mathf.Clamp(fieldOfView, fieldOfViewMin, fieldOfViewMax);
        cinemachineVirtualCamera.m_Lens.FieldOfView = fieldOfView;
    }
}
