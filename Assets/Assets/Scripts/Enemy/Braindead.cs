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
	
	// Update is called once per frame
	void Update () {
        if (buildings.Count <= 0)
            Destroy(this.gameObject);
        Aggro();
        //check status      
        CheckStatus();
        //check aggro
        
    }

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

    private bool CanAttack()
    {
        if (LastAttacked + BuildingAttackCooldown <= Time.time)
            return true;
        else
            return false;
    }

    public override void TakeDamage(int amount)
    {
        this.Health -= amount;
        if(this.Health <= 0)
        {       
             CurrentState = EnemyState.Dying;
        }
    }
}


