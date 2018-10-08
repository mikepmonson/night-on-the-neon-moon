using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable {
    
    public enum EnemyState
    {
        Entering,
        Moving,
        Attacking,
        Dying
    }

    public int Health
    {
        get; set;    
    }

    public float MoveSpeed
    {
        get; set;
    }

    public EnemyState CurrentState
    {
        get; set;
    }

    public void TakeDamage(int amount)
    {
        this.Health -= amount;
    }
}
