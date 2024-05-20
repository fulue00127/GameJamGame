using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2.0f;
    public int damage = 10;
    public float attackCooldown = 1.0f;
    private float lastAttackTime;
    private Transform player;
    private Animator animator;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>(); // Animator bileşenini al
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
        }

        // Düşmanı oyuncuya doğru döndür
        if (distance <= attackRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack"); // Saldırı animasyonunu tetikle
        StartCoroutine(DealDamage()); // Hasarı zamanlamak için bir coroutine başlat
    }

    IEnumerator DealDamage()
    {
        yield return new WaitForSeconds(0.5f); // Hasarı uygulamadan önce bekleme süresi (animasyonun ortasında)
        if (Vector3.Distance(player.position, transform.position) <= attackRange)
        {
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }
}