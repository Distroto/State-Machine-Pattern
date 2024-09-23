using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private int attackDamage = 10;
    [SerializeField] private float attackCooldown = 1f;

    private Transform target;
    private AIController aiController;
    private float lastAttackTime;

    private void Start()
    {
        GameObject aiObject = GameObject.FindWithTag("AI");
        if (aiObject != null)
        {
            target = aiObject.transform;
            aiController = aiObject.GetComponent<AIController>();
        }
        else
        {
            Debug.LogError("AI not found in the scene. Make sure it has the 'AI' tag.");
        }
        FollowPlayer();
    }

    private void Update()
    {
        if (target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.position);

            if (distanceToTarget <= attackRange)
            {
                AttackAI();
            }
        }
    }

    private void FollowPlayer()
    {

    }

    private void AttackAI()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;
            aiController.TakeDamage(attackDamage);
            Debug.Log($"Enemy attacked AI for {attackDamage} damage!");
        }
    }
}