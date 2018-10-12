using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable {

    public int Health { get; set; }
    [SerializeField]
    private Sprite _destroyedSprite;

    // Use this for initialization
    void Start () {
        Health = 3;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)
    {
        this.Health -= amount;
        Debug.Log(this.name + " took " + amount + " damage.");
        Debug.Log(this.name + " Health: " + this.Health);
        if (this.Health <= 0)
            this.gameObject.GetComponent<SpriteRenderer>().sprite = _destroyedSprite;
    }
}
