using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour
{

    [SerializeField] private float health;

	// Use this for initialization
	void Start ()
	{
	    health = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKey(KeyCode.A))
        {
            transform.position = new Vector3(transform.position.x - 0.02f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = new Vector3(transform.position.x + 0.02f, transform.position.y, transform.position.z);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.02f, transform.position.z);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
