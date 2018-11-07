using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IDamageable {

    public int Health { get; set; }//building health
    [SerializeField]
    private Sprite[] _buildingSprites;//array of building sprites to indicate damage state
    public int MaxHealth { get; set; }

    // Use this for initialization
    void Start () {
        Health = 4;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    /// <summary>
    /// reduce health and update sprite
    /// </summary>
    /// <param name="amount"> amount of damage to be taken</param>
    public void TakeDamage(int amount)
    {
        this.Health -= amount;
        Debug.Log(this.name + " took " + amount + " damage.");
        Debug.Log(this.name + " Health: " + this.Health);
        UpdateSprite();
    }

    /// <summary>
    /// update the sprite to match building health
    /// </summary>
    private void UpdateSprite()
    {
        this.gameObject.GetComponent<SpriteRenderer>().sprite = _buildingSprites[Health];
    }
}
