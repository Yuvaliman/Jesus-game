using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    public Rigidbody2D body;

    public Crossfade Crossfade;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    public Animator anim;

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

    IEnumerator DeathRoutine()
    {
        if (!isDeadAnimationStarted)
        {
            anim.SetBool("IsDead", true);
            isDeadAnimationStarted = true;
        }

        yield return new WaitForSeconds(deathDelay);

        Crossfade.CrossfadeAnimation(2);

        isDeadAnimationStarted = false;
    }
}
