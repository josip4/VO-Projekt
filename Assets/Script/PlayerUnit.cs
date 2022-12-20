using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : BaseUnit
{
    private int _playerMask;
    private GameObject _target;
    private bool _canAttack = true;

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerMask = LayerMask.NameToLayer("Player");
        _agent.speed = _moveSpeed;
    }
    void Update()
    {
        Move();
        TargetEnemy();
        MoveToTarget();
    }
    public override void Attack (GameObject _target)
    {
        if (!_canAttack) return;
        _canAttack = false;
        _target.GetComponent<BaseUnit>().TakeDamage(_attack);
        StartCoroutine(AttackTimer());
    }
    
    private void TargetEnemy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray clickPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(clickPosition, out var hit, Mathf.Infinity)) return;
            print("Atack");
            if (!(hit.transform.tag == "Enemy")) return;
            _target = hit.transform.gameObject;
        }
    }
    private void MoveToTarget()
    {
        if (!_target) return;
        float distance = Vector3.Distance (_target.transform.position, transform.position);
        if (distance > _attackRange)
        {
            _agent.isStopped = false;
            _agent.destination = _target.GetComponent<BaseUnit>().Position;
        }
        else
        {
            _agent.isStopped = true;
            Attack(_target);
        }

    }
    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            _target = null;
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePosition, out var hitInfo, Mathf.Infinity, _playerMask))
            {
                _agent.isStopped = false;
                _agent.SetDestination(hitInfo.point);
            }
        }
    }
    IEnumerator AttackTimer()
    {
        // _animator.speed = _attackSpeed;
        yield return new WaitForSeconds(1 / _attackSpeed);
        _canAttack = true;
        // _canMove = true;
    }

    protected override void Die()
    {
        throw new System.NotImplementedException();
    }
}
