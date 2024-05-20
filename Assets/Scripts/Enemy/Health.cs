using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Animator _animator;

    void Start()
    {
        currentHealth = maxHealth;
        _animator = GetComponent<Animator>();
    }
    

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 50)
        {
            Hit();
        }
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }

    void Hit()
    {
        _animator.SetTrigger("isDead"); 
    }
}