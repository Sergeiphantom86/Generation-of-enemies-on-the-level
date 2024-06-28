using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Vector3 _target;
    private float _speed = 0.1f;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target + Vector3.forward, _speed);
    }

    public void SetTargetPosition(Vector3 target)
    {
        _target = target;
    }
}
