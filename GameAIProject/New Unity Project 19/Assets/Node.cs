using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node : MonoBehaviour 
{
	public List<GameObject> nodeArray;

	private GameObject adjacentUp;
	private GameObject adjacentRight;
	private GameObject adjacentDown;
	private GameObject adjacentLeft;
	private List<GameObject>Grid;
	bool initializeGrid = false; 
	bool callDFSA = false;



	// Use this for initialization
	void Start() 
	{
		Grid = GameObject.Find("SceneManager").GetComponent<NodeGenerator>().grid;
		
		NoidConnections();

		nodeArray.Add(gameObject);
		nodeArray.Add(adjacentUp);
		nodeArray.Add(adjacentRight);
		nodeArray.Add(adjacentDown);
		nodeArray.Add(adjacentLeft);
	}

	
	void NoidConnections()
	{	
		//print("still running");

		int currGameInt = 0;
		int indexNumber = 0;
		int indexNumberTotal = 0;
		char currGameChar = '0';

		string[] array = gameObject.name.Split('(');

		currGameInt = int.Parse(array[0]);

		int adjacentUpName = (currGameInt + 5);
		int adjacentDownName = (currGameInt - 5);
		int adjacentLeftName = (currGameInt - 1);
		int adjacentRightName = (currGameInt + 1);

		foreach (GameObject node in Grid)
		{
			if (node.name == (adjacentUpName.ToString() + "(Clone)"))
			{
				//print("node name matches adjacent name");
				adjacentUp = node;
			}
			
			if (node.name == (adjacentDownName.ToString() + "(Clone)"))
			{
				adjacentDown = node;
			}
			
			if (node.name == (adjacentLeftName.ToString() + "(Clone)"))
			{
				adjacentLeft = node;
			}
			
			if (node.name == (adjacentRightName.ToString() + "(Clone)"))
			{
				adjacentRight = node;
			}
		}
	}


	// Update is called once per frame
	void Update() { }

	
}




