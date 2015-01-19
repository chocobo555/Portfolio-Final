using UnityEngine;
using System.Collections;

public class scriptBullet : MonoBehaviour 
{
	public float bulletSpeed = 15;
	public float bulletRange = 10;
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(0, bulletSpeed * Time.deltaTime, 0);
		
		if (transform.position.y >= bulletRange) 
		{
			Destroy(gameObject);
		}
	}
	
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "asteroid") 
		{
			Vector3 temp = other.transform.position;
			temp.y = 8;
			temp.x = Random.Range(-7, 7);
			other.transform.position = temp;
		}
	}
}
