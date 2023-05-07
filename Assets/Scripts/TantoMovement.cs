using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantoMovement : MonoBehaviour {

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private bool movementBasedOnCamera = true;

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
            Vector3 forward;
            Vector3 right;
            if (movementBasedOnCamera) {
                forward = Camera.main.transform.forward;
                right = Camera.main.transform.right;
            } else {
                forward = this.transform.forward;
                right = this.transform.right;
            }

            forward.y = 0;
            right.y = 0;
            forward = forward.normalized;
            right = right.normalized;

            Vector3 forwardVector = forward * inputVector.z;
            Vector3 rightVector = right * inputVector.x;
            
            Vector3 moveToVector = forwardVector + rightVector;

            this.transform.Translate(moveToVector * Time.deltaTime * moveSpeed, Space.World);
        }
    }
}
