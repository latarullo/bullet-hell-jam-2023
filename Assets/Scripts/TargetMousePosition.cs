using UnityEngine;

public class TargetMousePosition : MonoBehaviour {

    [SerializeField] private LayerMask layerMask;
    private float rotationSpeed = 10;

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, layerMask)) {
            Vector3 h = hit.point - this.transform.position;
            float angle = Mathf.Atan2(h.x, h.z) * Mathf.Rad2Deg;

            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

        }
    }
}
