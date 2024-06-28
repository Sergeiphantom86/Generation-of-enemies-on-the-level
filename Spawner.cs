using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _prefab;
    [SerializeField] private Transform _target;
    [SerializeField] private List<Transform> _spawnPoints;

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

    private Enemy Create()
    {
        return Instantiate(_prefab, _spawnPoints[GetIndexSpawnPoint()].position, Quaternion.identity);
    }

    private void GetFromPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);

        enemy.SetTargetPosition(_target.position);
    }

    private void ReleaseInPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private int GetIndexSpawnPoint()
    {
        return Random.Range(0, _spawnPoints.Count);
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
