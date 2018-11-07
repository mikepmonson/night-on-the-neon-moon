using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Braindead : Enemy, IDamageable{

    //Properties
    public GameObject Target { get; set; }
    public bool ChasingPlayer { get; set; }
    public float BuildingAttackCooldown { get; set; }
    public float AggroRange { get; set; }
    public override int Health { get; set; }
    public override float MoveSpeed { get; set; }
    public override EnemyState CurrentState { get; set; }
    public float LastAttacked { get; set; }
    public override int MaxHealth { get; set; }

    //THIS FIELD IS ONLY FOR TESTING UNTIL GAMEMANAGER IS COMPLETE
    [SerializeField]
    public List<GameObject> buildings;
    //END TESTING FIELD


    // Use this for initialization
    void Start () {
        LastAttacked = 0f;
        BuildingAttackCooldown = 2f;
        AggroRange = 1.5f;
        MoveSpeed = 2f;
        GameObject[] buildingsArr = GameObject.FindGameObjectsWithTag("Building");
        buildings = new List<GameObject>(buildingsArr);
        CurrentState = EnemyState.Moving;
        Target = GameObject.FindGameObjectWithTag("Player");
        Debug.Log("Started");
    }
	
	/// <summary>
    /// Check the aggro and check the status. if no buildings have more than 0 health, destroy the gameobject
    /// </summary>
	void Update () {  
        Aggro();
        //check status      
        CheckStatus();
        if (buildings.Count <= 0)
            Destroy(this.gameObject);

    }

    /// <summary>
    /// Check the status of the gameobject and act accordingly
    /// </summary>
    private void CheckStatus()
    {
        if(CurrentState == EnemyState.Entering)
        {

        }
        else if(CurrentState == EnemyState.Moving)
        {
            //move towards target
            this.transform.position = Vector3.MoveTowards(this.gameObject.transform.position, Target.transform.position, MoveSpeed * Time.deltaTime);
        }
        else if (CurrentState == EnemyState.Attacking)
        {
            Attack();
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
    }
    
    /// <summary>
    /// if this player comes in contact with a player, damage the player. if it is a building, start attacking the building
    /// </summary>
    /// <param name="other"> the other object this collided with </param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
                player.TakeDamage(1);
        }
        else if (other.tag == "Building")
        {
            CurrentState = EnemyState.Attacking;
        }
    }

    /// <summary>
    /// if the player is within aggro range, make the target the player and chase it
    /// otherwise, start moving to the nearest building that has more than 0 hp
    /// </summary>
    public void Aggro()
    {
        //if the difference between the player position and this gameObject position is in range, target the player 
        if (Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) <= AggroRange)
        {
            Target = GameObject.FindGameObjectWithTag("Player");
            ChasingPlayer = true;
            if (CurrentState != EnemyState.Moving)
                CurrentState = EnemyState.Moving;
        }
        else
        {
            Target = FindNearestBuilding();
            ChasingPlayer = false;     
        }
    }

    /// <summary>
    ///  finds the closest building to this gameobject
    /// </summary>
    /// <returns> closest building t othis object </returns>
    public GameObject FindNearestBuilding()
    {
            GameObject closestBuilding = buildings[0];
            foreach (GameObject bldg in buildings)
            {
                if (Vector3.Distance(this.transform.position, bldg.transform.position) < Vector3.Distance(this.transform.position, closestBuilding.transform.position))
                {
                    closestBuilding = bldg;
                }
            }
            return closestBuilding;      
    }

    /// <summary>
    /// if not chasing the player and can attack is true, if the target is a building, damage the building, set the last attacked time
    /// remove the building from the list if it is 0 hp or below
    /// </summary>
    private void Attack()
    {
        if (!ChasingPlayer && CanAttack() == true)
        {
            if (Target.tag == "Building")
            {
                Building bldg = Target.GetComponent<Building>();
                bldg.TakeDamage(1);
                Debug.Log(this.name + " is attacking " + bldg.name);
                LastAttacked = Time.time;
                if (bldg.Health <= 0)
                {
                    buildings.Remove(Target);
                    CurrentState = EnemyState.Moving;
                }
            }
        }
    }

    /// <summary>
    /// uses cooldown, current gametime, and the last attacked time to determine if the enemy can make an attack
    /// </summary>
    /// <returns> whether or not the braindead can attack</returns>
    private bool CanAttack()
    {
        if (LastAttacked + BuildingAttackCooldown <= Time.time)
            return true;
        else
            return false;
    }

    /// <summary>
    /// reduce health by the amoutn of damage taken. if the health is 0 or lower, enter dying state
    /// </summary>
    /// <param name="amount"> the amount of damage to take</param>
    public override void TakeDamage(int amount)
    {
        this.Health -= amount;
        if(this.Health <= 0)
        {       
             CurrentState = EnemyState.Dying;
        }
    }
}


