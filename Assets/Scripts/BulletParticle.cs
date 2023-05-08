using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticle : MonoBehaviour {

    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject hitEffect;
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
        Debug.Log("OnParticleCollision");
        int events = particleSystem.GetCollisionEvents(other, collisionEvents);
        foreach (ParticleCollisionEvent e in collisionEvents) {
            SoundManager.Instance.PlayBoltImpact(e.intersection);
            Instantiate(hitEffect, e.intersection, Quaternion.LookRotation(e.normal));
        }

        if (other.TryGetComponent<Enemy>(out Enemy enemy)) {
            Debug.Log("enemy.takeDamage");
            enemy.TakeDamage(101);
        }
    }
}
