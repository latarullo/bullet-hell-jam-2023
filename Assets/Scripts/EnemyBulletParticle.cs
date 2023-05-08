using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletParticle : MonoBehaviour {
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private float shootCooldownTimer = 1f;
    [SerializeField] private float shootCooldownMaxTimer = 1f;

    List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private float lastShot;

    private void Awake() {
        lastShot = Time.time;
    }

    //private void Update() {
    //    shootCooldownTimer -= Time.deltaTime;
    //    if (shootCooldownTimer < 0) {
    //        particleSystem.Play();
    //        shootCooldownTimer = shootCooldownMaxTimer;
    //    }
    //}

    private void OnParticleCollision(GameObject other) {
        int events = particleSystem.GetCollisionEvents(other, collisionEvents);
        foreach (ParticleCollisionEvent e in collisionEvents) {
            Instantiate(hitEffect, e.intersection, Quaternion.LookRotation(e.normal));
            TantoMovement.Instance.TakeDamage(1);
        }
    }

    public void Shoot() {
        if (lastShot + shootCooldownTimer < Time.time) {
            particleSystem.Play();
            lastShot = Time.time;
        }
    }
}