using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pathfinding : MonoBehaviour 
{
	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}
	
	private static Tile GetLowestScoreTile(List<Tile> openList)
	{
		Tile lowest = openList[0];
		
		foreach (Tile cur in openList)
		{
			if (cur.GetFScore() < lowest.GetFScore())
			{
				lowest = cur;
			}
		}
		
		return lowest;
	}
	
	private static float GetHeuristicCost(Tile start,Tile end)
	{
		return Vector3.Distance(start.GetPos(),end.GetPos());
	}
	
	private static float GetDistance(Tile first,Tile second)
	{
		if (first.transform.position == second.transform.position)
		{
			return 0;
		}
		
		return 1;
	}
	
	public static bool IsPathfindingPossible(Tile start,Tile end)
	{
		List<Tile> closedList = new List<Tile>();
		List<Tile> openList = new List<Tile>();
		Dictionary<Tile,Tile> cameFrom = new Dictionary<Tile,Tile>();
		Tile current;
		float tentativeGScore;
		bool isPossible = false;
		
		if (end.IsGraveSpot())
		{
			end.SetOpen(true);
			//end.GetOtherGraveTile().SetOpen(true);
		}
		
		start.SetGScore(0);
		start.SetFScore(start.GetGScore() + GetHeuristicCost(start,end));
		openList.Add(start);
		
		while (openList.Count > 0)
		{
			current = GetLowestScoreTile(openList);
			if (current == end)
			{
				isPossible = true;
				break;
			}
			
			openList.Remove(current);
			closedList.Add(current);
			foreach (Tile neighbor in current.GetNeighbors())
			{
				if (closedList.Contains(neighbor))
				{
					continue;
				}
				
				//tentativeGScore = current.GetGScore() + GetDistance(current,neighbor);
				tentativeGScore = current.GetGScore() + 1;
				
				if ((!openList.Contains(neighbor)) || (tentativeGScore < neighbor.GetGScore()))
				{
					neighbor.SetGScore(tentativeGScore);
					neighbor.SetFScore(neighbor.GetGScore() + GetHeuristicCost(neighbor,end));
					cameFrom[neighbor] = current;
					if (!openList.Contains(neighbor))
					{
						openList.Add(neighbor);
					}
				}
			}
		}
		
		if (end.IsGraveSpot())
		{
			end.SetOpen(false);
			//end.GetOtherGraveTile().SetOpen(false);
		}
		
		return isPossible;
	}
	
	public static Tile GetNextTile(Tile start,Tile end)
	{
		List<Tile> closedList = new List<Tile>();
		List<Tile> openList = new List<Tile>();
		Dictionary<Tile,Tile> cameFrom = new Dictionary<Tile,Tile>();
		Tile current;
		float tentativeGScore;
		
		start.SetGScore(0);
		start.SetFScore(start.GetGScore() + GetHeuristicCost(start,end));
		openList.Add(start);
		
		while (openList.Count > 0)
		{
			current = GetLowestScoreTile(openList);
			if (current == end)
			{
				if (cameFrom.Count < 1)
				{
					//Debug.Log("No nodes found");
					return start;
				}
				
				return ReconstructPath(cameFrom, end);
			}
			
			openList.Remove(current);
			closedList.Add(current);
			foreach (Tile neighbor in current.GetNeighbors())
			{
				if (closedList.Contains(neighbor))
				{
					continue;
				}
				
				//tentativeGScore = current.GetGScore() + GetDistance(current,neighbor);
				tentativeGScore = current.GetGScore() + 1;
				
				if ((!openList.Contains(neighbor)) || (tentativeGScore < neighbor.GetGScore()))
				{
					neighbor.SetGScore(tentativeGScore);
					neighbor.SetFScore(neighbor.GetGScore() + GetHeuristicCost(neighbor,end));
					cameFrom[neighbor] = current;
					if (!openList.Contains(neighbor))
					{
						openList.Add(neighbor);
					}
				}
			}
		}
	    Debug.Log("Cannot pathfind");
		return start;
	}
	
	private static Tile ReconstructPath(Dictionary<Tile,Tile> cameFrom, Tile curTile)
	{
		
		List<Tile> path = new List<Tile>();
		
		while (cameFrom.ContainsKey(curTile))
		{
			path.Add(curTile);
			curTile = cameFrom[curTile];
		}
		
		path.Reverse();
		
		return path[0];
	}
}
