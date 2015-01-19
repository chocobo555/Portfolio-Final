using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public sealed class MoveToPatrolPointA : FSMState<Enemy>
{
	Vector2 vecConvert;
	Vector2 heading;
	Vector2 destinationHeading;
	float dotFloat;
	//float currDot;
	Vector2 currDestinationHeading;

	static readonly MoveToPatrolPointA instance = new MoveToPatrolPointA();

	public static MoveToPatrolPointA Instance 
	{
		get 
		{
			return instance;
		}
	}


	static MoveToPatrolPointA() { }
	private MoveToPatrolPointA() { }


	public override void Enter (Enemy e)
	{
		if (e.Location != Locations.PatrolPointA) 
		{
			//Debug.Log("I dont see the player. Heading to patrol point A");
			//Debug.Log("Entering State: MoveToPatrolPointA");
			e.ChangeLocation(Locations.PatrolPointA);
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

			Vector3 newDestination = new Vector3(0,0,0);

			if (GameObject.Find("SceneManager").GetComponent<DFSA>().processDone == true)  //.output.Count == 24) 
			{
				//Debug.Log("A: " + e.pickPatrolA);

				for (int i = 0; i < e.AreaNodes.Count; i++) 
				{

					if (e.AreaNodes[i].name == e.pickPatrolAName) 
					{
						newDestination = e.AreaNodes[i].transform.position;
						//Debug.Log("a fires off");
						e.AreaNodes[i].tag = "CheckPointA";
					}
				}

				vecConvert = newDestination;
			}
			else 
			{
				vecConvert = e.PatrolPointA.transform.position;
			}


			heading = e.calculateHeading(e.gameObject);
			destinationHeading = e.Seek(e.gameObject,vecConvert, e.speed) * Time.deltaTime;

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
				//Debug.Log("else meep");
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


		if (e.hasReachedPointA == true) 
		{
			//Debug.Log("Patrolling can be so peaceful.");
			Debug.Log("PatrolPointA reached.");
			e.ChangeState(MoveToPatrolPointB.Instance);
		}
	}
	

	public override void Exit(Enemy e) 
	{
		e.hasReachedPointA = false;
		//Debug.Log("Moving to Patrol point B");
		//Debug.Log("Exiting State: MoveToPatrolPointA");
	}

};








