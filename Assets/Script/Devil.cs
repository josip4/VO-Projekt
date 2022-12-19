using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Devil : BaseUnit
{
    [SerializeField]
    private float _minPlayerRange = 10f;
    private bool _canMove = true;
    private bool _canAttack = true;
    private Animator _animator;
    private NavMeshAgent _agent;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
        bool inRange = PlayerInAttackRange(out PlayerUnit player);
        if (inRange) Attack<PlayerUnit>(player);
        else _animator.SetBool("Attacking", false);
    }
    private bool PlayerInAttackRange(out PlayerUnit player)
    {
        player = FindObjectOfType<PlayerUnit>();
        float distance = Vector3.Distance (player.transform.position, transform.position);
        return distance <= _attackRange;
    }
    
    private bool PlayerInRange(out PlayerUnit player)
    {
        player = FindObjectOfType<PlayerUnit>();
        float distance = Vector3.Distance (player.transform.position, transform.position);
        return distance <= _minPlayerRange;

    }
    private void FollowPlayer()
	{
        if (!_canMove)
        {
            _agent.isStopped = true;
            return;
        }
        _animator.speed = 1;
        bool inRange = PlayerInRange(out PlayerUnit player);
        _animator.SetBool("Moving", inRange);
        if (inRange)
        {
            _agent.isStopped = false;
            _agent.destination = player.Position;
        }
        else
        {
            _agent.isStopped = true;
        }
    }
    public override void Attack<T>(T target)
    {
        if (!_canAttack) return;
        _canAttack = false;
        _canMove = false;
        _animator.SetBool("Attacking", true);
        StartCoroutine(AttackTimer());
        target.TakeDamage(_attack);        
    }
    IEnumerator AttackTimer()
    {
        _animator.speed = _attackSpeed;
        yield return new WaitForSeconds(1 / _attackSpeed);
        _canAttack = true;
        _canMove = true;
    }
}
