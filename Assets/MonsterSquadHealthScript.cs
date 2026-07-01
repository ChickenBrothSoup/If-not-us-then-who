using UnityEngine;
using System.Collections;
using System;

public class MonsterSquadHealthScript : MonoBehaviour
{
    public float monstersHealth = 600f;
    public static event Action OnMonsterDied;

    [Header("Movement")]
    public float startMoveDelay = 3f;
    public float moveSpeed = 2f;
    public float lifetimeAfterMoveStarts = 5f;

    private bool canMove = false;
    private SceneManagerScript sceneTransitionManager;

    private void Awake()
    {
        sceneTransitionManager = FindFirstObjectByType<SceneManagerScript>();
    }

    private void Start()
    {
        StartCoroutine(StartMovingAfterDelay());
    }

    private IEnumerator StartMovingAfterDelay()
    {
        yield return new WaitForSeconds(startMoveDelay);

        canMove = true;

        // Wait before disappearing
        yield return new WaitForSeconds(lifetimeAfterMoveStarts);

        sceneTransitionManager.TriggerTransition();

        Destroy(gameObject);
    }

    private void Update()
    {
        if (canMove)
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        monstersHealth -= damage;
        Debug.Log("Monster health: " + monstersHealth);

        if (monstersHealth <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Monster squad is dead");
        OnMonsterDied?.Invoke();

        if (sceneTransitionManager != null)
        {
            sceneTransitionManager.TriggerTransition();
        }

        Destroy(gameObject);
    }
}


