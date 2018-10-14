using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public int Health { get; set; }
    // Use this for initialization
    void Start () {
        this.Health = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void TakeDamage(int amount)
    {
        this.Health -= amount;
    }
}
