using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 15f;
    public float killDistance = 2f;
    public float patrolRadius = 10f;
    public float patrolInterval = 5f;

    [Header("Audio Clips")]
    public AudioClip walkClip;
    public AudioClip idleClip;
    public AudioClip killClip;

    [Header("Audio Settings")]
    public float maxVolume = 1f;
    public float minVolume = 0.1f;

    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private bool isPlayerDead = false;
    private float patrolTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        patrolTimer = patrolInterval;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
            else
                Debug.LogError("Player not found! Make sure player GameObject has 'Player' tag.");
        }

        PlayIdleSound();
    }

    void Update()
    {
        if (isPlayerDead || player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        AdjustVolumeBasedOnDistance(distance);

        if (distance <= detectionRange)
        {
            agent.SetDestination(player.position);

            if (distance <= killDistance)
            {
                KillPlayer();
            }
        }
        else
        {
            PatrolRandomly();
        }

        if (animator != null && !isPlayerDead)
        {
            bool isWalking = agent.velocity.magnitude > 0.1f;
            animator.SetBool("isWalking", isWalking);
            HandleMovementSound(isWalking);
        }
    }

    void PatrolRandomly()
    {
        patrolTimer += Time.deltaTime;

        if (patrolTimer >= patrolInterval && !agent.pathPending && agent.remainingDistance <= 0.5f)
        {
            Vector3 newPos = RandomNavSphere(transform.position, patrolRadius, -1);
            agent.SetDestination(newPos);
            patrolTimer = 0f;
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * distance + origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, distance, layermask);
        return navHit.position;
    }

    void KillPlayer()
    {
        isPlayerDead = true;
        Debug.Log("Player caught!");

        agent.isStopped = true;
        animator.SetTrigger("Kill");
        PlayKillSound();

        Invoke("RestartScene", 2f);
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlayerDead && other.CompareTag("Player"))
        {
            KillPlayer();
        }
    }

    // --- Audio Handling ---

    void HandleMovementSound(bool isWalking)
    {
        if (audioSource == null) return;

        if (isWalking)
        {
            if (audioSource.clip != walkClip)
            {
                audioSource.clip = walkClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.clip != idleClip)
            {
                audioSource.clip = idleClip;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
    }

    void PlayKillSound()
    {
        if (audioSource == null || killClip == null) return;

        audioSource.Stop();
        audioSource.loop = false;
        audioSource.clip = killClip;
        audioSource.Play();
    }

    void PlayIdleSound()
    {
        if (audioSource == null || idleClip == null) return;

        audioSource.clip = idleClip;
        audioSource.loop = true;
        audioSource.Play();
    }

    // --- Volume Control Based on Distance ---
    void AdjustVolumeBasedOnDistance(float distance)
    {
        if (audioSource == null) return;

        // Jika di luar detection range, volume minimum
        if (distance > detectionRange)
        {
            audioSource.volume = minVolume;
            return;
        }

        // Hitung volume berdasarkan kedekatan
        float t = Mathf.Clamp01(1 - (distance / detectionRange));
        float targetVolume = Mathf.Lerp(minVolume, maxVolume, t);
        audioSource.volume = targetVolume;
    }
}
