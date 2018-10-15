using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    [SerializeField]
    private float _velocity; //speed of the projectile
    [SerializeField]
    private float _lifetime; //how long the projectile GameObject stays alive
    public float birth; //the gametime that this object is instantiated
    private GameObject target; //the target of this projectile (player)

    /// <summary>
    /// Sets the rotation of this projectile to face the player
    /// </summary>
    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 offset = target.transform.position - this.transform.position;

        transform.rotation = Quaternion.LookRotation(
                               Vector3.forward, // Keep z+ pointing straight into the screen.
                               offset           // Point y+ toward the target.
                             );
    }
    /// <summary>
    /// Sets the velocity, projectile lifetime, and birth time
    /// </summary>
    void Start () {
        _velocity = 5f;
        _lifetime = 5.0f;
        birth = Time.time;
    }
	
	/// <summary>
    /// Every tick, calls Movement() method and destroys the gameobject if it has existed over _lifetime
    /// </summary>
	void Update () {
        Movement();
        if (Time.time - birth >= _lifetime)
        {
            Destroy(this.gameObject);
        }
    }
    /// <summary>
    /// moves towards the initial direction set upon instantiation
    /// </summary>
    private void Movement()
    {
        this.transform.Translate(new Vector3(0, 1, 0) * _velocity * Time.deltaTime);

    }
    /// <summary>
    /// if this collides with the player, damage the player
    /// </summary>
    /// <param name="other">the object this collided with</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            p.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
