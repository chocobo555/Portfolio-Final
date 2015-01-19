using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class NodeGenerator : MonoBehaviour
{
	static int xAxis = 5;
	static int yAxis = 5;
	
	public List<GameObject> grid;
	public GameObject node;

	bool DFSAhasBeenCalled = false;
	

	void Awake () 
	{ 
		//grid = new int[_width, _height];

		int intName = 1;
		for (var i = 0; i < yAxis; i++)
		{
			for (var j = 0; j < xAxis; j++)
			{
				//grid[j,i] = 0;

				Vector3 tempVec = new Vector3((0 + (j * 8)), (0 + (i * 8)), 0);
				node.transform.position = tempVec;
				GameObject currGameObj = node;    
				grid.Add((GameObject)Instantiate(currGameObj));
				currGameObj.name = intName.ToString();
				//Node myNode = currGameObj.AddComponent(Node);
				//NoidConnections(currGameObj); it calls this on its own scripetd
				
				intName++;
			}
		}
	}


	// Update is called once per frame
	void Update () 
	{
		if (DFSAhasBeenCalled == false) 
		{
			DFSAhasBeenCalled = true;
			GameObject.Find("SceneManager").GetComponent<DFSA>().ProcessNode(grid[0]);
		}

	}
}






