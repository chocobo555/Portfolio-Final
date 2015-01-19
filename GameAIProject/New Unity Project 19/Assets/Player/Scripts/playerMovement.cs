using UnityEngine;
using System.Collections;

public class playerMovement : MonoBehaviour 
{
	float moveSpeed = 450;
	float rotSpeed = 200;


	// Update is called once per frame
	void Update () 
	{

		if (Input.GetKey(KeyCode.W))
		{
			rigidbody2D.AddForce(transform.up * moveSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
		}

		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.back * rotSpeed * Time.deltaTime);
		}

	}

	/*
	public void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") 
		{
			Destroy(gameObject);
		}
	}
	*/
}
