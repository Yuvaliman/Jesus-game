using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealthController : MonoBehaviour
{
    public Rigidbody2D body;

    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maximumHealth;
    [SerializeField] private EnemyHealthBar EnemyHealthBar;

    private bool isDeadAnimationStarted = false;

    public float RemainingHealthPercentage
    {
        get { return _currentHealth / _maximumHealth; }
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied;
    public UnityEvent OnDamaged;
    public UnityEvent OnHealthChanged;

    public void TakeDamage(float damageAmount)
    {
        if (IsInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount;

        EnemyHealthBar.ShowHealthChange(damageAmount, false);

        OnHealthChanged.Invoke();

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            OnDied.Invoke();
            body.velocity = Vector2.zero;
            StartCoroutine(DeathRoutine());
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (_currentHealth == _maximumHealth)
        {
            return;
        }

        _currentHealth += amountToAdd;

        EnemyHealthBar.ShowHealthChange(amountToAdd, true);


        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    // Start death animation and destroy the entire prefab
    IEnumerator DeathRoutine()
    {
        if (!isDeadAnimationStarted)
        {
            isDeadAnimationStarted = true;

            // Stop the enemy's movement by setting its velocity to zero
            body.velocity = Vector2.zero;

            // No delay, destroy immediately
            Destroy(transform.root.gameObject);
        }

        isDeadAnimationStarted = false;

        // Yield break to end the coroutine
        yield break;
    }
}
