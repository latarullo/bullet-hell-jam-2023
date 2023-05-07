using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TantoMainMenuAnimationScript : MonoBehaviour {
    [SerializeField] private float animationWaitTime = 2f;
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
            //TODO: stronger camera shaking
        }

        if (animation == "CannonShoot") {
            //TODO: weaker camera shaking
        }

        if (lastAnimation != null) {
            animator.ResetTrigger(lastAnimation);
        }
        lastAnimation = animation;
        animator.SetTrigger(animation);
    }
}
