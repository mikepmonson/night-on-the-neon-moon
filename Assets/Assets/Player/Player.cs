using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public float moveSpeed = 3f;
    public int health = 3;
    public float primaryCooldown = 1f;

    private Vector3 lastDir;


	// Use this for initialization
	void Start () {
        

	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
        HandleMovement();
	}

    void HandleInput()
    {
        lastDir = new Vector3(0, 0, 0);
        lastDir.x = Input.GetAxis("Horizontal");
        lastDir.y = Input.GetAxis("Vertical");

        if (!lastDir.x.Equals(0f) && !lastDir.y.Equals(0f))
        {
            lastDir *= 0.5f;
        }


        //ToDo: Handle Firing
    }
    void HandleMovement()
    {
        transform.Translate(lastDir * moveSpeed * Time.deltaTime);
    }
}
