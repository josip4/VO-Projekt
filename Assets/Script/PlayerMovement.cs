using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent _agent;
    private int _playerMask;
    void Start()
    {
        _playerMask = LayerMask.NameToLayer("Player");
    }

    // Update is called once per frame
    void Update()
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
