using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] enemies;
    [SerializeField] private float followRange = 10f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float attackDuration = 2f;
    [SerializeField] private float retreatDuration = 1f;
    [SerializeField] private float retreatDistance = 3f;


    public AIState currentState;
    public int currentHealth;
    public int maxHealth = 100;
    private NavMeshAgent agent;
    private float stateTimer;
    private bool isRetreating;

    public enum AIState
    {
        Idle,
        FollowPlayer,
        AttackEnemy,
        Dead
    }

    private void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        SetState(AIState.Idle);
    }

    private void Update()
    {
        switch (currentState)
        {
            case AIState.Idle:
                CheckForPlayer();
                break;
            case AIState.FollowPlayer:
                FollowPlayer();
                CheckForEnemies();
                break;
            case AIState.AttackEnemy:
                AttackEnemy();
                break;
            case AIState.Dead:
                // Do nothing when dead
                break;
        }
    }

    private void SetState(AIState newState)
    {
        currentState = newState;
        stateTimer = 0f;
        isRetreating = false;
        Debug.Log("AI State changed to: " + currentState);
    }

    private void CheckForPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) <= followRange)
        {
            SetState(AIState.FollowPlayer);
        }
    }

    private void FollowPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void CheckForEnemies()
    {
        Transform nearestEnemy = GetNearestEnemy();
        if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.position) <= attackRange)
        {
            SetState(AIState.AttackEnemy);
        }
    }

    private void AttackEnemy()
    {
        Transform nearestEnemy = GetNearestEnemy();
        if (nearestEnemy == null)
        {
            SetState(AIState.FollowPlayer);
            return;
        }

        float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.position);

        if (distanceToEnemy > attackRange * 1.5f)
        {
            SetState(AIState.FollowPlayer);
            return;
        }

        stateTimer += Time.deltaTime;

        if (isRetreating)
        {
            // Retreating behavior
            Vector3 retreatDirection = (transform.position - nearestEnemy.position).normalized;
            Vector3 retreatPosition = transform.position + retreatDirection * retreatDistance;
            agent.SetDestination(retreatPosition);

            if (stateTimer >= retreatDuration)
            {
                isRetreating = false;
                stateTimer = 0f;
            }
        }
        else
        {
            // Attacking behavior
            agent.SetDestination(nearestEnemy.position);

            if (stateTimer >= attackDuration)
            {
                isRetreating = true;
                stateTimer = 0f;
            }
        }

        // Perform attack logic here
        if (distanceToEnemy <= attackRange && !isRetreating)
        {
            Debug.Log("AI attacks enemy!");
        }
    }

    private Transform GetNearestEnemy()
    {
        Transform nearest = null;
        float minDistance = float.MaxValue;
        foreach (Transform enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearest = enemy;
            }
        }
        return nearest;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        Debug.Log($"AI took {damage} damage. Current health: {currentHealth}/{maxHealth}");
        if (currentHealth <= 0)
        {
            SetState(AIState.Dead);
            agent.isStopped = true;
            Debug.Log("AI has died!");
        }
    }
}