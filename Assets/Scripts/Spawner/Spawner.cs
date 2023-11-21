using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Spawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private Poolable[] _prefabs;
    [SerializeField] private Transform _container;
    [SerializeField] private int _spawnsBeforeReuse;
    [SerializeField] private float _timeBetweenSpawns;

    [Header("Pool Settings")]
    [SerializeField] private bool _collectionCheck;
    [SerializeField] private int _defaultCapacity;
    [SerializeField] private int _maxSize;

    private List<SpawnPoint> _spawnPoints;
    private Queue<SpawnPoint> _recentlyUsedSpawnPoints;
    private Dictionary<string, ObjectPool<Poolable>> _pools = new();

    private float _elapsedTime;

    private void Awake()
    {
        _spawnPoints = new List<SpawnPoint>(GetComponentsInChildren<SpawnPoint>());
        _recentlyUsedSpawnPoints = new Queue<SpawnPoint>(_spawnsBeforeReuse);

        CreatePools();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void CreatePools()
    {
        foreach (Poolable prefab in _prefabs)
        {
            if (_pools.ContainsKey(prefab.name))
                continue;

            Poolable createItem()
            {
                Poolable poolable = Instantiate(prefab, _container);
                poolable.name = prefab.name;
                return poolable;
            }

            var pool = new ObjectPool<Poolable>(
                createItem, OnTakeFromPool, OnReturnToPool, OnDestroyPoolObject,
                _collectionCheck, _defaultCapacity, _maxSize);

            _pools.Add(prefab.name, pool);
        }
    }

    private void OnTakeFromPool(Poolable poolableObject)
    {
        SpawnPoint spawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

        RemoveSpawnPoint(spawnPoint);

        poolableObject.transform.position = spawnPoint.transform.position;
        poolableObject.gameObject.SetActive(true);

        poolableObject.SetPool(_pools[poolableObject.name]);
    }

    private void OnReturnToPool(Poolable poolableObject)
    {
        poolableObject.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(Poolable poolableObject)
    {
        Destroy(poolableObject.gameObject);
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            while (_elapsedTime < _timeBetweenSpawns)
            {
                _elapsedTime += Time.deltaTime;
                yield return null;
            }

            var randomPool = GetRandomPool();
            randomPool.Get();

            RefreshSpawnPoints();

            _elapsedTime = 0f;
        }
    }

    private void RefreshSpawnPoints()
    {
        if (_recentlyUsedSpawnPoints.Count < _spawnsBeforeReuse)
            return;

        SpawnPoint spawnPoint = _recentlyUsedSpawnPoints.Dequeue();
        _spawnPoints.Add(spawnPoint);
    }

    private void RemoveSpawnPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Remove(spawnPoint);
        _recentlyUsedSpawnPoints.Enqueue(spawnPoint);
    }

    private ObjectPool<Poolable> GetRandomPool()
        => _pools.ElementAt(Random.Range(0, _pools.Count)).Value;
}
