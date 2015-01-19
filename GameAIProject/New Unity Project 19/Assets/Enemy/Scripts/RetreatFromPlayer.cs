using UnityEngine;
using System.Collections;

public sealed class RetreatFromPlayer : FSMState<Enemy>
{
	Vector2 vecConvert;
	Vector2 heading;
	Vector2 destinationHeading;
	float dotFloat;
	bool dotValueCheck = false;
	float previuosDot = 0;
	//float currDot;
	Vector2 currDestinationHeading;

	static readonly RetreatFromPlayer instance = new RetreatFromPlayer();

	public static RetreatFromPlayer Instance
	{
		get
		{
			return instance;
		}
	}

	static RetreatFromPlayer() {}
	private RetreatFromPlayer() {}

	public override void Enter(Enemy e)
	{
		if (e.Location != Locations.nearPlayer) 
		{
			//Debug.Log("I will defeat you");
			//Debug.Log("Entering State: RetreatFromPlayer");
			e.ChangeLocation(Locations.nearPlayer);
		}
	}

	public override void Execute(Enemy e)
	{
		//Debug.Log("Executing State: RetreatFromPlayer");
		
		if (GameObject.Find ("Player") != null) 
		{
			//e.Retreat(e.transform, e.Player.transform, e.speed);

			vecConvert = e.Player.transform.position;
		
			heading = e.calculateHeading(e.gameObject);
			destinationHeading = e.Retreat(e.gameObject,vecConvert, e.speed) * Time.deltaTime;

			heading.Normalize();
			destinationHeading.Normalize();

			dotFloat = Vector2.Dot(heading, destinationHeading);

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
			previuosDot = dotFloat;

		}
		else if (GameObject.Find("Player") == null) 
		{
			if (e.getPreviuosState() == MoveToPatrolPointA.Instance) 
			{
				e.ChangeState(MoveToPatrolPointA.Instance);
			}
			else 
			{
				e.ChangeState(MoveToPatrolPointB.Instance);
			}
		}
	}


	public override void Exit(Enemy e) 
	{
		//Debug.Log("Moving to Patrol point A");
		//Debug.Log("Exiting State: AttackPlayer");
		e.playerIsNear = false;
	}
	
};










