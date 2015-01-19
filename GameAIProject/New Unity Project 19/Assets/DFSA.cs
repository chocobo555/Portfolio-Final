using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DFSA : MonoBehaviour 
{
	
	public List<GameObject> Stack = new List<GameObject>();
	public List<GameObject> visited = new List<GameObject>();
	public List<List<string>> conversion = new List<List<string>>();

	//public string outputString;
	public List<GameObject> output;

	public bool processStarted = false;
	public bool processDone = false;

	public GameObject Node0;
	
	
	// Use this for initialization
	void Start () 
	{
		//Stack = GameObject.Find("Main Camera").GetComponent<NodeGenerator>().grid;
		
		//ProcessNode(Node0);  //**************************************************************************
		
	}
	
	
	List<GameObject> Conversion(GameObject convert)
	{
		List<GameObject> tempObj = null;

		//string[] array = convert.name.Split('(');
		//int tempInt = int.Parse(array[0]);

		//int convertInt = int.Parse(convert.name);

		GameObject.Find (convert.name);

		//this cant be finished until i have each node as a list of gameonjects containing its neihbors.
		
		return tempObj;
		
	}


	public void ProcessNode(GameObject node)
	{
		//print(node.name);
		//print(Stack.Count);
		bool beenVisited = false;
		int nodeCount = 0;

		for (int i = 0; i < node.GetComponent<Node>().nodeArray.Count; i++)
		{

			//Debug.Log("Checking each string in the current Node. The current string is:" + node[i]);
			beenVisited = false;
			foreach (GameObject item in visited) 
			{
				//Debug.Log("Checking each string in Visited. The current Item is:" + item);
				if (node.GetComponent<Node>().nodeArray[i] == item) 
				{
					beenVisited = true;
					nodeCount++;
				}
			}
			if (nodeCount == node.GetComponent<Node>().nodeArray.Count) 
			{
				//Debug.Log("About to remove top string in Stack. Removing:" + CallTopOfStack());
				Stack.Remove(CallTopOfStack());
			}
			else if(beenVisited == false)
			{
				Stack.Add(node.GetComponent<Node>().nodeArray[i]);
				//Debug.Log(node.GetComponent<Node>().nodeArray[i]);//************

				if (node.GetComponent<Node>().nodeArray[i] != null) 
				{
					output.Add(node.GetComponent<Node>().nodeArray[i]);
				}
			


				/*
				foreach (GameObject item in output) 
				{
					if (item != null) 
					{
						outputString = outputString + node.GetComponent<Node>().nodeArray[i].name;
					}
					else 
					{
						outputString = outputString + node.GetComponent<Node>().nodeArray[i];
					}
				}

				if (node.GetComponent<Node>().nodeArray[i] != null) 
				{
					output = output + node.GetComponent<Node>().nodeArray[i].name;
				}
				else 
				{
					output = output + node.GetComponent<Node>().nodeArray[i];
				}
				*/

				visited.Add (node.GetComponent<Node>().nodeArray[i]);
			}
		}

		processStarted = true;
	}

	
	GameObject CallTopOfStack()
	{

		int i;
		for (i = 0; i < Stack.Count; i++) { }
		int tempInt = i - 1;

		for (int j = tempInt; j > 1; j--) 
		{
			//print("for loop");
			if (Stack[j] == null) 
			{
				tempInt--;
			}
			else 
			{
				//Debug.Log(Stack[tempInt]);
				return Stack[tempInt];
			}
		}

		//Debug.Log(Stack[tempInt]);
		return null;
	}


	void FindNextNode()
	{
		//Debug.Log ("top of stack" + CallTopOfStack().name);
		if (CallTopOfStack() == null)
		{
			Debug.Log("All nodes found! :D");
			processDone = true;

			string outputString = "";
			
			foreach (GameObject item in output) 
			{
				if (item != null) 
				{
					outputString = outputString + item.name;
				}
				else 
				{
					outputString = outputString + item;
				}
			}
			print(outputString);
		}
		else 
		{
			ProcessNode(CallTopOfStack());
		}
		
	}
	
	
	// Update is called once per frame
	void Update () 
	{
		if (processStarted == true && processDone == false)
		{
			if (Stack.Count > 0) 
			{
				FindNextNode();
			}
			else 
			{
				//Debug.Log("Output: " + output);
				
			}	
		}

		
	}


}


