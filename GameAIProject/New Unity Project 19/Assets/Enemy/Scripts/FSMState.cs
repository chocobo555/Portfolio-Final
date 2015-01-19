using UnityEngine;
using System.Collections;
using System.Collections.Generic;


abstract public class FSMState <T> //: MonoBehaviour 
{

	abstract public void Enter(T entity);
	abstract public void Execute(T entity);
	abstract public void Exit(T entity);


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}

