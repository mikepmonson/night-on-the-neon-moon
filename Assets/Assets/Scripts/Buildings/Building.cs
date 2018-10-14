using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable {

    public int Health { get; set; }
    [SerializeField]
    private Sprite[] _buildingSprites;

    // Use this for initialization
    void Start () {
        Health = 4;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void TakeDamage(int amount)
    {
        this.Health -= amount;
        Debug.Log(this.name + " took " + amount + " damage.");
        Debug.Log(this.name + " Health: " + this.Health);
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _buildingSprites[Health];
    }
}
