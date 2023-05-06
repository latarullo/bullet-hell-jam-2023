using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class FacePlayer : MonoBehaviour {
    private Transform player;
    private float rotationSpeed = 100;

    void Update() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        Vector3 h = player.position - this.transform.position;
        float angle = Mathf.Atan2(h.x, h.z) * Mathf.Rad2Deg;

        Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
