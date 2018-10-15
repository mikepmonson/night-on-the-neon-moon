using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour {

    [SerializeField]
    private float _velocity; //speed of the projectile
    [SerializeField]
    private float _lifetime; //how long the projectile GameObject stays alive
    public float birth;
    private GameObject target;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        Vector3 offset = target.transform.position - this.transform.position;

        transform.rotation = Quaternion.LookRotation(
                               Vector3.forward, // Keep z+ pointing straight into the screen.
                               offset           // Point y+ toward the target.
                             );
    }
    // Use this for initialization
    void Start () {
        _velocity = 5f;
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
        this.transform.Translate(new Vector3(0, 1, 0) * _velocity * Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player p = other.GetComponent<Player>();
            p.TakeDamage(1);
            Destroy(this.gameObject);
        }
    }
}
