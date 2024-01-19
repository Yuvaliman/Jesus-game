using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    private void FixedUpdate()
    {
        SetVelocity();
    }

    private void SetVelocity()
    {
        Vector2 direction;

        if (_playerAwarenessController.AwareOfPlayer)
        {
            direction = _playerAwarenessController.DirectionToPlayer.normalized;
        }
        else
        {
            direction = transform.up;
        }

        _rigidbody.velocity = direction * _speed;
    }
}
