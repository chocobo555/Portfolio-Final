using UnityEngine;
using System.Collections;

public class scriptAsteroid : MonoBehaviour 
{
	public float asteroidSpeed = 6;
	
	
	// Update is called once per frame
	void Update () 
	{
		transform.Translate(Vector3.down * asteroidSpeed * Time.deltaTime);
		
		Vector3 temp = transform.position;
		if (temp.y <= -6) 
		{
			temp.y = 8;
			temp.x = Random.Range(-7, 7);
		}
		transform.position = temp;
	}
}
