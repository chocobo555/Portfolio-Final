using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public sealed class MoveToPatrolPointB : FSMState<Enemy>
{
	Vector2 vecConvert;
	Vector2 heading;
	Vector2 destinationHeading;
	float dotFloat;

	static readonly MoveToPatrolPointB instance = new MoveToPatrolPointB();

	public static MoveToPatrolPointB Instance
	{
		get
		{
			return instance;
		}
	}
	static MoveToPatrolPointB() {}
	private MoveToPatrolPointB() {}


	public override void Enter (Enemy e)
	{
		if (e.Location != Locations.PatrolPointB) 
		{
			//Debug.Log("Heading to patrol point B");
			//Debug.Log("Entering State: MoveToPatrolPointB");
			e.ChangeLocation(Locations.PatrolPointB);
		}
	}


	public override void Execute(Enemy e)
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
			//Debug.Log("Patrolling.");
			//Debug.Log("Executing State: MoveToPatrolPointB");

			Vector3 newDestination = new Vector3(0,0,0);
	
			if (GameObject.Find("SceneManager").GetComponent<DFSA>().processDone == true)  //.output.Count == 24) 
			{
				//Debug.Log("B: " + e.pickPatrolB);
				
				for (int i = 0; i < e.AreaNodes.Count; i++) 
				{
					if (e.AreaNodes[i].name == e.pickPatrolBName) 
					{
						newDestination = e.AreaNodes[i].transform.position;
						//Debug.Log("b fires off");
						e.AreaNodes[i].tag = "CheckPointB";
					}
				}
				
				vecConvert = newDestination;
			}
			else 
			{
				vecConvert = e.PatrolPointB.transform.position;
			}

			//Vector2 vecConvert = e.PatrolPointB.transform.position;



			heading = e.calculateHeading(e.gameObject);
			destinationHeading = e.Seek(e.gameObject,vecConvert, e.speed) * Time.deltaTime;

			heading.Normalize();
			destinationHeading.Normalize();

			dotFloat = Vector2.Dot(heading, destinationHeading);

			//e.Rotation(e.transform);
			//e.Movement(e.rigidbody2D,(destinationHeading));

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


		if (e.hasReachedPointB == true)
		{
			Debug.Log ("PatrolPointB reached.");
			e.ChangeState(MoveToPatrolPointA.Instance);
		}
	}

	public override void Exit(Enemy e)
	{
		e.hasReachedPointB = false;
		//Debug.Log("Heading back to patrol point A");
		//Debug.Log("Exiting State: MoveToPatrolPointB");
	}

};








