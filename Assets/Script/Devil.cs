using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Devil : BaseUnit
{
    [SerializeField]
    private float _minPlayerRange = 10f;
    private bool _canMove;
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
        AttackPlayer();
    }
    
    private bool PlayerInRange(out PlayerUnit player)
    {
        player = FindObjectOfType<PlayerUnit>();
        float distance = Vector3.Distance (player.transform.position, transform.position);
        return distance <= _minPlayerRange;

    }
    private void AttackPlayer()
	{
        bool inRange = PlayerInRange(out PlayerUnit player);
        _animator.SetBool("Moving", inRange);
        if (inRange)
        {
            print($"DEATH TO PLAYER");
            print($"ps={player.Position}");
            _agent.destination = player.Position;
        }
        else{
            _agent.isStopped = true;
        }
    }
    public override void Attack(BaseUnit target)
    {
        throw new System.NotImplementedException();
    }
}
