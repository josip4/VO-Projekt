using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseUnit : MonoBehaviour
{
    [SerializeField]
    protected int _maxHealth = 100;
    public int MaxHealth => _maxHealth;
    protected int _health;
    public int Health => _health;
    [SerializeField]
    protected int _attack = 10;
    [SerializeField]
    protected float _attackSpeed = 1f;
    [SerializeField]
    protected float _attackRange = 1f;
    [SerializeField]
    protected float _moveSpeed = 100;
    protected NavMeshAgent _agent;
    public Vector3 Position => _agent.nextPosition;

    #nullable enable
    abstract public void Attack(GameObject target);
    public void Awake() {
        _health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Die();
    }

    abstract protected void Die();


    void OnMouseEnter()
    {
        print($"Health = {_health}");
    }
}
