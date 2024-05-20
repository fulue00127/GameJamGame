using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CHARMOV : MonoBehaviour
{
    public float moveSpeed = 3;
    [HideInInspector] public Vector3 dir;
    private float hzInput, vInput;
    
    private Animator animator;
    
    private NavMeshAgent agent;

    [SerializeField] private LayerMask groundMask;
    private Vector3 spherePos;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetDirectionAndMove();
    }

    void GetDirectionAndMove()
    {
        hzInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        dir = transform.forward * vInput + transform.right * hzInput;

        if (dir != Vector3.zero)
        {
            agent.isStopped = false;
            agent.destination = transform.position + dir;
            animator.SetBool("isWalking", true);
        }
        else
        {
            agent.isStopped = true;
            animator.SetBool("isWalking", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(spherePos, agent.radius - 0.05f);
    }
}