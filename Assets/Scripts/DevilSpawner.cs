using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class DevilSpawner : MonoBehaviour {

    public static DevilSpawner Instance { get; private set; }

    public event EventHandler OnEnemyCreated;

    [SerializeField] private Transform player;
    [SerializeField] private Transform devilPredab;
    [SerializeField] private float minimumSpawnCircleRange = 5;
    [SerializeField] private float maximumSpawnCircleRange = 10;
    [SerializeField] private float spawnTimer = 3;
    [SerializeField] private int spawnEnemiesQuantity = 20;

    [SerializeField] private float waveNumber = 1;

    private List<Transform> enemies = new List<Transform>();

    private void Awake() {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnDevil());
    }

    // Update is called once per frame
    void Update() {

    }
    private IEnumerator SpawnDevil() {
        for (int i = 0; i < spawnEnemiesQuantity; i++) {
            Transform devil = CreateNewDevil();
        }
        yield return new WaitWhile(() => false);

        //yield return new WaitForSeconds(spawnTimer);
    }

    private Transform CreateNewDevil() {

        Vector3 position = getNewCircleRandomPosition();
        foreach (Transform enemy in enemies) {
            if (!enemy.gameObject.activeSelf) {
                OnEnemyCreated?.Invoke(this, EventArgs.Empty);
                return enemy.GetComponent<Enemy>().Create(position);
            }
        }

        Transform enemyTransform = Instantiate(devilPredab, position, Quaternion.identity);
        enemies.Add(enemyTransform);
        OnEnemyCreated?.Invoke(this, EventArgs.Empty);
        return enemyTransform;
    }

    private Vector3 getNewCircleRandomPosition() {
        float distance = UnityEngine.Random.Range(minimumSpawnCircleRange, maximumSpawnCircleRange);
        float angle = UnityEngine.Random.Range(0, 359);
        float x = Mathf.Sin(angle);
        float z = Mathf.Cos(angle);
        return player.position + new Vector3(x, 0, z) * distance;
    }

    public int GetEnemiesAlive() {
        int alive = 0;

        foreach (Transform enemy in enemies) {
            if (enemy.gameObject.activeSelf) {
                alive++;
            }
        }

        return alive;
    }
}
