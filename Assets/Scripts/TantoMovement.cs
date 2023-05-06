using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantoMovement : MonoBehaviour {
    void Update() {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) {
            movement.z += 1;
        }
        if (Input.GetKey(KeyCode.S)) {
            movement.z -= 1;
        }
        if (Input.GetKey(KeyCode.A)) {
            movement.x += -1;
        }
        if (Input.GetKey(KeyCode.D)) {
            movement.x += 1;
        }

        if (movement.magnitude != 0) {
            Vector3 forwardVector = this.transform.forward * movement.z * Time.deltaTime;
            Vector3 rightVector = this.transform.right * movement.x * Time.deltaTime;
            Vector3 moveToVector = forwardVector + rightVector;

            this.transform.Translate(moveToVector, Space.World);
        }
    }
}
