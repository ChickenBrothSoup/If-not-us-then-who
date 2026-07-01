using UnityEngine;
using System.Collections;

public class InfantryCombatScript : MonoBehaviour
{
    public MonsterSquadHealthScript monsterSquadHealth;

    public enum State { Idle, Firing }
    public State CurrentState { get; set; }

    [Header("Startup")]
    public float startDelay = 3f;

    [Header("Shooting")]
    public float minTimeBetweenShots = 0.05f;
    public float maxTimeBetweenShots = 0.2f;
    public float damage = 10f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] shootSounds;

    [Header("Spawn Sound")]
    public AudioClip spawnSound;

    private float fireCooldown;
    private bool isLocked = false;

    private void OnEnable()
    {
        MonsterSquadHealthScript.OnMonsterDied += SetIdle;
    }

    private void OnDisable()
    {
        MonsterSquadHealthScript.OnMonsterDied -= SetIdle;
    }

    private void Start()
    {
        CurrentState = State.Idle;
        StartCoroutine(StartAfterDelay());
    }

    private IEnumerator StartAfterDelay()
    {
        yield return new WaitForSeconds(startDelay);

        if (audioSource != null && spawnSound != null)
        {
            audioSource.PlayOneShot(spawnSound);
        }

        StartFiring();
    }

    public void StartFiring()
    {
        if (isLocked) return;

        isLocked = true;
        CurrentState = State.Firing;
        fireCooldown = 0f;
    }

    public void SetIdle()
    {
        CurrentState = State.Idle;
        isLocked = false;
    }

    private void Update()
    {
        if (CurrentState == State.Idle) return;

        transform.rotation = Quaternion.Euler(0f, 0f, 0f);

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            Fire();
            fireCooldown = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
        }
    }

    private void Fire()
    {
        if (monsterSquadHealth != null)
        {
            monsterSquadHealth.TakeDamage(damage);
        }

        if (audioSource != null && shootSounds != null && shootSounds.Length > 0)
        {
            AudioClip randomClip = shootSounds[Random.Range(0, shootSounds.Length)];
            audioSource.PlayOneShot(randomClip);
        }

        Debug.Log("Shot fired!");
    }
}

