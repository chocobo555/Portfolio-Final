using UnityEngine;
using System.Collections;

public sealed class AttackPlayer : FSMState<Enemy> 
{
	static readonly AttackPlayer instance = new AttackPlayer();

	public static AttackPlayer Instance
	{
		get
		{
			return instance;
		}
	}

	static AttackPlayer() {}
	private AttackPlayer() {}

	public override void Enter(Enemy e)
	{
		if (e.Location != Locations.nearPlayer) 
		{
			//Debug.Log("I will defeat you");
			//Debug.Log("Entering State: AttackPlayer");
			e.ChangeLocation(Locations.nearPlayer);
		}
	}

	public override void Execute(Enemy e)
	{
		//Debug.Log("Executing State: AttackPlayer");
		

		if (GameObject.Find("Player") != null) 
		{

			//e.Seek(e.transform, e.Player.transform, e.speed);

			Vector2 vecConvert = e.Player.transform.position;
			//Debug.Log(e.Seek(e.gameObject, vecConvert, e.speed));
			//e.transform.Translate(e.Seek(e.gameObject, vecConvert, e.speed) * Time.deltaTime);
			//e.SteeringForce(e.Seek(e.gameObject, vecConvert, e.speed), e.gameObject, 100);
			e.Movement(e.rigidbody2D,(e.Seek(e.gameObject, vecConvert, e.speed) * Time.deltaTime));

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
		//Debug.Log("Exiting State: AttackPlayer");
		e.playerIsNear = false;
	}
};








