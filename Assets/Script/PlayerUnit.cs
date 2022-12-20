using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : BaseUnit
{
    private int _playerMask;
    private GameObject _target;
    private bool _canAttack = true;
    private bool _canMove = true;
    public Animator _animator;
    public GameObject deathVFX;

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
        FaceTarget(_target);
        if (!_canAttack) return;
        _canAttack = false;
        StartCoroutine(AttackTimer());
    }

    private void TargetEnemy()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray clickPosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(clickPosition, out var hit, Mathf.Infinity)) return;
            if (!(hit.transform.tag == "Enemy")) return;
            _agent.updateRotation = false;
            _target = hit.transform.gameObject;
        }
    }
    private void MoveToTarget()
    {
        if (!_target) {
            return;
        }
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
        _animator.SetFloat("m_speed", (_agent.velocity.magnitude / _agent.speed), .1f, Time.deltaTime);
        if (!_canMove) return;
        if (!Input.GetMouseButton(0)) return;

        _target = null;
        _agent.updateRotation = true;
        Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(movePosition, out var hitInfo, Mathf.Infinity, _playerMask)) return;
        _agent.isStopped = false;
        _agent.SetDestination(hitInfo.point);
    }
    IEnumerator AttackTimer()
    {
        // _animator.speed = _attackSpeed;
        _animator.SetTrigger("in_combat");
        yield return new WaitForSeconds(1 / _attackSpeed);
        _target.GetComponent<BaseUnit>().TakeDamage(_attack);
        _canAttack = true;
        _canMove = true;
        // _animator.SetBool("attack", false);
    }

    protected override void Die()
    {
        Instantiate(deathVFX ,gameObject.transform.position, deathVFX.transform.rotation);
        Destroy(gameObject);

        GameManager.gameOver = true;
    }

    void FaceTarget(GameObject target)
    {
      Vector3 direction = (target.transform.position - transform.position).normalized;
      Quaternion lookDirection = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
      transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 5f);
    }
}
