using UnityEngine;
using System.Collections;


public class SteeringBehaviorAndLocomotion : MonoBehaviour
{
	int timeElapsed = 0;
	public GameObject wanderTarget;

	// STEERING BEHAVIOR

	public Vector2 Seek(GameObject thisEntity, Vector2 targetPosition, float speed)// give maxspeed of particular entity
	{

		Vector2 vecConvert = thisEntity.transform.position;
		//Debug.Log (vecConvert);
		
		Vector2 tempVec2 = targetPosition - vecConvert;
		tempVec2.Normalize ();
		Vector2 desiredVelocity = tempVec2 * speed;

		return(desiredVelocity);
	}


	public Vector2 Retreat(GameObject thisEntity, Vector2 retreatFromPosition, float speed)
	{	
		Vector2 vecConvert = thisEntity.transform.position;

		Vector2 tempVec2 = vecConvert - retreatFromPosition;
		tempVec2.Normalize();
		Vector2 desiredVelocity = tempVec2 * speed;
			
		return(desiredVelocity - thisEntity.gameObject.rigidbody2D.velocity);
	}



	public Vector2 Wander(GameObject thisEntity, float wanderRadius, float wanderDistance, float wanderJitter)
	{
		wanderTarget.GetComponent<CircleCollider2D>().radius = wanderRadius;

		float tempFloat1 = Random.Range (-1, 1) * wanderJitter;
		float tempFloat2 = Random.Range (-1, 1) * wanderJitter;

		Vector3 tempVec = new Vector3(tempFloat1, tempFloat2, 0);

		//Vector2 vecConvert1 = wanderTarget.transform.position;

		wanderTarget.transform.position += tempVec;

		wanderTarget.transform.position.Normalize();

		//wanderTarget.transform.position.x *= wanderRadius;
		//wanderTarget.transform.position.y *= wanderRadius;

		Vector2 tempVec2 = new Vector2(wanderDistance, 0);

		Vector2 vecConvert1 = wanderTarget.transform.position;

		vecConvert1 *= wanderRadius;

		Vector2 targetLocal = vecConvert1 + tempVec2;

		Vector2 vecConvert2 = thisEntity.transform.position;

		return targetLocal - vecConvert2;

	}








	// LOCOMOTION

	public void Movement(Rigidbody2D movingEntity, Vector3 movementVector)
	{
		movementVector.Normalize();
		movingEntity.AddForce(movementVector * 5);

	}


	public Vector2 calculateHeading(GameObject thisEntity)
	{
		Vector2 tempVec = -thisEntity.transform.right;
		Vector2 heading = tempVec;

		return heading;
	}


	public void Rotation(Transform thisEntity, Vector3 turnDirection, float rotSpeed)
	{
		thisEntity.Rotate(turnDirection * rotSpeed * Time.deltaTime);
	}


	/*
	public void SteeringForce(Vector2 steeringActionVector, GameObject thisEntity, float maxSpeed)
	{
		Vector2 myVelocity = thisEntity.rigidbody2D.velocity;
		Debug.Log ("Velocity: " + myVelocity);
		Vector2 myPosition = thisEntity.transform.position;
		Debug.Log("Position: " + myPosition);
		//float myMass = thisEntity.rigidbody2D.mass;
		//Debug.Log("Mass: " + myMass);
		int timeElapsed = GetTimeElapsed();
		Debug.Log("Time Elapsed: " + timeElapsed);

		//calculate the combined force from each steering behavior
		Vector2 steeringForce = steeringActionVector;

		// Acceleration = Force/Mass
		Vector2 acceleration = steeringForce / myMass;
		//Debug.Log("Acceleration: " + acceleration);

		//update velocity
		myVelocity += acceleration * timeElapsed;


		//myVelocity.Truncate(maxSpeed);

		myPosition += myVelocity * timeElapsed;

	}
	*/

	

	// Update is called once per frame
	void Update () 
	{
		timeElapsed++;
	}
}



























