using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantoMainMenuAnimationScript : MonoBehaviour {
    [SerializeField] private float animationWaitTime = 2f;
    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private float firstAnimationTime = 5f;

    private Animator animator;
    private string lastAnimation;
    private string[] animations = { "CannonShoot", "RifleShoot", "SwordSlash" };

    void Start() {
        this.animator = GetComponent<Animator>();
        Animate("Landing");
        StartCoroutine(RandomlyAnimate());
    }

    private IEnumerator RandomlyAnimate() {
        yield return new WaitForSeconds(firstAnimationTime);
        while (true) {
            Animate(animations[Random.Range(0, animations.Length)]);
            yield return new WaitForSeconds(animationWaitTime);
        }
    }

    private void Animate(string animation) {
        if (animation == "CannonShoot") {
            StartCoroutine(PlayCannonSound());
        }

        if (animation == "RifleShoot") {
            StartCoroutine(PlayCannonSound());
            
        }

        if (lastAnimation != null) {
            animator.ResetTrigger(lastAnimation);
        }
        lastAnimation = animation;
        animator.SetTrigger(animation);
    }

    private IEnumerator PlayCannonSound() {
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlayCannonShoot(this.transform);
    }

    private IEnumerator PlayRifleSound() {
        yield return new WaitForSeconds(1);
        SoundManager.Instance.PlayRifleShoot(this.transform);
    }
}
