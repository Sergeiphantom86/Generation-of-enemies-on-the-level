using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<GameObject> _target;

    private ObjectPool<Enemy> _pool;

    private int _poolCapacity = 50;
    private int _poolMaxSize = 50;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            Create, 
            GetFromPool, 
            ReleaseInPool, 
            Destroy, 
            true, 
            _poolCapacity, 
            _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(WinWithDelay());
    }

    private void OnEnable()
    {
        Enemy._onEnemyMove += SpecifyDirection;
    }

    private void OnDisable()
    {
        Enemy._onEnemyMove -= SpecifyDirection;
    }

    private Enemy Create()
    {
        return Instantiate(_prefab, _spawnPoints[GetIndexSpawnPoint()].transform.position, Quaternion.identity);
    }

    private void GetFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);

        SpecifyDirection(enemy);
    }

    private void ReleaseInPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private int GetIndexSpawnPoint()
    {
        return Random.Range(0, _spawnPoints.Count);
    }

    private void SpecifyDirection(Enemy enemy)
    {
        if (_target.Count >= enemy.GetTargetIndex())
        {
            enemy.SetPointInterest(_target[enemy.SetTargetIndex()]);
        }
    }

    private IEnumerator WinWithDelay()
    {
        int repeatRate = 2;

        while (_pool.CountAll < _poolMaxSize)
        {
            _pool.Get();

            yield return new WaitForSeconds(repeatRate);
        }
    }
}
