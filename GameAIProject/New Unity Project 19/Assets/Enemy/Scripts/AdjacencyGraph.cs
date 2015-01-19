using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AdjacencyGraph<K>
{
	private List<List<K>> vertexList = new List<List<K>>();
	private Dictionary<K, List<K>> vertexDict = new Dictionary<K, List<K>>();



	public AdjacencyGraph(K rootVertexKey)
	{
		AddVertex(rootVertexKey);
	}


	private List<K> AddVertex(K key)
	{
		List<K> vertex = new List<K>();
		vertexList.Add(vertex);
		vertexDict.Add(key, vertex);

		return vertex;
	}


	public void AddEdge(K startKey, K endKey)
	{
		List<K> startVertex = vertexDict.ContainsKey(startKey) ? vertexDict[startKey] : null;
		List<K> endVertex = vertexDict.ContainsKey(endKey) ? vertexDict[endKey] : null;

		if (startVertex == null) 
		{
			throw new ArgumentException("cannot create edge from a non-existent start vertex.");
		}

		if (endVertex == null) 
		{
			endVertex = AddVertex(endKey);
		}

		startVertex.Add(endKey);
		endVertex.Add(startKey);

	}


	public void RemoveVertex(K key)
	{
		List<K> vertex = vertexDict[key];

		//first remove the edges/adjacency entries
		int vertexNumAdjacent = vertex.Count;
		for (int i = 0; i < vertexNumAdjacent; i++) 
		{
			K neighbourVertexKey = vertex[i];
			RemoveEdge(key, neighbourVertexKey);
		}

		//lastly remove the vertex/adj. list
		vertexList.Remove(vertex);
		vertexDict.Remove (key);

	}
	

	public void RemoveEdge(K startKey, K endKey)
	{
		((List<K>)vertexDict[startKey]).Remove(endKey);
		((List<K>)vertexDict[endKey]).Remove(startKey);
	}


	public bool Contains(K key)
	{
		return vertexDict.ContainsKey(key);
	}


	public int VertexDegree(K key)
	{
		return vertexDict[key].Count;
	}

}
