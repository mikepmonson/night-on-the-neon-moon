using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable {
    public float moveSpeed = 3f;
    public int Health { get; set; }
    public float primaryCooldown = 1f;

    private Vector3 lastDir;
    [SerializeField]
    private GameObject projectile;



    // Use this for initialization
    void Start () {
        Health = 3;

	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
        HandleMovement();
        Shoot();
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

    /// <summary>
    /// This was used for testing, delete when ready to implement actual shooting
    /// </summary>
    private void Shoot()
    {
        if(Input.GetMouseButton(0))
        Instantiate(projectile, new Vector3(this.transform.position.x, this.transform.position.y + .153f, 0), Quaternion.identity);
    }


}
