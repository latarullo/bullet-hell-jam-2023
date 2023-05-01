using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Entities.SystemBaseDelegates;

public class TargetMousePosition : MonoBehaviour {

    private float minFov = 15f;
    private float maxFov = 90f;
    private float sensitivity = 10f;
    private float rotationSpeed = 10;
    [SerializeField] private LayerMask layerMask;

    // Update is called once per frame
    void Update() {
        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) {
            Vector3 h = hit.point - this.transform.position;
            float angle = Mathf.Atan2(h.x, h.z) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        }
    }
}
