using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed;
    [SerializeField]
    private float _minIdleTime = 1f;
    [SerializeField]
    private float _maxIdleTime = 3f;

    private Rigidbody2D _rigidbody;
    private PlayerAwarenessController _playerAwarenessController;
    private Vector2 _randomDirection;
    private bool _isIdle;
    private float _idleTimer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAwarenessController = GetComponent<PlayerAwarenessController>();
    }

    private void FixedUpdate()
    {
        if (_playerAwarenessController.AwareOfPlayer)
        {
            MoveTowardsPlayer();
        }
        else
        {
            if (!_isIdle)
            {
                _isIdle = true;
                _idleTimer = Random.Range(_minIdleTime, _maxIdleTime);
            }
            else
            {
                _idleTimer -= Time.fixedDeltaTime;
                if (_idleTimer <= 0)
                {
                    _isIdle = false;
                }
            }
            if (!_isIdle)
            {
                _randomDirection = Random.insideUnitCircle.normalized;
            }
            MoveRandomly();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector2 direction = _playerAwarenessController.DirectionToPlayer.normalized;
        _rigidbody.velocity = direction * _speed;
    }

    private void MoveRandomly()
    {
        _rigidbody.velocity = _randomDirection * _speed;
    }
}
