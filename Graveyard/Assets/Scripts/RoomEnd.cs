using UnityEngine;
using System.Collections;

public class RoomEnd : MonoBehaviour 
{
	[SerializeField] private Direction direction;
	
	[SerializeField] private bool used;

	void Start () 
	{
		used = false;
	}
	
	void Update () 
	{
		
	}
	
	private bool CanCreateWall()
	{
		Vector3 pos = transform.position;
		Vector3 tilePos = Vector3.zero;
		
		switch(direction)
		{
		case Direction.UP:
			tilePos = new Vector3(pos.x,pos.y-1,pos.z+1);
			break;
		case Direction.DOWN:
			tilePos = new Vector3(pos.x,pos.y-1,pos.z-1);
			break;
		case Direction.LEFT:
			tilePos = new Vector3(pos.x-1,pos.y-1,pos.z);
			break;
		case Direction.RIGHT:
			tilePos = new Vector3(pos.x+1,pos.y-1,pos.z);
			break;
		}
		
		Collider[] otherTiles = Physics.OverlapSphere(tilePos,0.4f);
		
		foreach (Collider ob in otherTiles)
		{
			if (ob.tag == "Floor")
			{
				return false;
			}
		}
		
		return true;
	}
	
	public void CreateWall(Vector2 roomSize)
	{
		/*if (!CanCreateWall())
		{
			return;
		}
	
		GameObject wall = GameObject.Instantiate(Resources.Load("Prefabs/Wall")) as GameObject;
		Vector3 pos = transform.position;
		float extraWalls = 0;
		
		wall.transform.position = new Vector3(pos.x,pos.y,pos.z);
		
		if (Enums.IsVertical(direction))
		{
			extraWalls = (roomSize.x-1)/2;
			for (int i=1; i<=extraWalls; i++)
			{
				wall = GameObject.Instantiate(Resources.Load("Prefabs/Wall")) as GameObject;
				wall.transform.position = new Vector3(pos.x-i,pos.y,pos.z);
				wall = GameObject.Instantiate(Resources.Load("Prefabs/Wall")) as GameObject;
				wall.transform.position = new Vector3(pos.x+i,pos.y,pos.z);
			}
		}
		else
		{
			extraWalls = (roomSize.y-1)/2;
			for (int i=1; i<=extraWalls; i++)
			{
				wall = GameObject.Instantiate(Resources.Load("Prefabs/Wall")) as GameObject;
				wall.transform.position = new Vector3(pos.x,pos.y,pos.z-i);
				wall = GameObject.Instantiate(Resources.Load("Prefabs/Wall")) as GameObject;
				wall.transform.position = new Vector3(pos.x,pos.y,pos.z+i);
			}
		}*/
	}
	
	public bool WasUsed()
	{
		return used;
	}
	
	public void SetUsed()
	{
		used = true;
	}
	
	public Direction GetDirection()
	{
		return direction;
	}
}
