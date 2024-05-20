using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent navMeshAgent;
    public float speed = 1.5f;
    private Animator _animate;
    public float attackRange = 2.0f; // Saldırı menzili
    private EnemyAttack enemyAttack; // EnemyAttack scriptine referans

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _animate = GetComponent<Animator>();
        navMeshAgent.speed = speed;
        enemyAttack = GetComponent<EnemyAttack>(); // EnemyAttack scriptini al
    }

    void Update()
    {
        if (player != null)
        {
            float distance = Vector3.Distance(player.position, transform.position);

            // Eğer düşman saldırı menzilinde değilse oyuncuya doğru hareket etsin
            if (distance > attackRange)
            {
                navMeshAgent.SetDestination(player.position);
                _animate.SetBool("isWalking", true);
            }
            else
            {
                // Saldırı menzilindeyse dur ve saldırı animasyonunu oynat
                navMeshAgent.ResetPath();
                _animate.SetBool("isWalking", false);
                enemyAttack.enabled = true; // Saldırmayı aktif et
            }

            // Düşmanı oyuncuya doğru döndür
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}