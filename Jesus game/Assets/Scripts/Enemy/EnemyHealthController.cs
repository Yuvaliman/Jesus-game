using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EnemyHealthController : MonoBehaviour
{
    public Rigidbody2D body;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    bool isDeadAnimationStarted = false;
    float deathDelay = 1.0f;

    public float RemainingHealthPercentage
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
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

        OnHealthChanged.Invoke();

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();
            body.velocity = new Vector2(0, 0);
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

        OnHealthChanged.Invoke();

        if (_currentHealth > _maximumHealth)
        {
            _currentHealth = _maximumHealth;
        }
    }

    // start death animation

    IEnumerator DeathRoutine()
    {
        if (!isDeadAnimationStarted)
        {
            isDeadAnimationStarted = true;
        }

        yield return new WaitForSeconds(deathDelay);


        isDeadAnimationStarted = false;
    }
}
