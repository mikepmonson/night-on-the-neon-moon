using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class Enemy : MonoBehaviour, IDamageable {
    
    public enum EnemyState
    {
        Entering,
        Moving,
        Attacking,
        Dying
    }

    abstract public int Health
    {
        get; set;    
    }

    abstract public float MoveSpeed
    {
        get; set;
    }

    abstract public EnemyState CurrentState
    {
        get; set;
    }

    abstract public void TakeDamage(int amount);
    
}
