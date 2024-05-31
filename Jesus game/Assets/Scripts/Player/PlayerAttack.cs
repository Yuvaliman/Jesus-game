using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float swordDamageAmount = 10f;
    [SerializeField] private float swordAttackRange = 1.5f;
    [SerializeField] private float bowDamageAmount = 15f;
    [SerializeField] private float bowAttackRange = 10f;
    [SerializeField] private float swordAttackDelay = 0.5f;
    [SerializeField] private float bowAttackDelay = 1.5f;

    private bool canAttack = true;
    private WeaponSystem weaponSystem;

    private void Start()
    {
        weaponSystem = GetComponent<WeaponSystem>();
        if (weaponSystem == null)
        {
            Debug.LogError("WeaponSystem component is missing from the GameObject.");
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(AttackWithDelay());
        }
    }

    private IEnumerator AttackWithDelay()
    {
        Attack();
        canAttack = false;
        if (weaponSystem.IsSwordActive())
        {
            yield return new WaitForSeconds(swordAttackDelay);
        } else
        {
            yield return new WaitForSeconds(bowAttackDelay);
        }
        canAttack = true;
    }

    private void Attack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 attackDirection = (mousePosition - transform.position).normalized;
        float damageAmount;
        float attackRange;
        Color rayColor;

        if (weaponSystem.IsSwordActive())
        {
            damageAmount = swordDamageAmount;
            attackRange = swordAttackRange;
            rayColor = Color.red; // Sword attack ray color
            Debug.Log("Sword attack initiated.");
        }
        else
        {
            damageAmount = bowDamageAmount;
            attackRange = bowAttackRange;
            rayColor = Color.blue; // Bow attack ray color
            Debug.Log("Bow attack initiated.");
        }

        Debug.DrawRay(transform.position, attackDirection * attackRange, rayColor, 0.5f);

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, attackDirection, attackRange);

        bool hitDetected = false;

        foreach (RaycastHit2D hit in hits)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                hitDetected = true;
                Debug.Log("Hit detected on " + hit.collider.name);
                EnemyHealthController enemyHealth = hit.collider.GetComponent<EnemyHealthController>();

                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damageAmount);
                    Debug.Log("Enemy health reduced by " + damageAmount);
                }
                else
                {
                    Debug.Log("EnemyHealthController component is missing on the hit object.");
                }
            }
        }

        if (!hitDetected)
        {
            Debug.Log("No enemy hit detected.");
        }
    }
}
