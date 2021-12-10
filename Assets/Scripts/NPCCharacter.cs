using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCCharacter : MonoBehaviour
{
    public GameObject Target;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private float _speed;
    private float _distance;
    // Start is called before the first frame update
    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
      

    }

    // Update is called once per frame
    void Update()
    {
        _speed = _navMeshAgent.velocity.magnitude;
        _distance = Vector3.Distance(gameObject.transform.position, Target.transform.position);

       _animator.SetFloat("speed", _speed);
       
        if(_distance < 1.5)
        {
            _animator.SetBool("dance", true);
        }
        else
        {
            _animator.SetBool("dance", false);
        }
        _navMeshAgent.destination = Target.transform.position;
    }
}
