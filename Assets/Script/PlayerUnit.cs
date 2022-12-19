using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerUnit : BaseUnit
{
    [SerializeField]
    private NavMeshAgent _agent;
    private int _playerMask;
    public Vector3 Position => _agent.nextPosition;
    void Start()
    {
        _playerMask = LayerMask.NameToLayer("Player");
        _agent.speed = _moveSpeed;
    }
    void Update()
    {
        Move();
    }
    public override void Attack<T> (T target)
    {
        throw new System.NotImplementedException();
    }
    private void Move()
    {
        if (Input.GetMouseButton(1))
        {
            Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(movePosition, out var hitInfo, Mathf.Infinity, _playerMask))
            {
                _agent.SetDestination(hitInfo.point);
            }
        }
    }
}
