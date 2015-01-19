using UnityEngine;
using System.Collections;

public class PursuePlayer : FSMState<Enemy>
{
	Vector2 vecConvert;
	Vector2 heading;
	Vector2 destinationHeading;
	float dotFloat;

	static readonly PursuePlayer instance = new PursuePlayer();
	
	public static PursuePlayer Instance
	{
		get
		{
			return instance;
		}
	}
	
	static PursuePlayer() {}
	private PursuePlayer() {}


	public override void Enter(Enemy e)
	{
		if (e.Location != Locations.nearPlayer) 
		{
			//Debug.Log("I will defeat you");
			//Debug.Log("Entering State: PursuePlayer");
			e.ChangeLocation(Locations.nearPlayer);
		}
	}


	public override void Execute(Enemy e)
	{
		//Debug.Log("Executing State: PursuePlayer");
	
		if (GameObject.Find("Player") != null) 
		{
			if (e.pursueRadius == false) 
			{
				vecConvert = e.playerHeading.transform.position;
			}
			else 
			{
				vecConvert = e.Player.transform.position;
			}
			//e.transform.Translate(e.Seek(e.gameObject, vecConvert, e.speed) * Time.deltaTime);

			heading = e.calculateHeading(e.gameObject);
			destinationHeading = e.Seek(e.gameObject,vecConvert, e.speed) * Time.deltaTime;

			heading.Normalize();
			destinationHeading.Normalize();
				
			dotFloat = Vector2.Dot(heading, destinationHeading);
				
				
			e.Movement(e.rigidbody2D,(destinationHeading));
			if (dotFloat < -.01) 
			{
				e.Rotation(e.transform, Vector3.back, 70);
			}
			else 
			{
				e.Rotation(e.transform, Vector3.forward, 70);
			}
		}
		else if (GameObject.Find("Player") == null)
		{
			if (e.getPreviuosState() == MoveToPatrolPointA.Instance) 
			{
				//Debug.Log("HEA YEA YEA YEA YEA YEA YEA YEA YEA YEA YEA YEA YEA YEA YEA YEA HEA YEA YEA");
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
		//Debug.Log("Exiting State: PursuePlayer");
		e.playerIsNear = false;
	}
}













