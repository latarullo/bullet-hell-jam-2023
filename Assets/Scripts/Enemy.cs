using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public static event EventHandler OnEnemyKilled;

    [SerializeField] private float maxLife = 100;

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float attackRange = 20f;
    [SerializeField] private float playerDetectionRange = 50f;

    [SerializeField] private GameObject particleShoot;

    private Transform player;
    private bool isAttacking = false;
    private float life = 100;

    private void Start() {
        life = maxLife;
    }

    private void Update() {
        if (player == null) {
            player = TantoMovement.Instance.gameObject.transform;
        }

        if (life > 0) {
            if (Vector3.Distance(player.position, this.transform.position) < playerDetectionRange) {
                if (Vector3.Distance(player.position, this.transform.position) < attackRange) {
                    if (!isAttacking) {
                        isAttacking = true;

                        if (IsRanged()) {
                            particleShoot.GetComponent<EnemyBulletParticle>().Shoot();
                        } else {
                            getAnimator().SetTrigger("Attack");
                        }
                    }
                } else {
                    isAttacking = false;
                    getAnimator().ResetTrigger("Attack");
                    Vector3 playerDir = player.position - this.transform.position;
                    playerDir.y = 0;
                    playerDir = playerDir.normalized;

                    this.transform.position += playerDir * Time.deltaTime * moveSpeed;
                    this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
                }
            }
        }


    }

    public void TakeDamage(float damage) {
        this.life -= damage;
        if (life < 0) {
            Animator animator = getAnimator();
            animator.SetTrigger("Die");
            OnEnemyKilled?.Invoke(this, EventArgs.Empty);
            StartCoroutine(Hide());
            //Destroy(this.gameObject);
        }
    }

    private Animator getAnimator() {
        Animator animator = this.gameObject.GetComponent<Animator>();
        if (animator == null) {
            animator = this.gameObject.GetComponentInChildren<Animator>();
        }

        return animator;
    }

    private IEnumerator Hide() {
        SoundManager.Instance.PlayDemonDie(this.transform);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(3);
        Animator animator = getAnimator();
        animator.ResetTrigger("Die");
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        this.gameObject.SetActive(false);
    }

    public Transform Create(Vector3 position) {
        this.gameObject.SetActive(true);
        this.life = maxLife;
        this.transform.position = position;
        return this.transform;
    }

    private bool IsRanged() {
        return this.attackRange > 2;
    }
}
