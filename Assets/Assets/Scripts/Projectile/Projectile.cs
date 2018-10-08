using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float _velocity; //speed of the projectile
    [SerializeField]
    private string[] _collidesWith; //array of tags that object can collide with

    private float _trajectory; //angle of travel
    private float _lifetime; //how long the projectile GameObject stays alive


    void Awake() {
        //find out if player or enemy shot
        //set _trajectory to the angle (target vector3 - origin vector3)
        if(this.tag == "ProjectilePlayer")
        {
            
        }
        else if(this.tag == "ProjectileEnemy")
        {

        }
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void Movement()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
    }
}
