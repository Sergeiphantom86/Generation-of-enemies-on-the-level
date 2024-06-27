using UnityEngine;

public class Enemy : MonoBehaviour
{
    private readonly float _speedMovement = 0.05f;
    private readonly float _growthRates = 0.1f;

    private GameObject _target;

    private void Awake()
    {
        _target = new();
    }

    private void FixedUpdate()
    {
        IncreaseSize();
        MovGoal();
    }

    public void IncreaseSize()
    {
        if (transform.localScale.x <= 1)
        {
            transform.localScale += transform.lossyScale * _growthRates;
        }
    }

    private void MovGoal()
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
}