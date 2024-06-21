using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent m_Agent;
    [SerializeField] private Transform player;
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Agent.destination = player.position;

        // Calculate the speed of the NavMeshAgent
        float speed = m_Agent.velocity.magnitude;

        // Update the Animator parameter
        animator.SetFloat("movement", speed);
    }
}
