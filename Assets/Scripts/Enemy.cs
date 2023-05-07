using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public static event EventHandler OnEnemyKilled;

    [SerializeField] private float life = 100;
    [SerializeField] private float maxLife = 100;

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
}
