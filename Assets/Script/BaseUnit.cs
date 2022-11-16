using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private int _attack = 10;
    abstract public void Attack();
    abstract public void TakeDamage(int damage);
    void OnMouseEnter()
    {
        print("mouse enter");
    }
}
