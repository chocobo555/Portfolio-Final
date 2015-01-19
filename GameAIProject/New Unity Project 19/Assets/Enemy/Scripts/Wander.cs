using UnityEngine;
using System.Collections;

//public sealed class Wander : FSMState<Enemy>
//{
	/*

	Vector2 vecConvert;
	Vector2 heading;
	Vector2 destinationHeading;
	float dotFloat;
	//float currDot;
	Vector2 currDestinationHeading;
	
	static readonly Wander instance = new Wander();
	
	public static Wander Instance 
	{
		get 
		{
			return instance;
		}
	}
	
	
	static Wander() { }
	private Wander() { }
	
	
	public override void Enter (Enemy e)
	{
		if (e.Location != Locations.nearPlayer) 
		{
			//Debug.Log("I dont see the player. Heading to patrol point A");
			//Debug.Log("Entering State: MoveToPatrolPointA");
			e.ChangeLocation(Locations.nearPlayer);
		}
	}
	
	
	public override void Execute (Enemy e) 
	{
		if (e.playerIsNear == true) 
		{
			if (e.playerLevel > e.enemyLevel) 
			{
				//Debug.Log("There's the player!! Get them!!!");
				//Debug.Log("Changing State: RetreatFromPlayer");
				e.ChangeState(RetreatFromPlayer.Instance);
			}
			else 
			{
				//Debug.Log("There's the player!! Get them!!!");
				//Debug.Log("Changing State: AttackPlayer");
				
				//e.ChangeState(AttackPlayer.Instance);
				e.ChangeState(PursuePlayer.Instance);
			}
		}
		else 
		{
			//Debug.Log("Executing State: MoveToPatrolPointA");
			
			vecConvert = e.PatrolPointA.transform.position;
			
			heading = e.calculateHeading(e.gameObject);
			destinationHeading = e.Wander(e.gameObject, 2, 3, .5);
			
			heading.Normalize();
			destinationHeading.Normalize();
			
			//Debug.Log("HeadingH: " + heading);
			//Debug.Log("HeadingD: " + destinationHeading);
			
			dotFloat = Vector2.Dot(heading, destinationHeading);
			//Debug.Log(dotFloat);
			
			//e.Movement(e.rigidbody2D,(destinationHeading));
			//e.Rotation(e.transform);
			
			//Debug.Log(dotFloat);
			
			if (dotFloat > -.01 && dotFloat < .01)
			{
				//Debug.Log("meep! ");
				e.Movement(e.rigidbody2D,(destinationHeading));
				//e.SteeringForce(e.Seek(e.gameObject, vecConvert, e.speed), e.gameObject, 100);
			}
			else 
			{
				if (dotFloat < -.01) 
				{
					e.Rotation(e.transform, Vector3.back, 40);
				}
				else 
				{
					e.Rotation(e.transform, Vector3.forward, 40);
				}
				
			}	
			
			//Debug.Log ("Heading: " + e.calculateHeading(e.gameObject));
		}

	}
	
	
	public override void Exit(Enemy e) 
	{
		e.hasReachedPointA = false;
		//Debug.Log("Moving to Patrol point B");
		//Debug.Log("Exiting State: MoveToPatrolPointA");
	}

	*/
//}
