using UnityEngine;
using System.Collections;

public class scriptPlayer : MonoBehaviour 
{
	public float playerSpeedVertical 	= 10;
	public float playerSpeedHorizontal 	= 10;
	public float xAxisMax 				= 6;
	public float xAxisMin 				= -6;
	public float yAxisMax 				= 6;
	public float yAxisMin 				= -6;
	public Transform projectile;
	public Transform socket;
	
	// Update is called once per frame
	void Update () 
	{
		float translationVertical = Input.GetAxis("Vertical") * playerSpeedVertical * Time.deltaTime;
        float translationHorizontal = Input.GetAxis("Horizontal") * playerSpeedHorizontal * Time.deltaTime;

		transform.Translate(translationHorizontal, translationVertical, 0);
		
		
		/*
		 if(transform.position.x >= 6)
		 	transform.position.x = 6;
		!!!Error will occur telling you you ned to store the value in a temproary variable!!!
		*/
		Vector3 RestrictPositions = transform.position; //copy to auxilary variable
		if(RestrictPositions.x >= xAxisMax)
			RestrictPositions.x = xAxisMax;//modify components
		if(RestrictPositions.x <= xAxisMin)
			RestrictPositions.x = xAxisMin;//modify components
		if(RestrictPositions.y >= yAxisMax)
			RestrictPositions.y = yAxisMax;//modify components
		if(RestrictPositions.y <= yAxisMin)
			RestrictPositions.y = yAxisMin;//modify components
		transform.position = RestrictPositions;//save modified value
		
		
		if (Input.GetKeyDown("space")) 
		{
			Instantiate(projectile, socket.position, socket.rotation);
		}
	}
} 