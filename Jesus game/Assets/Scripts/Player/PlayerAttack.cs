using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;

    [SerializeField]
    private float _attackRange;

    [SerializeField]
    private LayerMask _enemyLayerMask; // Drag and drop the layer(s) containing your enemies in the Inspector

    [SerializeField]
    private float _attackDelay = 0.5f; // Adjust the delay time as needed

    private bool _canAttack = true;

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canAttack)
        {
            StartCoroutine(AttackWithDelay());
        }
    }

    IEnumerator AttackWithDelay()
    {
        Attack();
        _canAttack = false;
        yield return new WaitForSeconds(_attackDelay);
        _canAttack = true;
    }

    void Attack()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        Vector2 attackDirection = (mousePosition - transform.position).normalized;

        // Visualize the ray
        Debug.DrawRay(transform.position, attackDirection * _attackRange, Color.red, 0.5f);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, attackDirection, _attackRange, _enemyLayerMask);

        if (hit.collider != null)
        {
            EnemyHealthController enemyHealth = hit.collider.GetComponent<EnemyHealthController>();

            if (enemyHealth != null)
            {
                // Damage the enemy
                enemyHealth.TakeDamage(_damageAmount);
            }
        }
    }
}
