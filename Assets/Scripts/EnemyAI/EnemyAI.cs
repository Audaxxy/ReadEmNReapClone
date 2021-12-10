using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{

    private enum EnemyState
    {
        Chase = 0,
        Attack = 1,
        Dead =2
    }

    #region Private Value
    private NavMeshAgent agent;
    private Animator animator;

    [Header("Debuging")]
    [SerializeField] EnemyState currentState;
    [SerializeField] float attackTurnSpeed;
    private GameObject player;
    private int recoil=30;
    #endregion

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");
        agent.destination = player.transform.position;
    }
	
	private void  FixedUpdate()
    {
        agent.destination = player.transform.position;
        switch (currentState)
        {
            case EnemyState.Chase:
                UpdateChaseState();
                break;
            case EnemyState.Attack:
                UpdateAttackState();
                break;
            case EnemyState.Dead:
                UpdateDeadState();
                break;
        }
    }


    private void UpdateChaseState()
    {
        //if(health <=0){currentState = EnemyState.Dead}
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    currentState = EnemyState.Attack;
                }
            }

           // Debug.Log("Now is in Chase State");
        }

        //animator.SetInteger("CurrentState",(int)EnemyState.Chase);
    }

    private void UpdateAttackState()
    {
        //if(health <=0){currentState = EnemyState.Dead}
        if (agent.pathPending)
        {
            if (agent.remainingDistance > agent.stoppingDistance)
            {
                if (agent.hasPath || agent.velocity.sqrMagnitude > 0f)
                {
                    currentState = EnemyState.Chase;
                }
            }
        }
        if (recoil >= 30)
        {
            //player.GetComponent<PlayerHealth>().ReduceHealth();
            recoil = 0;
        }
        recoil++;
        //Debug.Log("Now is in Attack State");
    }

    private void UpdateDeadState()
    {
        agent.enabled = false;
        //animator.SetInteger("CurrentState", (int)EnemyState.Dead);
    }
}
