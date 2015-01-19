using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum Locations { nearPlayer, PatrolPointA, PatrolPointB };


public class Enemy : SteeringBehaviorAndLocomotion
{
	private FiniteStateMachine<Enemy> FSM;
	//public SteeringBehavior SB;

	public Locations Location = Locations.PatrolPointA;

	public List<GameObject> AreaNodes;

	public GameObject Player;
	public GameObject PatrolPointA;
	public GameObject PatrolPointB;
	public GameObject playerHeading;

	public bool playerIsNear = false;
	public bool pursueRadius = false;
	public bool hasReachedPointA = false;
	public bool hasReachedPointB = false;
	public float horizontalSpeed = 10;
	public float verticalSpeed = 10;
	public float speed = 40;
	public float playerLevel = 3;
	public float enemyLevel = 10;


	public int pickPatrolA = 0;
	public int pickPatrolB = 1;

	public string pickPatrolAName = "";
	public string pickPatrolBName = "";


	public void OnTriggerEnter2D(Collider2D other)
	{
		//Debug.Log("has collided with " + other.gameObject.name + "*****************************************************");

		if (other.tag == "CheckPointA" && getCurrentState() == MoveToPatrolPointA.Instance)
		{
			hasReachedPointA = true;
			pickPatrolA++;

		}

		if (other.tag == "CheckPointB" && getCurrentState() == MoveToPatrolPointB.Instance)
		{
			hasReachedPointB = true;
			pickPatrolB++;

		}

		/*
		if (other.tag == "playerDetection") 
		{
			playerIsNear = true;
		}
		*/

		if (other.tag == "playerCloseRadius") 
		{
			playerIsNear = true;
			pursueRadius = true;
		}

	}


	public void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "playerCloseRadius") 
		{
			pursueRadius = false;
		}
	}


	public void ChangeState(FSMState<Enemy> e)
	{
		FSM.ChangeState(e);
	}


	public FSMState<Enemy> getPreviuosState()
	{
		return FSM.getPreviuosState();
	}


	public FSMState<Enemy> getCurrentState()
	{
		return FSM.getCurrentState();
	}
	                                     

	public void Awake()
	{
		FSM = new FiniteStateMachine<Enemy>();
		FSM.Configure(this, MoveToPatrolPointA.Instance);
	}


	void Update () 
	{	
		FSM.Update ();
		
		AreaNodes = GameObject.Find("SceneManager").GetComponent<DFSA>().output;

		pickPatrolAName = pickPatrolA.ToString() + "(Clone)";
		pickPatrolBName = pickPatrolB.ToString() + "(Clone)";
	}


	public void ChangeLocation(Locations l)
	{
		Location = l;
	}

}





