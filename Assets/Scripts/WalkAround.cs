using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAround : MonoBehaviour {
    private Vector3 posicaoAleatoria;

    void Start() {
        posicaoAleatoria = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
    }

    void Update() {
        Vector3 direcaoAndar = (posicaoAleatoria - this.transform.position).normalized;
        if (Vector3.Distance(posicaoAleatoria, this.transform.position) > 0.5) {
            this.transform.position += direcaoAndar * Time.deltaTime;
        } else {
            posicaoAleatoria = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        }

    }
}
