using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Enemy, IDamageable {
    public override int Health { get; set; }
    public override float MoveSpeed { get; set; }
    public override EnemyState CurrentState { get; set; }
    [SerializeField]
    private GameObject _projectile;
    private float _fireCooldown;
    private float _lastFireTime;
    private float _fireRange;

    // Use this for initialization
    void Start () {
        _fireCooldown = 2f;
        _lastFireTime = 0;
        _fireRange = 4f;
        CurrentState = EnemyState.Moving;
        MoveSpeed = 2.1f;
        Health = 3;
	}
	
	// Update is called once per frame
	void Update () {
        if (CurrentState == EnemyState.Entering)
        {

        }
        else if (CurrentState == EnemyState.Moving)
        {
            //move towards target
            this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, MoveSpeed * Time.deltaTime);
        }
        else if (CurrentState == EnemyState.Attacking)
        {
            
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

    private void Fire()
    {
        if (CanFire() && Vector3.Distance(this.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= _fireRange)
        {
            Instantiate(_projectile, this.transform.position, Quaternion.identity);
            _lastFireTime = Time.time;
        }
        
    }

    public override void TakeDamage(int amount)
    {
        this.Health -= amount;
        if (this.Health <= 0)
        {
            CurrentState = EnemyState.Dying;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(1);
        }
    }

    private bool CanFire()
    {
        if(_lastFireTime + _fireCooldown <= Time.time)
            return true;
        
        else
            return false;
    }
}
