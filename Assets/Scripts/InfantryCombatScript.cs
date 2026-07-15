using UnityEngine;
using System.Collections;

public class InfantryCombatScript : MonoBehaviour
{
    public MonsterSquadHealthScript monsterSquadHealth;
    public enum State { Idle, Running, Firing, Dead }
    public State CurrentState { get; set; }

    [Header("Startup")]
    public float startDelay = 3f;

    [Header("Shooting")]
    public float minTimeBetweenShots = 0.05f;
    public float maxTimeBetweenShots = 0.2f;
    public float damage = 10f;
    public float muzzleFlashDuration = 0.05f; // how long the sprite stays "on" per shot

    [Header("Muzzle Flash Sprite")]
    public GameObject muzzleFlashSprite; // drag a child GameObject (with a SpriteRenderer) here

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip[] shootSounds;

    [Header("Spawn Sound")]
    public AudioClip spawnSound;

    [Header("Speed")]
    public float speed = 5f;

    [Header("Collision")]
    public string stopTag = "Wall"; // tag of whatever should stop the monster and trigger firing

    private bool isLocked = false;
    private Coroutine fireRoutine;

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
        CurrentState = State.Running;

        if (muzzleFlashSprite != null)
            muzzleFlashSprite.SetActive(false);
    }

    private void Update()
    {
        if (CurrentState == State.Firing || CurrentState == State.Dead)
        {
            return;
        }

        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    // --- Collision handling ---
    // Use whichever matches your collider setup; delete the ones you don't need.

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(stopTag) && CurrentState == State.Running)
        {
            StartFiring();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(stopTag) && CurrentState == State.Running)
        {
            StartFiring();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(stopTag) && CurrentState == State.Running)
        {
            StartFiring();
        }
    }

    // --- Firing ---

    public void StartFiring()
    {
        if (isLocked) return;
        isLocked = true;
        CurrentState = State.Firing;
        fireRoutine = StartCoroutine(FireRoutine());
    }

    public void SetRunning()
    {
        if (fireRoutine != null)
            StopCoroutine(fireRoutine);

        if (muzzleFlashSprite != null)
            muzzleFlashSprite.SetActive(false);

        CurrentState = State.Running;
        isLocked = false;
    }

    public void SetIdle()
    {
        if (fireRoutine != null)
            StopCoroutine(fireRoutine);

        if (muzzleFlashSprite != null)
            muzzleFlashSprite.SetActive(false);

        CurrentState = State.Idle;
        isLocked = false;
    }

    private IEnumerator FireRoutine()
    {
        yield return new WaitForSeconds(startDelay);

        while (CurrentState == State.Firing)
        {
            Fire();
            yield return new WaitForSeconds(muzzleFlashDuration);

            if (muzzleFlashSprite != null)
                muzzleFlashSprite.SetActive(false);

            float wait = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
            yield return new WaitForSeconds(wait);
        }
    }

    public void Fire()
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

        if (muzzleFlashSprite != null)
            muzzleFlashSprite.SetActive(true);

        Debug.Log("Shot fired!");
    }

    public void Die()
    {
        if (fireRoutine != null)
            StopCoroutine(fireRoutine);

        CurrentState = State.Dead;
        Destroy(gameObject);
    }
    //public MonsterSquadHealthScript monsterSquadHealth;

    //public enum State { Idle, Running, Firing, Dead }
    //public State CurrentState { get; set; }

    //[Header("Startup")]
    //public float startDelay = 3f;

    //[Header("Shooting")]
    //public float minTimeBetweenShots = 0.05f;
    //public float maxTimeBetweenShots = 0.2f;
    //public float damage = 10f;

    //[Header("Audio")]
    //public AudioSource audioSource;
    //public AudioClip[] shootSounds;

    //[Header("Spawn Sound")]
    //public AudioClip spawnSound;

    //private float fireCooldown;
    //private bool isLocked = false;

    //[Header("Speed")]
    //public float speed = 5f;

    //private void OnEnable()
    //{
    //    MonsterSquadHealthScript.OnMonsterDied += SetIdle;
    //}

    //private void OnDisable()
    //{
    //    MonsterSquadHealthScript.OnMonsterDied -= SetIdle;
    //}

    //private void Start()
    //{
    //    CurrentState = State.Running;

    //}



    //public void StartFiring()
    //{
    //    if (isLocked) return;

    //    isLocked = true;
    //    CurrentState = State.Firing;
    //    fireCooldown = 0f;
    //}

    //public void SetRunning()
    //{
    //    CurrentState = State.Running;
    //    isLocked = false;
    //}

    //public void SetIdle()
    //{

    //}

    //private void Update()
    //{
    //    if (CurrentState == State.Firing)
    //    {
    //        return;
    //    }




    //    transform.Translate(Vector3.right * speed * Time.deltaTime);

    //transform.rotation = Quaternion.Euler(0f, 0f, 0f);

    //fireCooldown -= Time.deltaTime;

    //if (fireCooldown <= 0f)
    //{
    //    Fire();
    //    fireCooldown = Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    //}
    //}

    //public void Fire()
    //{
    //if (monsterSquadHealth != null)
    //{
    //    monsterSquadHealth.TakeDamage(damage);
    //}

    //if (audioSource != null && shootSounds != null && shootSounds.Length > 0)
    //{
    //    AudioClip randomClip = shootSounds[Random.Range(0, shootSounds.Length)];
    //    audioSource.PlayOneShot(randomClip);
    //}
    //    CurrentState = State.Firing;

    //    Debug.Log("Shot fired!");


    //}

    //public void Die()
    //{
    //    CurrentState = State.Dead;
    //    Destroy(gameObject);
    //}
}

