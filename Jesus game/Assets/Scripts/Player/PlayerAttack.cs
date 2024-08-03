using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float swordDamageAmount = 10f;
    [SerializeField] private float swordAttackRange = 1.5f;
    [SerializeField] private float bowDamageAmount = 15f;
    [SerializeField] private float bowAttackRange = 10f;
    [SerializeField] private float attackDelay = 0.25f;

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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(AttackWithDelay(true)); // True for sword attack
        }
        else if (Input.GetMouseButtonDown(1) && canAttack)
        {
            StartCoroutine(AttackWithDelay(false)); // False for bow attack
        }
    }

    private IEnumerator AttackWithDelay(bool isSword)
    {
        Attack(isSword);
        canAttack = false;
        yield return new WaitForSeconds(attackDelay);
        canAttack = true;
    }

    private void Attack(bool isSword)
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 attackDirection = (mousePosition - transform.position).normalized;
        float damageAmount;
        float attackRange;
        Color rayColor;

        if (isSword)
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
