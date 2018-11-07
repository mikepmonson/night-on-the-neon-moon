using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy, IDamageable {
    public override int Health { get; set; }
    public override float MoveSpeed { get; set; }
    public override EnemyState CurrentState { get; set; }
    public override int MaxHealth { get; set; }
    [SerializeField]
    private GameObject _projectile;
    private float _fireCooldown;
    private float _lastFireTime;
    private float _fireRange;

    /// <summary>
    /// Initializes cooldown, last fire time, fire range, enemy state, move speed, and health
    /// </summary>
    void Start () {
        _fireCooldown = 2f;
        _lastFireTime = 0;
        _fireRange = 4f;
        CurrentState = EnemyState.Moving;
        MoveSpeed = 2.1f;
        Health = 3;
	}
	
	/// <summary>
    /// Every game tick, this will check the enemy state, and calls the Fire() method
    /// </summary>
	void Update () {
        if (CurrentState == EnemyState.Entering)
        {
            //not being used at the moment
        }
        else if (CurrentState == EnemyState.Moving)
        {
            //move towards target
            this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, MoveSpeed * Time.deltaTime);
        }
        else if (CurrentState == EnemyState.Dying)
        {
            //disable rigidbody and collider
            if (this.GetComponent<Rigidbody2D>() != null && this.GetComponent<BoxCollider2D>() != null)
            {
                Destroy(this.GetComponent<Rigidbody2D>());
                Destroy(this.GetComponent<BoxCollider2D>());
            }
            //play death anim
                //can skip for now
            //destroy gameobject
            Destroy(this.gameObject);
        }

        Fire();
    }

    /// <summary>
    /// If the CanFire() method returns true, and this object is within range of the player, instantiate projectile and update last fired
    /// </summary>
    private void Fire()
    {
        if (CanFire() && Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= _fireRange)
        {
            Instantiate(_projectile, this.transform.position, Quaternion.identity);
            _lastFireTime = Time.time;
        }
        
    }

    /// <summary>
    /// reduce health by the amount taken, enter Dying state if health is les sthan or equal to zero
    /// </summary>
    /// <param name="amount">the amount of damage to be administered</param>
    public override void TakeDamage(int amount)
    {
        this.Health -= amount;//reduce health by amount
        if (this.Health <= 0)
        {
            CurrentState = EnemyState.Dying;//if health is <= 0, enter Dying state
        }
    }

    /// <summary>
    /// if this object collides with player, make player take damage
    /// </summary>
    /// <param name="other">object this collided with</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(1);
        }
    }

    /// <summary>
    /// will return the result of the last time fired + the cooldown compared to the current game time
    /// </summary>
    /// <returns>true if the cooldown has been satisfied</returns>
    private bool CanFire()
    {
        if(_lastFireTime + _fireCooldown <= Time.time)
            return true;
        
        else
            return false;
    }
}
