using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject _target;

    private float _speedMovement = 0.05f;
    private float _growthRates = 0.1f;
    private int _targetIndex;

    public static Action<Enemy> _onEnemyMove;

    private void Awake()
    {
        _targetIndex = 0;
        _target = new();
    }

    private void FixedUpdate()
    {
        IncreaseSize();
        MovePosition();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<Target>())
        {
            _onEnemyMove?.Invoke(this);
        }
    }

    public void IncreaseSize()
    {
        if (transform.localScale.x <= 1)
        {
            transform.localScale += transform.lossyScale * _growthRates;
        }
    }

    private void MovePosition()
    {
        if (transform.localScale.x >= 1)
        {
            transform.LookAt(_target.transform.position);
            transform.position += transform.forward * _speedMovement;
        }
    }

    public void SetPointInterest(GameObject target)
    {
        _target = target;
    }

    public int SetTargetIndex()
    {
        return _targetIndex++;
    }

    public int GetTargetIndex()
    {
        return _targetIndex;
    }
}
