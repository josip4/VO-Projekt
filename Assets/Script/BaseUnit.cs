using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    #nullable enable
    abstract public void Attack<T>(T target) where T : BaseUnit?;
    public void Awake() {
        _health = _maxHealth;
    }
    public void TakeDamage(int damage)
    {
        _health -= damage;
        if (_health <= 0) Destroy(gameObject);
    }
    void OnMouseEnter()
    {
        print($"Health = {_health}");
    }
}
