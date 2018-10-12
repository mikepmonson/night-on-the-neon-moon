using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField]
    private float _velocity; //speed of the projectile
    [SerializeField]
    private float _lifetime; //how long the projectile GameObject stays alive
    public float birth;


    private void Awake()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle -= 90));
    }
    void Start () {
        _velocity = 3.0f;
        _lifetime = 5.0f;
        birth = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        Movement();
        if (Time.time - birth >= _lifetime)
        {
            Destroy(this.gameObject);
        }
    }


    private void Movement()
    {
        this.transform.Translate(new Vector3(0,1,0) * _velocity * Time.deltaTime);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            enemy.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
