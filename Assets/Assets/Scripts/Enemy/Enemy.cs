﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour, IDamageable {
    
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

    public int MaxHealth
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
