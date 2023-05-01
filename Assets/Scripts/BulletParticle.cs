using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour {

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private float shootCooldownTimer = 1f;
    [SerializeField] private float shootCooldownMaxTimer = 1f;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private void Update() {
        shootCooldownTimer -= Time.deltaTime;
        if (shootCooldownTimer < 0) {
            particleSystem.Play();
            shootCooldownTimer = shootCooldownMaxTimer;
        }
    }

    private void OnParticleCollision(GameObject other) {
        int events = particleSystem.GetCollisionEvents(other, collisionEvents);
        foreach (ParticleCollisionEvent e in collisionEvents) {
        }

        if (other. TryGetComponent<Enemy>(out Enemy enemy)) {
            enemy.TakeDamage(1000);
        }
    }
}
