using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    enum EnemyType {
        Devil,
        Goat,
        Chonk,
        Matriarch
    }

    private Dictionary<EnemyType, List<Transform>> enemiesDictionary = new Dictionary<EnemyType, List<Transform>>();

    private void Awake() {
        Instance = this;
        enemiesDictionary.Add(EnemyType.Devil, new List<Transform>());
        enemiesDictionary.Add(EnemyType.Goat, new List<Transform>());
        enemiesDictionary.Add(EnemyType.Chonk, new List<Transform>());
        enemiesDictionary.Add(EnemyType.Matriarch, new List<Transform>());
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
            Transform devil = CreateNewDevil(RandomEnumValue<EnemyType>());
        }
        yield return new WaitWhile(() => false);

        //yield return new WaitForSeconds(spawnTimer);
    }

    private Transform CreateNewDevil(EnemyType enemyType) {
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

    private Transform InstantiateEnemy(EnemyType enemyType, Vector3 position) {
        switch (enemyType) {
            case EnemyType.Devil: return Instantiate(devilPredab, position, Quaternion.identity);
            case EnemyType.Goat: return Instantiate(goatPrefab, position, Quaternion.identity);
            case EnemyType.Chonk: return Instantiate(chonkPrefab, position, Quaternion.identity);
            case EnemyType.Matriarch: return Instantiate(matriarchPrefab, position, Quaternion.identity);
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

        Dictionary<EnemyType, List<Transform>>.Enumerator enumerator = enemiesDictionary.GetEnumerator();
        while (enumerator.MoveNext()) {
            EnemyType key = enumerator.Current.Key;
            List<Transform> transforms = enemiesDictionary[key];
            foreach (Transform enemy in transforms) {
                if (enemy.gameObject.activeSelf) {
                    alive++;
                }
            }
        }

        //foreach (Transform enemy in enemies) {
        //    if (enemy.gameObject.activeSelf) {
        //        alive++;
        //    }
        //}

        return alive;
    }

    public void StunAllEnemies() {
        Dictionary<EnemyType, List<Transform>>.Enumerator enumerator = enemiesDictionary.GetEnumerator();
        while (enumerator.MoveNext()) {
            EnemyType key = enumerator.Current.Key;
            List<Transform> transforms = enemiesDictionary[key];
            foreach (Transform enemy in transforms) {
                if (enemy.gameObject.activeSelf) {
                    //Animator animator = enemy.GetComponent<Animator>();
                    //Animator animator = enemy.GetComponent<AnimatorTriggerReset>().resetAll();
                    //.SetTrigger("Stun");
                }
            }
        }
    }

    static T RandomEnumValue<T>() {
        var value = Enum.GetValues(typeof(T));
        return (T)value.GetValue(UnityEngine.Random.Range(0, value.Length));
    }
}
