using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float _velocity; //speed of the projectile
    [SerializeField]
    private float _lifetime; //how long the projectile GameObject stays alive
    public float birth;//time the projectile was instantiated

    /// <summary>
    /// Get the position of the cursor, set the z value to 0 to match other gameobjects. adjeust rotation of projectile to match the vector of the mouse
    /// </summary>
    private void Awake()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle -= 90));
    }

    /// <summary>
    /// set velocity, lifetime, and the game time the object was instantiated
    /// </summary>
    void Start () {
        _velocity = 3.0f;
        _lifetime = 5.0f;
        birth = Time.time;
    }
	
	/// <summary>
    /// move the object, and destroy the object if it has outlived its lifetime
    /// </summary>
	void Update () {
        Movement();
        if (Time.time - birth >= _lifetime)
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// move the projectile
    /// </summary>
    private void Movement()
    {
        this.transform.Translate(new Vector3(0,1,0) * _velocity * Time.deltaTime);
        
    }

    /// <summary>
    /// if the projectile comes in contact with an object with the tag of Enemy, damage the enemy
    /// </summary>
    /// <param name="other">the object this collided with</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
