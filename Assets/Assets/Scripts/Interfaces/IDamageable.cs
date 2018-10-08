using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDamageable
{
    int Health
    {
        get; set;
    }

    void TakeDamage(int amount);
}
