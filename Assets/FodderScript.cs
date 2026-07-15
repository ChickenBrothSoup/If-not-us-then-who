using UnityEngine;
using System.Collections;

public class FodderScript : MonoBehaviour
{
    public enum EnemyState
    {
        Idle,
        Active,
        Attacking,
        Dead
    }

    public EnemyState CurrentState { get; set; }
    public Collider2D meleeCollider;
    public float attackDuration = 0.3f;
    public float attackCooldown = 1f;

    [Header("Movement Speed")]
    public float speed = 4f;

    private bool isAttacking = false;
    private Coroutine attackLoopCoroutine;

    private void Awake()
    {
        meleeCollider.enabled = true;
    }

    void Start()
    {
        CurrentState = EnemyState.Active;
    }

    void Update()
    {
        if (CurrentState == EnemyState.Active)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Human"))
        {
            CurrentState = EnemyState.Attacking;

            if (!isAttacking)
            {
                attackLoopCoroutine = StartCoroutine(AttackLoop());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Human"))
        {
            CurrentState = EnemyState.Active;

            if (attackLoopCoroutine != null)
            {
                StopCoroutine(attackLoopCoroutine);
            }

            isAttacking = false;
        }
    }

    private IEnumerator AttackLoop()
    {
        isAttacking = true;

        while (CurrentState == EnemyState.Attacking)
        {
            yield return StartCoroutine(MeleeAttackRoutine());
            yield return new WaitForSeconds(attackCooldown);
        }

        isAttacking = false;
    }

    private IEnumerator MeleeAttackRoutine()
    {
        meleeCollider.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        meleeCollider.enabled = false;
    }
}
