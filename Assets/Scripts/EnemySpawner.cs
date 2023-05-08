using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;
using static UnityEngine.EventSystems.EventTrigger;

public class EnemySpawner : MonoBehaviour {

    public static EnemySpawner Instance { get; private set; }

    public event EventHandler OnEnemyCreated;

    [SerializeField] private Transform player;
    [SerializeField] private Transform devilPredab;
    [SerializeField] private Transform goatPrefab;
    [SerializeField] private Transform chonkPrefab;
    [SerializeField] private Transform matriarchPrefab;
    [SerializeField] private float minimumSpawnCircleRange = 5;
    [SerializeField] private float maximumSpawnCircleRange = 10;
    [SerializeField] private float spawnTimer = 3;
    [SerializeField] private int spawnEnemiesQuantity = 20;

    [SerializeField] private float waveNumber = 1;

    private Dictionary<Enemy.EnemyType, List<Transform>> enemiesDictionary = new Dictionary<Enemy.EnemyType, List<Transform>>();

    private void Awake() {
        Instance = this;
        enemiesDictionary.Add(Enemy.EnemyType.Devil, new List<Transform>());
        enemiesDictionary.Add(Enemy.EnemyType.Goat, new List<Transform>());
        enemiesDictionary.Add(Enemy.EnemyType.Chonk, new List<Transform>());
        enemiesDictionary.Add(Enemy.EnemyType.Matriarch, new List<Transform>());
    }

    // Start is called before the first frame update
    void Start() {
        StartCoroutine(SpawnEnemyWave());
    }

    // Update is called once per frame
    void Update() {

    }
    public IEnumerator SpawnEnemyWave() {
        StartCoroutine(CameraSystem.Instance.ShakeCamera(5, 3f));
        System.Random r = new System.Random();
        int tauntChance = r.Next(0, 10);
        tauntChance = UnityEngine.Random.Range(0, 10);
        if (tauntChance %2 == 0) {
            SoundManager.Instance.PlayYouWillDie(this.transform);
        }
        for (int i = 0; i < spawnEnemiesQuantity; i++) {
            Transform devil = CreateNewDevil(RandomEnumValue());
        }
        yield return new WaitWhile(() => false);

        //yield return new WaitForSeconds(spawnTimer);
    }

    private Transform CreateNewDevil(Enemy.EnemyType enemyType) {
        enemiesDictionary.TryGetValue(enemyType, out List<Transform> enemies);

        Vector3 position = getNewCircleRandomPosition();
        foreach (Transform enemy in enemies) {
            if (!enemy.gameObject.activeSelf) {
                OnEnemyCreated?.Invoke(this, EventArgs.Empty);
                return enemy.GetComponent<Enemy>().Create(position);
            }
        }

        Transform enemyTransform = InstantiateEnemy(enemyType, position);
        enemies.Add(enemyTransform);
        OnEnemyCreated?.Invoke(this, EventArgs.Empty);
        return enemyTransform;
    }

    private Transform InstantiateEnemy(Enemy.EnemyType enemyType, Vector3 position) {
        switch (enemyType) {
            case Enemy.EnemyType.Devil: return Instantiate(devilPredab, position, Quaternion.identity);
            case Enemy.EnemyType.Goat: return Instantiate(goatPrefab, position, Quaternion.identity);
            case Enemy.EnemyType.Chonk: return Instantiate(chonkPrefab, position, Quaternion.identity);
            case Enemy.EnemyType.Matriarch: return Instantiate(matriarchPrefab, position, Quaternion.identity);
            default: throw new NotImplementedException("Invalid enemyType!" + enemyType);
        }
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

        Dictionary<Enemy.EnemyType, List<Transform>>.Enumerator enumerator = enemiesDictionary.GetEnumerator();
        while (enumerator.MoveNext()) {
            Enemy.EnemyType key = enumerator.Current.Key;
            List<Transform> transforms = enemiesDictionary[key];
            foreach (Transform enemy in transforms) {
                if (enemy.gameObject.activeSelf) {
                    alive++;
                }
            }
        }

        return alive;
    }

    public void StunAllEnemies() {
        Dictionary<Enemy.EnemyType, List<Transform>>.Enumerator enumerator = enemiesDictionary.GetEnumerator();
        while (enumerator.MoveNext()) {
            Enemy.EnemyType key = enumerator.Current.Key;
            List<Transform> transforms = enemiesDictionary[key];
            foreach (Transform enemy in transforms) {
                if (enemy.gameObject.activeSelf) {
                    StartCoroutine(StunEnemy(3, enemy));
                }
            }
        }
    }

    private IEnumerator StunEnemy(float duration, Transform enemyTransform) {
        Enemy enemy = enemyTransform.gameObject.GetComponent<Enemy>();
        Animator animator = enemyTransform.GetComponent<Animator>();
        if (animator == null) {
            animator = enemyTransform.GetComponentInChildren<Animator>();
        }
        ParticleSystem enemyParticleSystem = enemyTransform.GetComponentInChildren<ParticleSystem>();

        enemyParticleSystem?.gameObject?.SetActive(false);
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("Die");
        animator.ResetTrigger("Stun");
        animator.ResetTrigger("Walk");

        animator.SetTrigger("Stun");
        enemy.SetStunEnemy(true);
        yield return new WaitForSeconds(duration);
        animator.ResetTrigger("Stun");
        animator.SetTrigger("Walk");
        enemy.SetStunEnemy(false);
        enemyParticleSystem?.gameObject?.SetActive(true);
    }

    private EnemyType RandomEnumValue() {
        int dieValue = UnityEngine.Random.Range(0, 100);
        if (dieValue > 95) {
            return EnemyType.Matriarch;
        }
        if (dieValue > 70) {
            return EnemyType.Chonk;
        }
        if (dieValue > 40) {
            return EnemyType.Goat;
        }
        return EnemyType.Devil;
    }
}
