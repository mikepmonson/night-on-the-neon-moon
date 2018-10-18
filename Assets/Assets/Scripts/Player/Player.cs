﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public float moveSpeed = 3f;
    public int Health { get; set; }
    public float primaryCooldown = 1f;

    private Vector3 lastDir;



    // Use this for initialization
    void Start () {
        Health = 3;

	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
        HandleMovement();
	}

    void HandleInput()
    {
        lastDir = new Vector3(0, 0, 0);
        lastDir.x = Input.GetAxisRaw("Horizontal");
        lastDir.y = Input.GetAxisRaw("Vertical");

        //ToDo: Handle Firing
    }
    void HandleMovement()
    {
        lastDir = Vector3.Normalize(lastDir);
        transform.Translate(lastDir * moveSpeed * Time.deltaTime);
    }

    public void TakeDamage(int amount)
    {
        
    }

    //for shooting, when instantiating the projectile do: Instantiate(projectile, new Vector3(this.transform.position.x + .188, this.transform.position.y + .153, 0), Quaternion.Identity)
}
