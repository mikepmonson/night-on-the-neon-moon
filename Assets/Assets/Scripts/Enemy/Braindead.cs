using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
     * Todo: 
     * Add public properties: GameObject target, bool chasingPlayer, float buildingAttackCooldown
     * Add public methods: void Aggro(), void TargetNearestBuilding()
     * On update:

        If state is entering:
        Move towards target. If at target, acquire nearest building as target (TargetNearestBuilding) and enter moving state.

        If state is dying:
        Disable collider and rigidbody. Play death animation. Destroy object.

        Check health. If 0, enter dying state.

        If state is moving:
            If not ChasingPlayer:
                Check if building is in range. If so, enter attacking state.
        If state is attacking:
            If attack is not on cooldown, damage building. Otherwise, do nothing.
        On collision:
            If collision target is tagged as player, make them receive damage (IDamageable)   
    */
public class Braindead : MonoBehaviour{

    //Properties
    public GameObject Target { get; set; }
    public bool ChasingPlayer { get; set; }
    public float BuildingAttackCooldown { get; set; }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //methods
    public void Aggro()
    {

    }
    public void TargetNearestBuilding()
    {
        
    }
}


